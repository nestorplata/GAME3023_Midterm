using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "ScriptableRecipe", menuName = "ScriptableRecipe")]

public class ScriptableRecipe : ScriptableObject
{
    public ScriptableItem[] RecipeScriptableItems;
    public ScriptableItem ScriptableItemOutput;
    public int amount;

    private string recipe = "";

    public string GetRecipe()
    {

        return recipe;
    }
}
