using UnityEngine;
using UnityEngine.UI;

public class CanvasMng : MonoBehaviour
{
    public GameObject   interactionPanel;
    public Text         interactionText;
    public RawImage[]   inventoryIcons;

    void Start()
    {
        HideInteractionPanel();
    }

    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
    }

    /// <summary>
    /// Shows interaction pannel.
    /// </summary>
    /// <param name="textMessage">Interaction text.</param>
    public void ShowInteractionPanel(string textMessage)
    {
        interactionText.text = textMessage;

        interactionPanel.SetActive(true);
    }

    //Cria os icons para objetos no inventário

    /// <summary>
    /// Sets the inventory Icons.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="icon">Icon Texture</param>
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