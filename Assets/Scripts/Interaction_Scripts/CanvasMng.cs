using UnityEngine;
using UnityEngine.UI;

public class CanvasMng : MonoBehaviour
{
    public GameObject   interactionPanel;
    public GameObject   dialoguePanel;
    public Text         interactionText;
    public Text         dialogueText;
    public RawImage[]   inventoryIcons;

    void Start()
    {
        HideInteractionPanel();
    }

    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
    }
    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    //Legenda de interatividade

    /// <summary>
    /// 
    /// </summary>
    /// <param name="textMessage"></param>
    public void ShowInteractionPanel(string textMessage)
    {
        interactionText.text = textMessage;

        interactionPanel.SetActive(true);
    }

    //Legenda de diálogo
    //Coordenar no Player Interact com soundMng para o audioClip acontecer em simultâneo
    public void ShowDialoguePanel(string textDialogue)
    {
        dialogueText.text = textDialogue;

        dialoguePanel.SetActive(true);
    }

    //Cria os icons para objetos no inventário
    public void SetInventoryIcon(int i, Texture icon)
    {
        inventoryIcons[i].texture   = icon;
        inventoryIcons[i].color = Color.clear;
    }

    public void SetSelectedIcon(int n, int inventoryCount)
    {
        for (int i = 0; i < inventoryCount; ++i)
            inventoryIcons[i].color = Color.gray;
        inventoryIcons[n].color = Color.white;
    }


    //Apaga os icons para objetos no inventário (para não ficar lá imagem quando se apaga um objeto)
    public void ClearInventoryIcons()
    {
        for (int i = 0; i < inventoryIcons.Length; ++i)
        {
            inventoryIcons[i].texture   = null;
            inventoryIcons[i].color = Color.clear;
        }
    }

}