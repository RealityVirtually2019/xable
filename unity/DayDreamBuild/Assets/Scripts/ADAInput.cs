using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADAInput : MonoBehaviour
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
        return GvrControllerInput.AppButtonDown;
    }

    // This is where we can abstract input devices to communicate a deselect action
    public bool DeselectAction()
    {
        return GvrControllerInput.AppButtonUp;
    }
}
