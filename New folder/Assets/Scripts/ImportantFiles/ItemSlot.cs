using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public ObjectInteractibity CurrentItem;
    public Vector2 AnchoredSlotPosition;

    [SerializeField]
    private int TileType = TyleSignifiers.InventoryTile;
    
    void Start()     //Sets a transferable global tile position 
    {
        AnchoredSlotPosition = GetComponent<RectTransform>().anchoredPosition +
            transform.parent.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            ObjectInteractibity ItemDroped = eventData.pointerDrag.GetComponent<ObjectInteractibity>(); 
            ItemSlot DropedCurrentSlot = ItemDroped.CurrentSlotScript;

            if (DropedCurrentSlot.TileType == TyleSignifiers.OutputTile
                && CurrentItem)
            {
                //Destroys if ones tries to wrongly allocate a just created object
                Destroy(ItemDroped.gameObject);

            } //enables Object movement if Tile is empty and is not an Output Tile
            else if (!CurrentItem &&
                TileType != TyleSignifiers.OutputTile)
            {
                DropedCurrentSlot.UpdateGrid();

                //sets Current Item To Dropped Item
                CurrentItem = ItemDroped;
                CurrentItem.CurrentSlotScript = this;
                CurrentItem.rectTransform.anchoredPosition = AnchoredSlotPosition;

            }
            UpdateGrid();
        }
    }

    //Checks weather the previus or new tile is a crafting Tile
    public void UpdateGrid()
    {
        UI_Crafter uI_Crafter = transform.GetComponentInParent<UI_Crafter>();
        switch(TileType)
        {
            case TyleSignifiers.CraftingTile:
                uI_Crafter.UpdateItemsChar();       //if previous or new tile is Crafting, Consume Grid Objects Respectively
                break;
            case TyleSignifiers.OutputTile:         //if previous tile is Output, Consume Grid Objects Respectively  
                uI_Crafter.ReduceItems();
                break;

        }
    }


public char GetHeldItemSignifier()
    {
        if (CurrentItem)
        {
            return CurrentItem.GetSignifier();
        }
        else
        {
            return ' ';
        }

    }
}


static public class TyleSignifiers
{
    public const int InventoryTile = 0;
    public const int CraftingTile = 1;
    public const int OutputTile = 2;
}

