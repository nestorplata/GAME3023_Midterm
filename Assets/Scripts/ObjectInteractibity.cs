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

    public ItemSlot PreviousSlotScript;

    public RectTransform rectTransform;
    
    private Image Image;
    private CanvasGroup canvasGroup;
    private Text TextAmount;

    private char signifier = 'E';

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Image = GetComponent < Image > ();
        canvasGroup = GetComponent<CanvasGroup>();
        TextAmount = GetComponentInChildren<Text>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        PreviousSlotScript.ItemInSlotScript = null;
        PreviousSlotScript.TypeSwitchFunctionality();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        Debug.Log(signifier);

    }
    public void OnDrag(PointerEventData eventData)
    {
        
        rectTransform.anchoredPosition += eventData.delta;
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(PreviousSlotScript.AnchoredSlotPosition!= rectTransform.anchoredPosition)
        {
            rectTransform.anchoredPosition = PreviousSlotScript.AnchoredSlotPosition;
        }
    }

    public void SetProperties(Sprite sprite, string name, int cuanity, char signifier)
    {
        Image.sprite = sprite;
        gameObject.name = name;
        TextAmount.text = cuanity.ToString();
        this.signifier = signifier;
    }

    public char GetSignifier()
    {
        return signifier;
    }
}
