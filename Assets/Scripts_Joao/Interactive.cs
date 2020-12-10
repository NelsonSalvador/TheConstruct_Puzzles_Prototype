using UnityEngine;

public class Interactive : MonoBehaviour
{
    public enum InteractiveType {Pickable, One_Use, Multi_Uses, Indirect};
    public InteractiveType type;
    public bool             isActive;
    public bool             multiPickable;
    public bool            consumesItem;
    public bool            useOnPickup;
    public string[]          interactionTexts;
    //public audio         interactiveDialogue;
    //public audio         interactionDialogue;
    //public string        interactiveDialogueText;
    //public string        interactionDialogueText;
    //public string        positionDialogue; diálogos que são triggered após o jogador pisar blocos interativos invisiveis no chão
    //public int           dialogueTimer;
    //public bool          playedInteractiveDialogue;
    //public bool          playedInteractionDialogue;
    //public bool          playedPositionDialogue;
    public string          requirementText;
    public Interactive[]    activationChain;
    public Interactive[]   interactionChain;
    public Texture         icon;
    public Interactive[]   requirements;
    private int      _curInteractionTextId;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _curInteractionTextId   = 0;
        multiPickable = false;
        consumesItem = false;
        useOnPickup = false;
    }

    public string GetInteractionText()
    {
        return interactionTexts[_curInteractionTextId];
    }

    //Só é chamada por interações que ativam outras
    public void Activate()
    {
        isActive = true;

        if (_anim != null)
            _anim.SetTrigger("Activate");
    }

    private void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; ++i)
                activationChain[i].Activate();
        }
    }

    public void Interact()
    {
        if (_anim != null)
            _anim.SetTrigger("Interact");

        if (isActive)
        {
            ProcessActivationChain();
            ProcessInteractionChain();

            if (type == InteractiveType.One_Use || type == InteractiveType.Pickable)
                GetComponent<Collider>().enabled = false;
            else if (type == InteractiveType.One_Use)
                _curInteractionTextId = (_curInteractionTextId + 1) % interactionTexts.Length;
        }
    }

    private void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i > interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }
}
