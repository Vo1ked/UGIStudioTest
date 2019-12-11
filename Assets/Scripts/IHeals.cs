using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float Resist(IBullet bullet);

public interface IHeals
{
    float heals { get; set; }
    float maxHeals { get; set; }
    Resist resist { get; set; }
    float GetResists(IBullet bullet);
}
