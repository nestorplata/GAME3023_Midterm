using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        CreateStartingItems();
    }

    public void CreateStartingItems()
    {
        int i = 0;
        foreach (var ScritableItem in scriptableItems)
        {
            CreateItem(ScritableItem, UI_InventoryScript.GetTileScript(i));
            i++;
        }
    }

    public void CheckForRecipe(char[] grid, ItemSlot OutputTile)
    {
        string CraftedRecipe = "";
        for (var i = 0; i < grid.Length; i++)
        {
            CraftedRecipe = CraftedRecipe + grid[i];
        }
        CraftedRecipe = CraftedRecipe.Trim();
        Debug.Log(CraftedRecipe);

        if (OutputTile.ItemInSlotScript)
        {
            Destroy(OutputTile.ItemInSlotScript.gameObject);

        }
        foreach (var recipeScript in scriptableRecipes)
        {
            if(CraftedRecipe == recipeScript.recipe)
            {
                CreateItem(recipeScript.Output, OutputTile);
                break;
            }
        }

    }

    public void CreateItem(ScriptableItem ScriptableItem, ItemSlot Tile)
    {
        ObjectInteractibity ItemScript = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
        ItemScript.transform.position = Tile.transform.position;
        ItemScript.SetProperties(ScriptableItem.sprite, ScriptableItem.ObjectName, ScriptableItem.amount, ScriptableItem.GetSignifier());
        Tile.ItemInSlotScript = ItemScript;
        ItemScript.PreviousSlotScript = Tile;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
