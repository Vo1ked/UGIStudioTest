using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    small,
    medium,
    big
}
[System.Serializable]
public class Slot
{
    [HideInInspector]
    public string name = "Slot";
    public SlotType slotType;
    public IEquipment equipment { get; set; }
    public virtual bool CanEquipt(IEquipment equipment)
    {
        return equipment.SlotType == slotType;
    }
}
