using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Health { private set; get; } = 10;
    public bool Alive { private set; get; } = true;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetupBodyParts();
    }

    private void SetupBodyParts()
    {
        BodyPart[] bodyParts = GetComponentsInChildren<BodyPart>();

        foreach (BodyPart bodyPart in bodyParts)
            bodyPart.Setup(this);
    }

    public void TakeDamage(int damage)
    {
        if (!Alive)
            return;

        Health -= damage;

        if (Health <= 0)
            Kill();
    }

    public void Kill()
    {
        Alive = false;
        animator.SetBool("Dance", true);

        // StartCoroutine(Reset());
    }

    /*
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(5.0f);

        Alive = true;
        Health = 10;
        animator.SetBool("Dance", false);
    }
    */
}
