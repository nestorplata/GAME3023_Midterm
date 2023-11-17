using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private UI_Inventory UI_InventoryScript;
    [SerializeField] private UI_Crafter UI_Crafter;
    [SerializeField] private List<ScriptableRecipe> scriptableRecipes;
    [SerializeField] private ItemCreationStruct[] StartingItems;


    [SerializeField] private GameObject ItemPrefab;
    [System.Serializable]
    public struct ItemCreationStruct
    {
        public ScriptableItem scriptableItem;
        public int amount;
    }

    //private string<List>
    
    // Start is called before the first frame update
    void Awake()
    {
        UI_InventoryScript.CreateHoldingTiles();
        UI_Crafter.CreateCraftingTiles();
        CreateStartingItems();
    }

    public void CreateStartingItems()
    {
        int i = 0;
        foreach (var ItemStruct in StartingItems)
        {
            CreateItem(ItemStruct, UI_InventoryScript.GetTileScript(i));
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

        if (OutputTile.ItemInSlotScript)
        {
            Destroy(OutputTile.ItemInSlotScript.gameObject);

        }
        foreach (var recipeScript in scriptableRecipes)
        {
            if (CraftedRecipe == GetRecipe(recipeScript))
            {
                CreateItem(recipeScript, OutputTile);
                break;
            }
        }

    }


    public void CreateItem(ItemCreationStruct ItemStruct, ItemSlot Tile)
    {
        ScriptableItem ScriptableItem= ItemStruct.scriptableItem;
        ObjectInteractibity ItemScript = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
        ItemScript.transform.position = Tile.transform.position;
        ItemScript.SetProperties(ScriptableItem.sprite, ScriptableItem.ObjectName, ItemStruct.amount, ScriptableItem.GetSignifier());
        Tile.ItemInSlotScript = ItemScript;
        ItemScript.PreviousSlotScript = Tile;
    }

    public void CreateItem(ScriptableRecipe ScriptableRecipe, ItemSlot Tile)
    {
        ScriptableItem ScriptableItem = ScriptableRecipe.ScriptableItemOutput;
        ObjectInteractibity ItemScript = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
        ItemScript.transform.position = Tile.transform.position;
        ItemScript.SetProperties(ScriptableItem.sprite, ScriptableItem.ObjectName, ScriptableRecipe.amount, ScriptableItem.GetSignifier());
        Tile.ItemInSlotScript = ItemScript;
        ItemScript.PreviousSlotScript = Tile;
    }

    public string GetRecipe(ScriptableRecipe scriptableRecipe)
    {
        string recipe ="";
        foreach (ScriptableItem recipeItems in scriptableRecipe.RecipeScriptableItems)
        {
            recipe = recipe + recipeItems.GetSignifier();
        }
        return recipe;
    }
}
