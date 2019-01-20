﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XableInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is where we can abstract input devices to communicate a select action
    public bool SelectAction()
    {
        return (GvrControllerInput.AppButtonDown || Input.GetKey(KeyCode.Space));
    }

    // This is where we can abstract input devices to communicate a deselect action
    public bool DeselectAction()
    {
        return (GvrControllerInput.AppButtonUp || Input.GetKeyUp(KeyCode.Space));
    }

    public bool CycleActiveObject()
    {
        return (Input.GetKeyDown(KeyCode.Tab));
    }
}
