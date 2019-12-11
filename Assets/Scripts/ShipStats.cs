using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/ShipStats")]
public class ShipStats : ScriptableObject,ICloneable<ShipStats>
{
    public new string name;
    public int level;
    public float speed;
    public float heals;
    public List<Slot> slots = new List<Slot>();
    public ShipStats nextLvl;

    public ShipStats Clone()
    {
        ShipStats clone = new ShipStats
        {
            name = name,
            level = level,
            speed = speed,
            heals = heals,
            slots = new List<Slot>(slots),
            nextLvl = nextLvl
        };
        return clone;
    }
}
