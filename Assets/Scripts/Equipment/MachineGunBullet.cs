
public class MachineGunBullet : IBullet
{
    public BulletType bulletType { get; set; }
    public float damage { get; set; }

    public MachineGunBullet(BulletType bulletType, float damage)
    {
        this.bulletType = bulletType;
        this.damage = damage;
        if (bulletType.Equals(BulletType.bustBullet))
        {
            damage *= 2f;
        }
    }
}
