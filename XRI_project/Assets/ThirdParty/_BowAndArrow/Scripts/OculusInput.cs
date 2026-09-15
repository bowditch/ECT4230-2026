using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using Oculus.Interaction.Input;

public class OculusInput : MonoBehaviour
{
    public Bow m_Bow = null;
    public GameObject m_OppositeController = null;
    public OVRInput.Controller m_Controller = OVRInput.Controller.None;
    public OVRHand hand;

    private bool wasPinching = false;

    private void Update()
    {
        bool isFingerPinchUp = false;
        bool isFingerPinchDown = false;

        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (!wasPinching)
            {
                wasPinching = true;
                isFingerPinchDown = true;
            }
        }    
        else
        {
            if(wasPinching)
            {
                wasPinching = false;
                isFingerPinchUp = true;
            }
        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_Controller) || isFingerPinchDown)
            m_Bow.Pull(m_OppositeController.transform);

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_Controller) || isFingerPinchUp)
            m_Bow.Release();
    }
}
