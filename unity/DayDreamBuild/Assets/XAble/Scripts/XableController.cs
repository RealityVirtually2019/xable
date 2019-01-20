using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XableController : MonoBehaviour
{

    [HideInInspector]
    public XableInput input;
    [HideInInspector]
    public XableSettings settings;
    [HideInInspector]
    public Camera camera;

    [HideInInspector]
    private XableObject[] objects;
    private int activeObjectIndex;
    public XableObject activeObject;


    // Start is called before the first frame update
    void Start()
    {
        this.input = this.gameObject.GetComponent<XableInput>();
        this.settings = this.gameObject.GetComponent<XableSettings>();
        this.camera = Camera.main;
        this.objects = GameObject.FindObjectsOfType<XableObject>();
        this.SetActiveObjectByIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.input.CycleActiveObject())
        {
            this.CycleActiveObjectForward();
        }
    }

    void CycleActiveObjectForward()
    {
        this.activeObject.RestoreScale();
        this.activeObject.Unhighlight();
        if (this.activeObjectIndex + 1 < this.objects.Length)
        {
            this.SetActiveObjectByIndex(this.activeObjectIndex + 1);
        }
        else
        {
            this.SetActiveObjectByIndex(0);
        }
    }

    void SetActiveObjectByIndex(int i)
    {
        if (i < this.objects.Length)
        {
            this.activeObject = this.objects[i];
            this.activeObjectIndex = i;
            this.activeObject.Highlight();
        }
    }
}
