using UnityEngine;

public class Interactive : MonoBehaviour
{
    public enum InteractiveType {Pickable, One_Use, Multi_Uses, Indirect};
    public InteractiveType type;
    public string          interactionText;
    //public string        interactiveDialogue;
    //public string        interactionDialogue;
    //public string        positionDialogue; diálogos que são triggered após o jogador pisar blocos interativos invisiveis no chão
    //public int           dialogueTimer;
    public string          requirementText;
    public bool            consumesItem;
    public Interactive[]   interactionChain;
    public Texture         icon;
    public Interactive[]   requirements;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_anim != null)
            _anim.SetTrigger("Interact");

        ProcessInteractionChain();

        if (type == InteractiveType.One_Use)
            GetComponent<Collider>().enabled = false;
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
