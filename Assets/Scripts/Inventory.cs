using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> itemList;
    public Inventory()
    {
        //itemList = new List<Item>();
        //AddItem(new Item { ItemType = "coal", amount =1 });

    }

    public void AddItem(GameObject item)
    {
        itemList.Add(item);
    }

    public List<GameObject> GetItemList()
    {
        return itemList;
    }

}
