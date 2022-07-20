using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider col)
    {
        IAttackable attackable = col.GetComponent<IAttackable>(); // SUPER useful. You can get any component via an interface. 
        if (attackable != null)
            attackable.GetAttacked(player, this.transform);
    }
}
