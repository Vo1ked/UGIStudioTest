using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicShip : Ship
{
    [SerializeField]private ShipStats _shipStats;
    public new string name  = "Classic Ship";
    public override ShipStats stats { get => _shipStats;  set => _shipStats = value; }
    public override void GetDamage(IBullet damage)
    {
        heals = heals - damage.damage + GetResists(damage);
    }

    public override void Upgrate()
    {
        if (stats.nextLvl == null) return;
        stats = stats.nextLvl;
        InitStats();
    }

    private void Awake()
    {
        InitStats();
    }

}
