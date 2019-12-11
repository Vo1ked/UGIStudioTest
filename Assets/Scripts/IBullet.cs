using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    energy,
    bustEnergy,
    bullet,
    bustBullet
}
public interface IBullet
{
    BulletType bulletType { get; set; }
    float damage { get; set; }
}
