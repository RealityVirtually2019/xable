using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeObject : MonoBehaviour
{

    public GameObject objectToEnlarge;
    public float targetDistance = 1;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.enlarged)
            {
                this.SetOriginalSize(this.objectToEnlarge);
            }
            else
            {
                this.Enlarge(this.objectToEnlarge);
            }
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
        o.transform.parent = null; // This should move back to Enlarge after the position is set, it is only here for testing
        o.transform.position = this.originalLocation;
        o.transform.localScale = this.originalScale;
        o.transform.eulerAngles = this.originalRotationEuler;
        this.enlarged = false;
    }


    void Enlarge(GameObject o)
    {
        //o.transform.localScale = new Vector3(3,3,3);
        //o.transform.position = Camera.main.transform.position + Camera.main.transform.forward * this.targetDistance;
        o.transform.parent = Camera.main.transform;
        // NOTE: The neck model appears to be doing something weird here - head angle changes where the object moves to
        o.transform.localPosition = Camera.main.transform.forward * this.targetDistance;

        // Matching camera rotation may or may not be desired - needs more testing
        //o.transform.eulerAngles = Camera.main.transform.eulerAngles;
        this.enlarged = true;
    }
}
