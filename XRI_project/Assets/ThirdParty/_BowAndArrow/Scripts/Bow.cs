using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject m_ArrowPrefab = null;

    [Header("Bow")]
    public float m_GrabThreshold = 0.15f;
    public Transform m_Socket = null;
    public Transform m_Start = null;
    public Transform m_End = null;

    private Transform m_PullingHand = null;
    private Arrow m_CurrentArrow = null;
    private Animator m_Animator = null;

    private float m_PullValue = 0.0f;

    public float pullMultiplier = 1;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow(0.0f));
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        // Wait
        yield return new WaitForSeconds(waitTime);

        // Create, and child
        GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket);

        // Orient
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;

        // Set
        m_CurrentArrow = arrowObject.GetComponent<Arrow>();
    }

    private void Update()
    {
        // Only update if pulling, and we have an arrow
        if (!m_PullingHand || !m_CurrentArrow)
            return;

        // Pull value
        m_PullValue = CalculatePull(m_PullingHand);
        m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f);

        // Apply to animator
        m_Animator.SetFloat("Blend", m_PullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        // Direction, and size
        Vector3 direction = m_End.position - m_Start.position;
        float magnitude = direction.magnitude;

        // Difference
        direction.Normalize();
        Vector3 difference = pullHand.position - m_Start.position;

        // Right angle, value of 0 -  1
        return Vector3.Dot(difference, direction) / magnitude;
    }

    public void Pull(Transform hand)
    {
        // Simple distance check 
        float distance = Vector3.Distance(hand.position, m_Start.position);

        if (distance > m_GrabThreshold)
            return;

        // Set
        m_PullingHand = hand;
    }

    public void Release()
    {
        // If we've pulled far enough, fire
        if (m_PullValue > 0.25f)
            FireArrow();

        // Clear
        m_PullingHand = null;

        // Pull, animation
        m_PullValue = 0.0f;
        m_Animator.SetFloat("Blend", 0);

        // Create new arrow, with delay
        if (!m_CurrentArrow)
            StartCoroutine(CreateArrow(0.25f));
    }

    private void FireArrow()
    {
        m_CurrentArrow.Fire(m_PullValue * pullMultiplier);
        m_CurrentArrow = null;
    }
}
