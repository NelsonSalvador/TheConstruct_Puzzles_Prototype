using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 1.5f;
    public CanvasManager canvasMng;

    private Transform           _cameraTransform;
    private Interactive         _currentInteractive;
    //private Dialogue          dialogue;
    //private List<Interactive> waiting_Dialogue;
    //private List<Interactive> waiting_noninteractionDialogue;
    private bool                _requirementsInInventory;
    private List<Interactive>   _inventory;

    void Start()
    {
        _cameraTransform            = GetComponentInChildren<Camera>().transform;
        _requirementsInInventory    = false;
        _inventory                  = new List<Interactive>();
    }

    void Update()
    {
        CheckForInteractive();
        CheckForInteraction();
    }

    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo, MAX_INTERACTION_DISTANCE))
        {
            Interactive interactive = hitInfo.transform.GetComponent<Interactive>();

            //Após protótipo modificar isto de modo a acontecer com um timer (não dependente da posição do jogador, mas sim do diálogo ter acabado)
            if (interactive == null)
                ClearCurrentInteractive();
                // Clear waiting_Dialogue

            else if (interactive != _currentInteractive)
                SetCurrentInteractive(interactive);

            //Play waiting_Dialogue in queue se ele existir e nenhum timer está ativo
            //Se played i in waiting_dialogue também pertence a waiting_noninteractionDialogue ela é retirada da lista;
            //Play waiting_noninteractionDialogue in queue se ele existir e nenhum timer está ativo
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

            canvasMng.ShowInteractionPanel(interactive.interactionText);
            // Aparece diálogo se ele existir relacionado com observar este objeto (quando se preenche os requerimentos) e ativa timer para impedir que outros diálogos dêm overlap
            // Se algum interactiveDialogue der overlap, o diálogo è adicionado à waiting_Dialogue
            // Se algum positionDialogue der overlap, o diálogo è adicionado à waiting_Dialogue e à waiting_noninteractionDialogue;

            //Se Dialogue:
            //canvasMng.ShowDialoguePanel(interactive.interactiveDialogue)/canvasMng.ShowDialoguePanel(interactive.positionDialogue);
            //play audioclip
            //e apaga-se de forma definitiva o dialógo para que não se repita
        }
        else
        {
            _requirementsInInventory = false;

            canvasMng.ShowInteractionPanel(interactive.requirementText);
            // Aparece diálogo se ele existir relacionado com observar este objeto (quando se falha os requerimentos) e ativa timer para impedir que outros diálogos dêm overlap
            // Se algum interactiveDialogue der overlap, o diálogo è adicionado à waiting_Dialogue
            // Se algum positionDialogue der overlap, o diálogo è adicionado à waiting_Dialogue e à waiting_noninteractionDialogue;

            //Se Dialogue:
            //canvasMng.ShowDialoguePanel(interactive.interactiveDialogue)/canvasMng.ShowDialoguePanel(interactive.positionDialogue);
            //play audioclip
            //e apaga-se de forma definitiva o dialógo para que não se repita
        }
    }

    //Vê se o jogador preenche os requerimentos de interação
    //Por agora só inclui requerimentos relacionados com inventário
    //pode vir a ser necessário adicionar outro tipo de requerimentos (por exemplo: ações realizadas no passado)(ou mexer na interaction chain)
    private bool PlayerHasInteractionRequirements()
    {
        //Não existem requerimentos
        if (_currentInteractive.requirements == null)
            return true;

        //Existe requerimentos que o jogador não preenche
        for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
            if (!IsInInventory(_currentInteractive.requirements[i]))
                return false;

        //Ele preenche os requerimentos
        return true;
    }

    //Faz desaparecer o diálogo e desabilita interação
    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
        canvasMng.HideInteractionPanel();
    }

    //Verifica interação
    private void CheckForInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == Interactive.InteractiveType.Pickable)
                PickCurrentInteractive();
            //else if (_currentInteractive.type == Interactive.InteractiveType.PositionDialogue)
                //continue
            else if (_requirementsInInventory)
                InteractWithCurrentInteractive();
        }
    }

    //Desativa objeto
    private void PickCurrentInteractive()
    {
        _currentInteractive.gameObject.SetActive(false);
        AddToInventory(_currentInteractive);
    }

    //Interage com o objeto e remove os items do inventário se a interação os consome
    private void InteractWithCurrentInteractive()
    {
        if(_currentInteractive.consumesItem == true)
            for (int i = 0; i < _currentInteractive.requirements.Length; ++i)
                RemoveFromInventory(_currentInteractive.requirements[i]);

        _currentInteractive.Interact();
    }

    //Adiciona objeto ao inventário e atribui-lhe um icon
    private void AddToInventory(Interactive item)
    {
        _inventory.Add(item);
        canvasMng.SetInventoryIcon(_inventory.Count - 1, item.icon);
    }

    //Remove objeto e o seu icon do inventário
    private void RemoveFromInventory(Interactive item)
    {
        _inventory.Remove(item);

        canvasMng.ClearInventoryIcons();

        for (int i = 0; i < _inventory.Count; ++i)
            canvasMng.SetInventoryIcon(i, _inventory[i].icon);
    }

    //Vê se objeto está no inventário
    private bool IsInInventory(Interactive item)
    {
        return _inventory.Contains(item);
    }

}