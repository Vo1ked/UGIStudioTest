using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlazmaCanon : MonoBehaviour, IEquipment, IHeals, IGetDamage,IUpgrate
{
    const SlotType SLOT_TYPE = SlotType.small;
    public SlotType SlotType { get => SLOT_TYPE; }
    public float heals { get; set; }
    public float maxHeals { get; set; }
    public Resist resist { get; set; }

    [SerializeField] WeaponStats _weaponStats;
    public WeaponStats currentWeaponStats { get; private set; }
    private int clip;
    public BulletType bulletType;
    private float _timeToNextShoot;

    private void Awake()
    {
        InitStats();
    }

    private void InitStats()
    {
        clip = currentWeaponStats.ammo;
        maxHeals = currentWeaponStats.heals;
        heals = maxHeals;
    }

    public void AddEffect(Ship ship)
    {

    }

    public void Fire(IGetDamage target)
    {
        if (_timeToNextShoot > 0 || heals < 0) return;
        IBullet bullet = new LazerBullet(bulletType, currentWeaponStats.damage);
        target.GetDamage(bullet);
        clip--;
        if (clip < 1)
        {
            _timeToNextShoot = currentWeaponStats.reloadDelay;
            StartCoroutine(ShootDelay(() => clip = currentWeaponStats.ammo));
        }
        _timeToNextShoot = currentWeaponStats.shootDelay;
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay(System.Action OnComplite = null)
    {
        while (_timeToNextShoot > 0)
        {
            yield return null;
            _timeToNextShoot -= Time.deltaTime;
        }
        OnComplite?.Invoke();
    }
    public void RemoveEffect(Ship ship)
    {

    }

    public float GetResists(IBullet bullet)
    {
        if (resist == null) return 0;
        IEnumerable<float> list = resist.GetInvocationList().Select(x => (float)x.DynamicInvoke(bullet));
        return list.Sum();
    }

    public void GetDamage(IBullet damage)
    {
        heals = heals - damage.damage + GetResists(damage);
    }

    public void Upgrate()
    {
        if (currentWeaponStats.nextLvl == null) return;
        currentWeaponStats = currentWeaponStats.nextLvl;
        InitStats();
    }
}
