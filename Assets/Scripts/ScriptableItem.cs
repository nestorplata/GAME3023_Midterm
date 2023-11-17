using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ScriptableItem", menuName = "ScriptableItem")]
public class ScriptableItem : ScriptableObject
{
    public Sprite sprite;
    public string ObjectName;

    [SerializeField]
    private char Signifier;


    public char GetSignifier()
    {
        return Signifier;
    }

}
