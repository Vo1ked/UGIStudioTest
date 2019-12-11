using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultyCombaneEquipment : MonoBehaviour, IEquipment, IHeals, IGetDamage, IUpgrate
{
    [SerializeField] WeaponStats _machineGunStats;
    [SerializeField] WeaponStats _lazerCanonStats;
    [SerializeField] ShieldStats _shildStats;
    public WeaponStats currentMachineGunStats { get; private set; }
    public WeaponStats currentLazerCanonStats { get; private set; }
    public ShieldStats currentShildStats { get; private set; }
    public float heals { get; set; }
    public float maxHeals { get; set; }
    public Resist resist { get; set; }

    const SlotType SLOT_TYPE = SlotType.big;
    public SlotType SlotType => SLOT_TYPE;

    public int machineGunClip;
    public BulletType bulletType;
    public int lazerClip;
    public BulletType lazerBulletType;
    private float _timeToNextMachineGunShoot;
    private float _timeToNextLazerShoot;


    public void AddEffect(Ship ship)
    {
        ship.resist += AddShield;
    }

    private float AddShield(IBullet bullet)
    {
        if (bullet.bulletType == BulletType.bullet || bullet.bulletType == BulletType.bustBullet) return 0;
        return bullet.damage * currentShildStats.damageResist;
    }

    public void Fire(IGetDamage target)
    {
        if (heals < 0) return;
        if (_timeToNextMachineGunShoot < 0.01f)
        {
            Shoot(target);
            if (machineGunClip < 1)
            {
                _timeToNextMachineGunShoot = currentMachineGunStats.reloadDelay;
                StartCoroutine(ShootDelay(_timeToNextMachineGunShoot, () => machineGunClip = currentMachineGunStats.ammo));
            }
            _timeToNextMachineGunShoot = currentMachineGunStats.shootDelay;
            StartCoroutine(ShootDelay(_timeToNextMachineGunShoot));
        }

        if (_timeToNextLazerShoot < 0.01f)
        {
            LazerShoot(target);
            if (lazerClip < 1)
            {
                _timeToNextLazerShoot = currentLazerCanonStats.reloadDelay;
                StartCoroutine(ShootDelay(_timeToNextLazerShoot, () => lazerClip = currentLazerCanonStats.ammo));
            }
            _timeToNextLazerShoot = currentLazerCanonStats.shootDelay;
        }
        StartCoroutine(ShootDelay(_timeToNextLazerShoot));
    }

    private void Shoot(IGetDamage target)
    {
        IBullet bullet = new MachineGunBullet(bulletType, currentMachineGunStats.damage);
        target.GetDamage(bullet);
        machineGunClip--;
    }

    private void LazerShoot(IGetDamage target)
    {
        IBullet bullet = new LazerBullet(lazerBulletType, currentLazerCanonStats.damage);
        target.GetDamage(bullet);
        lazerClip--;
    }

    public void GetDamage(IBullet damage)
    {
        heals = heals - damage.damage + GetResists(damage);
    }

    public float GetResists(IBullet bullet)
    {
        if (resist == null) return 0;
        IEnumerable<float> list = resist.GetInvocationList().Select(x => (float)x.DynamicInvoke(bullet));
        return list.Sum();
    }

    private IEnumerator ShootDelay(float value, System.Action OnComplite = null)
    {
        while (value > 0)
        {
            yield return null;
            value -= Time.deltaTime;
        }
        OnComplite?.Invoke();
    }

    public void RemoveEffect(Ship ship)
    {
        ship.resist -= AddShield;

    }

    public void Upgrate()
    {
        if (currentMachineGunStats.nextLvl == null) return;
        currentMachineGunStats = currentMachineGunStats.nextLvl;
        currentLazerCanonStats = currentLazerCanonStats.nextLvl;
        currentShildStats = currentShildStats.nextLvl;
        InitStats();
    }

    private void InitStats()
    {
        machineGunClip = currentMachineGunStats.ammo;
        lazerClip = currentLazerCanonStats.ammo;
        maxHeals = currentMachineGunStats.heals + currentLazerCanonStats.heals + currentShildStats.heals;
        heals = maxHeals;
    }
}
