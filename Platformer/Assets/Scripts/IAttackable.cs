using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void GetAttacked(Transform attackerOrigin, Transform attackZoneOrigin);

}
