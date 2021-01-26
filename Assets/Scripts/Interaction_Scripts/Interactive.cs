using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactive : MonoBehaviour
{
    public int loadLevel = 0;
    public enum InteractiveType {Pickable, Direct, Indirect};
    public InteractiveType type;
    public bool             isActive;
    //public bool             multiPickable;
    public bool             consumesItem, usedOnAnimation, useWhenPicked, interactWhenPicked, orderedUsage, limitedItemUsageAtOnce;
    public string[]         interactionTexts;
    public int              requirementForSize, maximumUses;

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

    private int            playerSize;
    private int            _curInteractionTextId, audioAux;

    [HideInInspector]
    public int             numberOfUses;

    public Interactive[]   activationChain, deactivationChain, interactionChain, requirements;
    public Texture         icon;
    public int[]           itemSizeRestriction;
    private AudioSource    interactionAudio;
    public  AudioClip[]    interactionAudioClips;
    public  AudioClip      activationAudioClip;

    private Animator        _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        interactionAudio = GetComponent<AudioSource>();
        _curInteractionTextId  = 0;
        requirementForSize = 0;
        audioAux  = 0;
        numberOfUses = 0;

    }

    public string GetInteractionText()
    {
        if(interactionTexts.Length > 0)
        {
            return interactionTexts[_curInteractionTextId];
        }
        else
        {
            return "";
        }
    }

    //Só é chamada por interações que ativam outras
    public void Activate()
    {
        isActive = true;

        if (_anim != null)
            _anim.SetTrigger("Activate");

        if(interactionAudio != null && activationAudioClip != null)
        {
            interactionAudio.PlayOneShot(activationAudioClip);
        }
    }

    private void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; ++i)
                activationChain[i].Activate();
        }
    }

    public void Deactivate()
    {
        isActive = false;

        if (_anim != null)
            _anim.SetTrigger("Deactivate");
    }

    private void ProcessDeactivationChain()
    {
        if (deactivationChain != null)
        {
            for (int i = 0; i < deactivationChain.Length; ++i)
                deactivationChain[i].Deactivate();
        }
    }

    public void Interact()
    {
        playerSize = FindObjectOfType<Player>().GetComponent<PlayerInteract>().playerSize;

        if (loadLevel == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (loadLevel == -1)
        {
            SceneManager.LoadScene("MainHub");
        }

        if (loadLevel == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }

        if (loadLevel == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        if (_anim != null)
        {
            _anim.SetTrigger("Interact");
            _anim.SetInteger("Size", playerSize);
        }

        
        if(interactionAudio != null && interactionAudioClips != null)
        {
            interactionAudio.PlayOneShot(interactionAudioClips[audioAux]);
            audioAux += 1;
        }

        if (interactionAudioClips != null)
        {
            if (audioAux == interactionAudioClips.Length)
            {
                audioAux = 0;
            }
        }
        

        

        if (isActive)
        {
            ProcessDeactivationChain();
            ProcessActivationChain();
            ProcessInteractionChain();

            numberOfUses += 1;

            if (maximumUses == numberOfUses || type == InteractiveType.Pickable)
                GetComponent<BoxCollider>().enabled = false;
            else if ((maximumUses != numberOfUses) && (interactionTexts.Length != 0))
                _curInteractionTextId = (_curInteractionTextId + 1) % interactionTexts.Length;
        }
    }

    private void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }
}
