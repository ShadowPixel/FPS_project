﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action GroundDetected = delegate { };
    public event Action GroundVanished = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        GroundDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GroundVanished?.Invoke();
    }
}
