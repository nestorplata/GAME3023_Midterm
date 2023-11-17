using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public ObjectInteractibity ItemInSlotScript;
    public Vector2 AnchoredSlotPosition;
    public int TileType = TyleSignifiers.InventoryTile;

    void Start()
    {
        AnchoredSlotPosition = GetComponent<RectTransform>().anchoredPosition +
            transform.parent.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag && !ItemInSlotScript)
        {
            ItemInSlotScript = eventData.pointerDrag.GetComponent<ObjectInteractibity>();
            ItemInSlotScript.rectTransform.anchoredPosition = AnchoredSlotPosition;
            ItemInSlotScript.PreviousSlotScript = this;
        }
        TileOnDropFunctionality();


    }

    public void TileOnDropFunctionality()
    {
        switch (TileType)
        {
            case TyleSignifiers.CraftingTile:
                transform.GetComponentInParent<UI_Crafter>().UpdateItemsChar();
                break;

        }
    }
    public void TileOnBeginFunctionality()
    {
        UI_Crafter uI_CrafterScript = transform.GetComponentInParent<UI_Crafter>();
        switch (TileType)
        {
            case TyleSignifiers.OutputTile:
                uI_CrafterScript.ReduceItems();
                break;
            case TyleSignifiers.CraftingTile:
                uI_CrafterScript.UpdateItemsChar();
                break;
        }
    }


    public char GetHeldItemSignifier()
    {
        if (ItemInSlotScript)
        {
            return ItemInSlotScript.GetSignifier();

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

