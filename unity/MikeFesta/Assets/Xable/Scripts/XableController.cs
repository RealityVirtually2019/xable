﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XableController : MonoBehaviour
{

    [HideInInspector]
    public XableInput input;
    [HideInInspector]
    public XableSettings settings;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        this.input = this.gameObject.GetComponent<XableInput>();
        this.settings = this.gameObject.GetComponent<XableSettings>();
        this.camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
