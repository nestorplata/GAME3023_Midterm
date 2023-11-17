using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class UI_Crafter : MonoBehaviour
{

    [SerializeField] private GameObject StartingTile;
    [SerializeField] private ItemSlot OutputTileScript;

    private List<GameObject> slots = new List<GameObject>();
    private char[] CraftingGrid = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    // Start is called before the first frame update


    public void CreateCraftingTiles()
    {
        Vector2 Position = StartingTile.transform.position;
        for (int i = 0; i < 9; i++)
        {
            GameObject Holder = Instantiate(StartingTile, new Vector2(), transform.rotation);
            Holder.transform.SetParent(transform);
            Holder.transform.position = Position;
            slots.Add(Holder);

            Position.x += 63;
            if (Position.x > (2 * 63 + StartingTile.transform.position.x))
            {
                Position.x = StartingTile.transform.position.x;
                Position.y -= 65;
            }
        }
        Destroy(StartingTile);
    }

    public void UpdateItemsChar()
    {
        for (int i = 0;i < CraftingGrid.Count(); i++)
        {
            CraftingGrid[i] = slots[i].GetComponent<ItemSlot>().GetHeldItemSignifier();
        }
        GetComponentInParent<CraftingManager>().CheckForRecipe(CraftingGrid, OutputTileScript);
    }

    public void ReduceItems()
    {
        foreach (GameObject slot in slots)
        {
            ItemSlot SlotScript=slot.GetComponent<ItemSlot>();
            if (SlotScript.ItemInSlotScript!=null)
            {
                SlotScript.ItemInSlotScript.ReduceAmount();
            }
        }
        UpdateItemsChar();
    }


}
