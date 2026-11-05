using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using System;

public class PushClose : CoreFeatures
{
    [Header("Push")]
    [SerializeField]
    private Transform pushableObject;

    [SerializeField]
    private Grabbable grabbable;

    [SerializeField]
    private float snapDistance = 0.04f; //Once drawer is <= 4 centimeters, finish closing it

    private float closedZ;

    private Bounds bounds; //Size of mesh. Where is front side

    private PokeInteractor[] fingers; //This should controllers as well

    private readonly Dictionary<PokeInteractor, bool> wasInFront = new Dictionary<PokeInteractor, bool>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
