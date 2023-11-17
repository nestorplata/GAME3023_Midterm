using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ObjectInteractibity : MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler
{
     //private Canvas canvas;

    public ItemSlot CurrentSlotScript;

    public RectTransform rectTransform;
    
    private Image Image;
    private CanvasGroup canvasGroup;
    private Text TextAmount;

    private int cuantity;
    private char signifier = ' ';

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Image = GetComponent < Image > ();
        canvasGroup = GetComponent<CanvasGroup>();
        TextAmount = GetComponentInChildren<Text>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        CurrentSlotScript.CurrentItem = null; //Removes Itself from the previous tile
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)       //OnDrop on ItemSlot happens before On EndDrag,

    {

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (CurrentSlotScript.AnchoredSlotPosition != rectTransform.anchoredPosition)        //if there has been a change on current Slot, change positions
        {
            rectTransform.anchoredPosition = CurrentSlotScript.AnchoredSlotPosition;
        }
    }

    public void SetProperties(Sprite sprite, string name, int cuanity, char signifier)
    {
        Image.sprite = sprite;
        gameObject.name = name;
        this.cuantity = cuanity;
        TextAmount.text = cuanity.ToString();
        this.signifier = signifier;
    }

    public char GetSignifier()
    {
        return signifier;
    }

    public void ReduceAmount()
    {
        cuantity--;
        TextAmount.text = cuantity.ToString();
        if(cuantity <=0)
        {
            CurrentSlotScript.CurrentItem = null;
            Destroy(gameObject);
        }
    }


}
