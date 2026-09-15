using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    protected Target owner = null;

    public void Setup(Target newOwner)
    {
        owner = newOwner;
    }

    public void Damage(Vector3 amount)
    {
        owner.TakeDamage(1);
    }
}
