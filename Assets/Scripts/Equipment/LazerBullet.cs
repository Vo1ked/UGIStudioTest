
public class LazerBullet : IBullet
{
    public BulletType bulletType { get; set; }
    public float damage { get; set; }

    public LazerBullet(BulletType bulletType, float damage)
    {
        this.bulletType = bulletType;
        this.damage = damage;
        if (bulletType.Equals(BulletType.bustEnergy))
        {
            damage *= 2f;
        }
    }
}
