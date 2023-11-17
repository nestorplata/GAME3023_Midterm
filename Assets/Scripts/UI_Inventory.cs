using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{


    [SerializeField] private GameObject StartingTile;


    private List<GameObject> slots = new List<GameObject>();

    public void CreateHoldingTiles()
    {
        Vector2 Position = StartingTile.transform.position;
        for (int i = 0; i < 36; i++)
        {
            GameObject Holder = Instantiate(StartingTile, new Vector2(), transform.rotation);
            Holder.transform.SetParent(transform);
            Holder.transform.position = Position;
            slots.Add(Holder);

            Position.x += 63;
            if(Position.x>(8*63+ StartingTile.transform.position.x))
            {
                Position.x = StartingTile.transform.position.x;

                if(Position.y== StartingTile.transform.position.y)
                {
                    Position.y += 83;
                }
                else
                {
                    Position.y += 65;
                }
            }
        }
        Destroy(StartingTile);
    }
    public GameObject GetTile(int i)
    {
        return slots[i];
    }

    public ItemSlot GetTileScript(int i)
    {
        return GetTile(i).GetComponent<ItemSlot>();
    }


    //PreviousSlot = rectTransform.anchoredPosition;


}
