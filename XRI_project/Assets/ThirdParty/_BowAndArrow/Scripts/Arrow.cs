using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000.0f;
    public Transform m_Tip = null;

    private Rigidbody m_RigidBody = null;

    private bool m_IsStopped = true;
    private Vector3 m_LastPosition = Vector3.zero;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_LastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // If we've hit something, don't update
        if (m_IsStopped)
            return;

        // Rotate with physics
        m_RigidBody.MoveRotation(Quaternion.LookRotation(m_RigidBody.linearVelocity, transform.up));

        // Collision check
        RaycastHit hit;
        if (Physics.Linecast(m_LastPosition, m_Tip.position, out hit))
            Stop(hit.collider,hit.point);

        // Store position for next frame
        m_LastPosition = m_Tip.position;
    }

    private void Stop(Collider hitObject,Vector3 hitpoint)
    {
        // Flag
        m_IsStopped = true;

        // Parent
        if(hitObject.attachedRigidbody)
            transform.parent = (hitObject.attachedRigidbody.transform);

        // Disable physics
        m_RigidBody.isKinematic = true;
        m_RigidBody.useGravity = false;

        // Check for damage
        if(hitObject.attachedRigidbody)
            CheckForDamage(hitObject.attachedRigidbody.gameObject, hitpoint);
    }

    private void CheckForDamage(GameObject hitObject, Vector3 hitpoint)
    {
        // Get all components
        MonoBehaviour[] behaviours = hitObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour is IDamageable)
            {
                // Do the thing
                IDamageable damgeable = (IDamageable)behaviour;
                damgeable.Damage(hitpoint);

                // Leave loop
                break;
            }
        }
    }

    public void Fire(float pullValue)
    {
        // Flag
        m_IsStopped = false;

        // Unparent
        transform.parent = null;

        // Physics
        m_RigidBody.isKinematic = false;
        m_RigidBody.useGravity = true;
        m_RigidBody.AddForce(transform.forward * (pullValue * m_Speed));

        // Timer
        Destroy(gameObject, 5.0f);
    }
}
