using UnityEngine;

[CreateAssetMenu(menuName = "Entity/ShieldStats")]
public class ShieldStats : ScriptableObject
{
    public new string name;
    public int level;
    public float damageResist;
    public float heals;

    public ShieldStats nextLvl;
}

