using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    SlotType SlotType{get;}
    void Fire(IGetDamage target);
    void AddEffect(Ship ship);
    void RemoveEffect(Ship ship);
}
