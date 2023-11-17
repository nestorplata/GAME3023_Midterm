using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableRecipe", menuName = "ScriptableRecipe")]

public class ScriptableRecipe : ScriptableObject
{
    public string recipe;
    public ScriptableItem Output;

}
