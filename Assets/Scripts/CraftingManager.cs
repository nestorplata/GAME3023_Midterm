using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private UI_Inventory UI_InventoryScript;
    [SerializeField] private UI_Crafter UI_Crafter;
    [SerializeField] private List<ScriptableItem> scriptableItems;
    [SerializeField] private List<ScriptableRecipe> scriptableRecipes;

    [SerializeField] private GameObject ItemPrefab;

    private Inventory inventory;
    
    // Start is called before the first frame update
    void Awake()
    {
        inventory = new Inventory();
        UI_InventoryScript.CreateHoldingTiles();
        UI_Crafter.CreateCraftingTiles();
        CreateItems();
    }

    public void CreateItems()
    {
        int i = 0;
        foreach (var properties in scriptableItems)
        {
            ObjectInteractibity Item = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
            Item.transform.position = UI_InventoryScript.GetTile(i).transform.position;
            Item.SetProperties(properties.sprite, properties.ObjectName, properties.amount, properties.CharSignifier);
            UI_InventoryScript.GetTileScript(i).ItemInSlotScript = Item;
            Item.PreviousSlotScript = UI_InventoryScript.GetTileScript(i);
            i++;
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
