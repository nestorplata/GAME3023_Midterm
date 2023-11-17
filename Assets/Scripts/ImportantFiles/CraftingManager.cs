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

    public void CreateStartingItems()// Starting Items based of ItemCreationStruct and binds them to a tile
    {
        int i = 0;
        foreach (var ItemStruct in StartingItems)
        {
            CreateItem(ItemStruct, UI_InventoryScript.GetTileScript(i));
            i++;
        }
    }

    public void CheckForRecipe(char[] grid, ItemSlot OutputTile) //Checks Weather the grid Contains a recipe for an Item 
    {
        string CraftedRecipe = "";
        for (var i = 0; i < grid.Length; i++)
        {
            CraftedRecipe = CraftedRecipe + grid[i];
        }
        CraftedRecipe = CraftedRecipe.Trim();

        if (OutputTile.CurrentItem)
        {
            Destroy(OutputTile.CurrentItem.gameObject);

        }
        foreach (var recipeScript in scriptableRecipes)//Creates an object if a recipe is found
        {
            if (CraftedRecipe == GetRecipe(recipeScript))
            {
                CreateItem(recipeScript, OutputTile);
                break;
            }
        }

    }


    public void CreateItem(ItemCreationStruct ItemStruct, ItemSlot Tile) //Creates Item off a Struct and binds it to the Output Tile
    {
        ScriptableItem ScriptableItem= ItemStruct.scriptableItem;
        ObjectInteractibity ItemScript = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
        ItemScript.transform.position = Tile.transform.position;
        ItemScript.SetProperties(ScriptableItem.sprite, ScriptableItem.ObjectName, ItemStruct.amount, ScriptableItem.GetSignifier());
        Tile.CurrentItem = ItemScript;
        ItemScript.CurrentSlotScript = Tile;
    }

    public void CreateItem(ScriptableRecipe ScriptableRecipe, ItemSlot Tile)//Creates Item off a scriptable recipe and binds it to the Output Tile
    {
        ScriptableItem ScriptableItem = ScriptableRecipe.ScriptableItemOutput;
        ObjectInteractibity ItemScript = Instantiate(ItemPrefab, transform).GetComponent<ObjectInteractibity>();
        ItemScript.transform.position = Tile.transform.position;
        ItemScript.SetProperties(ScriptableItem.sprite, ScriptableItem.ObjectName, ScriptableRecipe.amount, ScriptableItem.GetSignifier());
        Tile.CurrentItem = ItemScript;
        ItemScript.CurrentSlotScript = Tile;
    }

    public string GetRecipe(ScriptableRecipe scriptableRecipe) // scriptable Recipes are array organized Scriptable Items with one Item output
    {
        string recipe ="";
        foreach (ScriptableItem recipeItems in scriptableRecipe.RecipeScriptableItems)
        {
            recipe = recipe + recipeItems.GetSignifier();
        }
        return recipe;
    }
}
