using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Ship : MonoBehaviour, IUpgrate, IGetDamage,IHeals
{
    public new string name = "Ship";
    public virtual float heals { get; set; }
    public virtual float maxHeals { get; set; }
    public virtual ShipStats stats { get; set; }
    protected  ShipStats _currentStatsValue;
    public Dictionary<string, object> customValues = new Dictionary<string, object>();
    public virtual ShipStats _currentStats
    {
        get
        {
            if(_currentStatsValue == null)
            {
                Debug.Log("Init Stats before change !");
            }
            return _currentStatsValue;
        }
    }
    public Resist resist { get; set; }

    public float GetResists(IBullet bullet)
    {
        if (resist == null) return 0;
        var list = resist.GetInvocationList().Select(x => (float)x.DynamicInvoke(bullet));
        return list.Sum();
    }

    public abstract void GetDamage(IBullet damage);
    public abstract void Upgrate();

    protected virtual void InitStats()
    {
        _currentStatsValue = stats.Clone();
        _currentStatsValue.slots.ForEach(x =>
        {
            x.equipment?.AddEffect(this);
        });
        maxHeals = _currentStats.heals;
        heals = maxHeals;
        

    }
}
