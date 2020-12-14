using System.Collections.Generic;
using UnityEngine;
public class PlayerInteract : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 1.5f;
    public CanvasMng canvasMng;

    private Transform           _cameraTransform;
    private Interactive         _currentInteractive;
    private Interactive         currentRequirement;
    //private Dialogue          dialogue;
    //private List<Interactive> waiting_Dialogue;
    //private List<Interactive> waiting_noninteractionDialogue;
    private bool                _requirementsInInventory;
    private List<Interactive>   _inventory;
    private int                 scrollSlot = 1;
    private int                 scrollAux = 0;
    private bool                added;

    [HideInInspector]
    public int playerSize = 0;

    void Start()
    {
        _cameraTransform            = GetComponentInChildren<Camera>().transform;
        _requirementsInInventory    = false;
        _inventory                  = new List<Interactive>();
    }

    void Update()
    {
        CheckForInteractive();
        CheckForScroll(0);
        CheckForInteraction();
        playerSize = FindObjectOfType<Player>().GetComponent<PlayerInteract>().playerSize;

        //Após protótipo, adicionar isto:
        //Se o timer do diálogo ter acabado
            // ClearCurrentDialogue();

            //Play waiting_Dialogue in queue se ele existir e ativa timer para impedir que outros diálogos dêm overlap
            //canvasMng.ShowDialoguePanel(recordedIntDialogue[0])
            //play audioclip
            //e apaga-se de forma definitiva o dialógo para que não se repita

            //ou se não existe:
            //Play waiting_noninteractionDialogue in queue se ele existir e ativa timer para impedir que outros diálogos dêm overlap
            //canvasMng.ShowDialoguePanel(recordedNIntDialogue[0])
            //play audioclip
            //e apaga-se de forma definitiva o dialógo para que não se repita
    }

    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo, MAX_INTERACTION_DISTANCE))
        {
            Interactive interactive = hitInfo.transform.GetComponent<Interactive>();

            if (interactive == null)
                ClearCurrentInteractive();

            else if (interactive != _currentInteractive)
                SetCurrentInteractive(interactive);
        }

        else
            ClearCurrentInteractive();
    }

    private void SetCurrentInteractive(Interactive interactive)
    {
        _currentInteractive = interactive;

        if (PlayerHasInteractionRequirements())
        {
            _requirementsInInventory = true;

            canvasMng.ShowInteractionPanel(interactive.GetInteractionText());

            // Diálogo é adicionado à queue  se ele existir relacionado com observar este objeto (quando se preenche os requerimentos)
            // Se for interactiveDialogue, o diálogo è adicionado à waiting_Dialogue e o texto à recordedIntDialogue[];
            // Se for positionDialogue, o diálogo è adicionado à waiting_noninteractionDialogue e o texto à recordedNIntDialogue[];
        }
        else
        {
            _requirementsInInventory = false;

            canvasMng.ShowInteractionPanel(interactive.requirementText);

            // Diálogo é adicionado à queue se ele existir relacionado com observar este objeto (quando se falha os requerimentos)
            // Se for interactiveDialogue, o diálogo è adicionado à waiting_Dialogue e o texto à recordedIntDialogue[];
            // Se for positionDialogue, o diálogo è adicionado à waiting_noninteractionDialogue e o texto à recordedNIntDialogue[];
        }
    }

    //Vê se o jogador preenche os requerimentos de interação
    private bool PlayerHasInteractionRequirements()
    {
        if (_currentInteractive.requirements == null)
        {
            return true;
        }

        if (_currentInteractive.orderedUsage && IsInInventory(_currentInteractive.requirements[_currentInteractive.numberOfUses]) && _currentInteractive.limitedItemUsageAtOnce)
        {
            return true;
        }
        else if (_currentInteractive.orderedUsage && !IsInInventory(_currentInteractive.requirements[_currentInteractive.numberOfUses]))
        {
            return false;
        }

        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            if (!IsInInventory(_currentInteractive.requirements[i]) && !_currentInteractive.limitedItemUsageAtOnce)
            {
                return false;
            }
            else if (_currentInteractive.limitedItemUsageAtOnce && IsInInventory(_currentInteractive.requirements[i]))
            {
                return true;
            }
        }

        if (_currentInteractive.limitedItemUsageAtOnce)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Desabilita interação e texto
    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
        canvasMng.HideInteractionPanel();
    }

    private void CheckForScroll(int inventoryChange)
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || inventoryChange == 1)
        {
            if(_inventory.Count == 0)
            {
                scrollAux  = 0;
                scrollSlot = 0;
            }
            else
            {
                scrollAux += 1;
                scrollSlot = Mathf.Abs(scrollAux % _inventory.Count);
                SelectInInventory();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || inventoryChange == 2)
        {
            if(_inventory.Count == 0)
            {
                scrollAux  = 0;
                scrollSlot = 0;
            }
            else
            {
                scrollAux -= 1;
                scrollSlot = Mathf.Abs(scrollAux % _inventory.Count);
                SelectInInventory();
            }
        }
    }

    //Faz desaparecer o diálogo
    // private void ClearCurrentDialogue()

    //Verifica interação
    private void CheckForInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == Interactive.InteractiveType.Pickable)
                PickCurrentInteractive();
            //else if (_currentInteractive.type == Interactive.InteractiveType.PositionDialogue)
                //continue
            else if (_currentInteractive.requirementForSize != 0 && _currentInteractive.requirementForSize == playerSize && _requirementsInInventory)
                InteractWithCurrentInteractive();
            else if (_requirementsInInventory && _currentInteractive.requirementForSize == 0)
                InteractWithCurrentInteractive();
        }
    }

    //Desativa objeto e adiciona-o ao inventário
    private void PickCurrentInteractive()
    {
        //Iman que puxa blocos (e futuros items que se ativam ao serem picked up)
        if(_currentInteractive.useWhenPicked)
            _currentInteractive.Activate();
            //se não funcionar trocar activate por interact e fazer activationchain com um outro objeto que os blocos vão utilizar para procurar confirmação
        if(_currentInteractive.interactWhenPicked)
            _currentInteractive.Interact();
            //O _currentInteractive.interactionDialogue è adicionado à waiting_Dialogue e o _currentInteractive.interactionDialogueText à recordedIntDialogue[];
            //Se for necessário um diálogo para a activation e interaction de um mesmo objeto, pode se adicionar activationDialogue ao script interactive.cs e mudar l135

        _currentInteractive.gameObject.SetActive(false);
        AddToInventory(_currentInteractive);
    }

    //Interage com o objeto e remove os items do inventário se a interação os consome
    private void InteractWithCurrentInteractive()
    {

        if (_currentInteractive.orderedUsage && IsInInventory(_currentInteractive.requirements[_currentInteractive.numberOfUses]) && _currentInteractive.limitedItemUsageAtOnce && scrollSlot == _inventory.IndexOf(_currentInteractive.requirements[_currentInteractive.numberOfUses]))
        {
            currentRequirement = _currentInteractive.requirements[_currentInteractive.numberOfUses];

            if (currentRequirement.usedOnAnimation == true)
            {
                currentRequirement.gameObject.SetActive(true);
            }

            currentRequirement.Interact();

            if (currentRequirement.consumesItem == true)
            {
                RemoveFromInventory(currentRequirement);
            }

            _currentInteractive.Interact();

            ClearCurrentInteractive();
            return;
        }

        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
        {
            if (IsInInventory(_currentInteractive.requirements[i]) && _currentInteractive.limitedItemUsageAtOnce && scrollSlot == _inventory.IndexOf(_currentInteractive.requirements[i]))
            {
                currentRequirement = _currentInteractive.requirements[i];

                if (currentRequirement.usedOnAnimation == true)
                {
                    currentRequirement.gameObject.SetActive(true);
                }

                currentRequirement.Interact();

                if (currentRequirement.consumesItem == true)
                {
                    RemoveFromInventory(currentRequirement);
                }

                break;
            }

            if (IsInInventory(_currentInteractive.requirements[i]) && !_currentInteractive.limitedItemUsageAtOnce)
            {
                currentRequirement = _currentInteractive.requirements[i];

                if (currentRequirement.usedOnAnimation == true)
                {
                    currentRequirement.gameObject.SetActive(true);
                }

                currentRequirement.Interact();

                if (currentRequirement.consumesItem == true)
                {
                    RemoveFromInventory(currentRequirement);
                }
            }

            //Se interactionDialogue:
            //O _currentInteractive.interactionDialogue è adicionado à waiting_Dialogue e o _currentInteractive.interactionDialogueText à recordedIntDialogue[];
        }

        _currentInteractive.Interact();
        //Se interactionDialogue:
        //O _currentInteractive.interactionDialogue è adicionado à waiting_Dialogue e o _currentInteractive.interactionDialogueText à recordedIntDialogue[];

        ClearCurrentInteractive();
    }

    //Adiciona objeto ao inventário e atribui-lhe um icon
    private void AddToInventory(Interactive item)
    {
        _inventory.Add(item);
        canvasMng.SetInventoryIcon(_inventory.Count - 1, item.icon);
        CheckForScroll(1);
    }

    //Remove objeto e o seu icon do inventário
    private void RemoveFromInventory(Interactive item)
    {
        _inventory.Remove(item);

        canvasMng.ClearInventoryIcons();

        for (int i = 0; i < _inventory.Count; ++i)
            canvasMng.SetInventoryIcon(i, _inventory[i].icon);

        CheckForScroll(2);
    }

    //Vê se objeto está no inventário
    private bool IsInInventory(Interactive item)
    {
        return _inventory.Contains(item);
    }

    private void SelectInInventory()
    {
        canvasMng.SetSelectedIcon(scrollSlot, _inventory.Count);
    }

}