using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player interactions.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    private const float MAX_INTERACTION_DISTANCE = 1.5f;

    public CanvasMng canvasMng;

    private Transform           _cameraTransform;
    private Interactive         _currentInteractive;
    private Interactive         currentRequirement;
    private bool                _requirementsInInventory;
    private List<Interactive>   _inventory;

    public List<int>           _inventorySize;
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
        _inventorySize              = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractive();
        CheckForScroll(0);
        CheckForInteraction();
        playerSize = FindObjectOfType<Player>().GetComponent<PlayerInteract>().playerSize;
    }

    /// <summary>
    /// Checks if player is looking at an interactive.
    /// </summary>
    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo, MAX_INTERACTION_DISTANCE))
        {
            Interactive interactive = hitInfo.transform.GetComponent<Interactive>();

            if (interactive == null || interactive.type == Interactive.InteractiveType.Indirect)
                ClearCurrentInteractive();

            else if (interactive != _currentInteractive)
                SetCurrentInteractive(interactive);
        }

        else
            ClearCurrentInteractive();
    }

    /// <summary>
    /// Shows the player a message according to if the player fills the interaction requirements and,
    /// sets the current interactive for future usage.
    /// </summary>
    /// <param name="interactive">Current interactive.</param>
    private void SetCurrentInteractive(Interactive interactive)
    {
        _currentInteractive = interactive;

        if (PlayerHasInteractionRequirements())
        {
            _requirementsInInventory = true;

            if(interactive.GetInteractionText() != "")
            {
                canvasMng.ShowInteractionPanel(interactive.GetInteractionText());
            }
        }
        else
        {
            _requirementsInInventory = false;

            if(interactive.requirementText != "")
            {
                canvasMng.ShowInteractionPanel(interactive.requirementText);
            }
        }
    }

    /// <summary>
    /// See if the player fills the requirements of the interaction.
    /// </summary>
    /// <returns>True if player fills the requirements.</returns>
    private bool PlayerHasInteractionRequirements()
    {
        if (_currentInteractive.numberOfUses == _currentInteractive.maximumUses && _currentInteractive.maximumUses != 0)
        {
            return false;
        }
        if (_currentInteractive.requirements == null)
        {
            return true;
        }

        if (_currentInteractive.orderedUsage && IsInInventory(_currentInteractive.requirements[_currentInteractive.numberOfUses]) && (_currentInteractive.itemSizeRestriction[_currentInteractive.numberOfUses] == _inventorySize[_inventory.IndexOf(_currentInteractive.requirements[_currentInteractive.numberOfUses])] || _currentInteractive.itemSizeRestriction.Length == 0) && _currentInteractive.limitedItemUsageAtOnce)
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
            else if (IsInInventory(_currentInteractive.requirements[i]) && !_currentInteractive.limitedItemUsageAtOnce && _currentInteractive.itemSizeRestriction[i] != 0 && _currentInteractive.itemSizeRestriction[i] != _inventorySize[_inventory.IndexOf(_currentInteractive.requirements[i])])
            {
                return false;
            }
            else if (_currentInteractive.limitedItemUsageAtOnce && IsInInventory(_currentInteractive.requirements[i]))
            {
                if(_currentInteractive.itemSizeRestriction.Length == 0)
                {
                    return true;
                }
                else if(_currentInteractive.itemSizeRestriction[i] == _inventorySize[_inventory.IndexOf(_currentInteractive.requirements[i])])
                {
                    return true;
                }
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

    /// <summary>
    /// Desabilitates interaction and text.
    /// </summary>
    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
        canvasMng.HideInteractionPanel();
    }

    /// <summary>
    /// Checks if player scrolls.
    /// </summary>
    /// <param name="inventoryChange">Sets the correct operation, according to inventory changes.</param>
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

    /// <summary>
    /// Checks for interaction.
    /// </summary>
    private void CheckForInteraction()
    {
        if (Input.GetMouseButtonDown(0) && _currentInteractive != null)
        {
            if (_currentInteractive.type == Interactive.InteractiveType.Pickable)
                PickCurrentInteractive();
            else if (_currentInteractive.requirementForSize != 0 && _currentInteractive.requirementForSize == playerSize && _requirementsInInventory)
                InteractWithCurrentInteractive();
            else if (_requirementsInInventory && _currentInteractive.requirementForSize == 0)
                InteractWithCurrentInteractive();
        }
    }

    /// <summary>
    /// Deactivates the objectand adds it to inventory.
    /// </summary>
    private void PickCurrentInteractive()
    {
        // Items activate when picked (Currently not used in vertical slice)
        if(_currentInteractive.useWhenPicked)
            _currentInteractive.Activate();
        // Items interact when picked.
        if(_currentInteractive.interactWhenPicked)
            _currentInteractive.Interact();

        _currentInteractive.gameObject.SetActive(false);
        AddToInventory(_currentInteractive);
    }

    /// <summary>
    /// Interacts with an object and removes the items from the inventory if the interaction consumes them.
    /// </summary>
    private void InteractWithCurrentInteractive()
    {

        if (_currentInteractive.orderedUsage && IsInInventory(_currentInteractive.requirements[_currentInteractive.numberOfUses])
        && (_currentInteractive.itemSizeRestriction[_currentInteractive.numberOfUses] 
        == _inventorySize[_inventory.IndexOf(_currentInteractive.requirements[_currentInteractive.numberOfUses])] || _currentInteractive.itemSizeRestriction.Length == 0) 
        && _currentInteractive.limitedItemUsageAtOnce && scrollSlot == _inventory.IndexOf(_currentInteractive.requirements[_currentInteractive.numberOfUses]))
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
            if (IsInInventory(_currentInteractive.requirements[i]) && _currentInteractive.limitedItemUsageAtOnce && (_currentInteractive.itemSizeRestriction.Length == 0 
            || _currentInteractive.itemSizeRestriction[i] == _inventorySize[_inventory.IndexOf(_currentInteractive.requirements[i])]) 
            && scrollSlot == _inventory.IndexOf(_currentInteractive.requirements[i]))
            {
                currentRequirement = _currentInteractive.requirements[i];

                if (currentRequirement.usedOnAnimation == true)
                {
                    currentRequirement.gameObject.SetActive(true);
                    currentRequirement.Interact();
                }

                if (currentRequirement.consumesItem == true)
                {
                    RemoveFromInventory(currentRequirement);
                }

                _currentInteractive.Interact();

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
        }

        if (!_currentInteractive.limitedItemUsageAtOnce)
        {
            _currentInteractive.Interact();
        }


        ClearCurrentInteractive();
    }

    /// <summary>
    /// Adds object to inventory and assigns it an icon.
    /// </summary>
    /// <param name="item">Item to add to inventory.</param>
    private void AddToInventory(Interactive item)
    {
        _inventory.Add(item);
        _inventorySize.Add(0);
        canvasMng.SetInventoryIcon(_inventory.Count - 1, item.icon);
        CheckForScroll(1);
    }

    /// <summary>
    /// Remove object and icon from inventory.
    /// </summary>
    /// <param name="item">Item to remove from inventory.</param>
    private void RemoveFromInventory(Interactive item)
    {
        _inventorySize.RemoveAt(_inventory.IndexOf(item));
        _inventory.Remove(item);

        canvasMng.ClearInventoryIcons();

        for (int i = 0; i < _inventory.Count; ++i)
            canvasMng.SetInventoryIcon(i, _inventory[i].icon);

        CheckForScroll(2);
    }

    //Vê se objeto está no inventário

    /// <summary>
    /// See if object is in inventory.
    /// </summary>
    /// <param name="item">Item to be looked for.</param>
    /// <returns>True if item is in inventory.</returns>
    private bool IsInInventory(Interactive item)
    {
        return _inventory.Contains(item);
    }

    /// <summary>
    /// Sets the selected icon.
    /// </summary>
    private void SelectInInventory()
    {
        canvasMng.SetSelectedIcon(scrollSlot, _inventory.Count);
    }
}