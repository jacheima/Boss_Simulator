using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData", order = 51)]

public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private string description;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int cost;

    [SerializeField]
    private int effectRadius;

    [SerializeField]
    private int happinessEffect;

    [SerializeField]
    private int productivityEffect;

    [SerializeField]
    private int socialEffect;

    [SerializeField]
    private int reliability;


    public string ItemName
    {
        get { return itemName; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

    public int Cost
    {
        get { return cost; }
    }

    public int EffectRadius
    {
        get { return effectRadius; }
    }

    public int HappinessEffect
    {
        get { return happinessEffect; }
    }

    public int ProductivityEffect
    {
        get { return productivityEffect; }
    }

    public int SociallEffect
    {
        get { return socialEffect; }
    }

    public int Reliability
    {
        get { return reliability; }
    }
}
