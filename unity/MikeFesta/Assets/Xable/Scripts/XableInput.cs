using System.Collections;
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
        //Debug.Log("x: " + GvrControllerInput.TouchPos.x);
    }

    // This is where we can abstract input devices to communicate a select action
    public bool SelectAction()
    {
        return ((GvrControllerInput.ClickButtonDown && GvrControllerInput.TouchPos.x > 0.2 && GvrControllerInput.TouchPos.x < 0.8) || Input.GetKey(KeyCode.LeftControl));
    }

    // This is where we can abstract input devices to communicate a deselect action
    public bool DeselectAction()
    {
        return (GvrControllerInput.ClickButtonUp || Input.GetKeyUp(KeyCode.Space));
    }

    public bool CycleActiveObject()
    {
        return (GvrControllerInput.AppButtonUp || Input.GetKeyDown(KeyCode.Tab));
    }

    public bool ShowAltTextAction()
    {
        return ((GvrControllerInput.ClickButtonDown && GvrControllerInput.TouchPos.x > 0.8) || Input.GetKey(KeyCode.LeftControl));
    }

    public bool HideAltTextAction()
    {
        return (GvrControllerInput.ClickButtonUp || Input.GetKeyUp(KeyCode.LeftControl));
    }

    public bool PlayAudioAction()
    {
        return ((GvrControllerInput.ClickButtonDown && GvrControllerInput.TouchPos.x < 0.2) || Input.GetKey(KeyCode.Space));
    }
}
