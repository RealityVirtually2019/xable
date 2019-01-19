using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeObject : MonoBehaviour
{
    public Camera vrCamera;
    public GameObject objectToEnlarge;
    public float targetDistance = 4;
    public float enlargedScale = 5;

    private bool enlarged;
    private Vector3 originalLocation;
    private Vector3 originalScale;
    private Vector3 originalRotationEuler;

    // Start is called before the first frame update
    void Start()
    {
        this.RecordOriginalSize(this.objectToEnlarge);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        if (GvrControllerInput.AppButtonUp)
        {
            this.SetOriginalSize(this.objectToEnlarge);
        }
        else if (GvrControllerInput.AppButtonDown && !this.enlarged)
        {
            this.Enlarge(this.objectToEnlarge);
        }
    }

    void RecordOriginalSize(GameObject o)
    {
        this.originalLocation = o.transform.position;
        this.originalScale = o.transform.localScale;
        this.originalRotationEuler = o.transform.eulerAngles;
        this.enlarged = false;
    }

    void SetOriginalSize(GameObject o)
    {
        o.transform.position = this.originalLocation;
        o.transform.localScale = this.originalScale;
        o.transform.eulerAngles = this.originalRotationEuler;
        this.enlarged = false;
    }


    void Enlarge(GameObject o)
    {
        o.transform.localScale = new Vector3(enlargedScale,enlargedScale,enlargedScale);
        //o.transform.position = vrCamera.transform.position + vrCamera.transform.forward * this.targetDistance;
        o.transform.parent = vrCamera.transform;
        // NOTE: The neck model appears to be doing something weird here - head angle changes where the object moves to
        Debug.Log(vrCamera.transform.eulerAngles);
        o.transform.eulerAngles = vrCamera.transform.eulerAngles;
        Debug.Log(vrCamera.transform.forward);
        o.transform.localPosition = vrCamera.transform..forward * this.targetDistance;
        o.transform.parent = null; // This should move back to Enlarge after the position is set, it is only here for testing
        // Matching camera rotation may or may not be desired - needs more testing
        this.enlarged = true;
    }
}
