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

    [SerializeField]
    private int TileType = TyleSignifiers.InventoryTile;
    
    void Start()
    {
        AnchoredSlotPosition = GetComponent<RectTransform>().anchoredPosition +
            transform.parent.GetComponent<RectTransform>().anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag && !ItemInSlotScript && 
            TileType != TyleSignifiers.OutputTile)
        {
            ItemInSlotScript = eventData.pointerDrag.GetComponent<ObjectInteractibity>();
            ItemInSlotScript.rectTransform.anchoredPosition = AnchoredSlotPosition;
            ItemInSlotScript.PreviousSlotScript.SwitchOnEndDrag();
            SwitchOnDrop();
            ItemInSlotScript.PreviousSlotScript = this;
        }
    }

    public void SwitchOnBeginDrag()
    {
        UI_Crafter UI_CrafterScript = transform.GetComponentInParent<UI_Crafter>();
        switch (TileType)
        {
            //case TyleSignifiers.OutputTile:
            //    UI_CrafterScript.ReduceItems();
            //    break;
            case TyleSignifiers.CraftingTile:
                UI_CrafterScript.UpdateItemsChar();
                break;
        }
    }

    public void SwitchOnDrop()
    {
        UI_Crafter UI_CrafterScript = transform.GetComponentInParent<UI_Crafter>();
        switch (TileType)
        {

            case TyleSignifiers.CraftingTile:
                UI_CrafterScript.UpdateItemsChar();
                break;
        }
    }

    public void SwitchOnEndDrag()
    {
        UI_Crafter UI_CrafterScript = transform.GetComponentInParent<UI_Crafter>();
        switch (TileType)
        {
            case TyleSignifiers.OutputTile:
                UI_CrafterScript.ReduceItems();
                break;
            case TyleSignifiers.CraftingTile:
                transform.GetComponentInParent<UI_Crafter>().UpdateItemsChar();
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

