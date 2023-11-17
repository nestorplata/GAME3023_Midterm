using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ScriptableItem", menuName = "ScriptableItem")]
public class ScriptableItem : ScriptableObject
{
    public Sprite sprite;
    public string ObjectName;
    public char CharSignifier;
    public int amount;

}
