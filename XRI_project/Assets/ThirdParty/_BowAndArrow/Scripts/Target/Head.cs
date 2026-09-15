using UnityEngine;

public class Head : BodyPart, IDamageable
{
    public void Damage(int amount)
    {
        owner.TakeDamage(amount * 2);
    }
}
