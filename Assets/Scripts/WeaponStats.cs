using UnityEngine;

[CreateAssetMenu(menuName = "Entity/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public new string name;
    public int level;
    public float damage;
    public int ammo;
    public float shootDelay;
    public float reloadDelay;
    public float heals;

    public WeaponStats nextLvl;
}

