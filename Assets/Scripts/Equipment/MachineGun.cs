using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MachineGun : MonoBehaviour, IEquipment,IHeals,IGetDamage
{
    const SlotType SLOT_TYPE = SlotType.small;
    public SlotType SlotType { get => SLOT_TYPE; }
    public float heals { get; set; }
    public float maxHeals { get; set; }
    public Resist resist { get; set; }

    [SerializeField] WeaponStats _weaponStats;
    private int clip;
    public BulletType bulletType;
    private float _timeToNextShoot;

    private void Awake()
    {
        clip = _weaponStats.ammo;
        maxHeals = _weaponStats.heals;
        heals = maxHeals;
    }

    public void AddEffect(Ship ship)
    {

    }

    public void Fire(IGetDamage target)
    {
        if (_timeToNextShoot > 0 || heals < 0) return;
        IBullet bullet = new MachineGunBullet(bulletType, _weaponStats.damage);
        target.GetDamage(bullet);
        clip--;
        if(clip < 1)
        {
            _timeToNextShoot = _weaponStats.reloadDelay;
            StartCoroutine(ShootDelay(() => clip = _weaponStats.ammo));
        }
        _timeToNextShoot = _weaponStats.shootDelay;
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
}
