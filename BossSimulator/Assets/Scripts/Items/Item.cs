using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int radius;
    public needs affectedNeed;

    public enum needs
    {
        happiness, productivity, social
    }
}
