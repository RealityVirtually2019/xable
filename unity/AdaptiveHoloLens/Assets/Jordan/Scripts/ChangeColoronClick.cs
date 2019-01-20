using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ChangeColoronClick : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (this.GetComponent<MeshRenderer>().material.color == Color.green)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
