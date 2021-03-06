﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class XableObject : MonoBehaviour
{

    public string AltText;
    public Texture HighContrast;
    public bool IsStatic;
    //ColorPallet;
    //AudioFile;
    //Haptics;
    //ClickAction - enlarge, animate

    private XableController xable;

    // definable shader name for colorblind filters
    public string colorBlindShaderName = "XAble/ColorBlindness";

    // Save original when enlarging and/or brining the object close
    private bool enlarged;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Vector3 originalScale;
    private Renderer renderer;

    private Shader colorBlindShader;
    private float colorBlindToggle;
    private float colorBlindAdjustment;



    // Start is called before the first frame update
    void Start()
    {
        this.xable = Object.FindObjectOfType<XableController>();
        this.renderer = this.gameObject.GetComponentInChildren<Renderer>();
        colorBlindShader = Shader.Find(colorBlindShaderName);
        
    }

    // Update is called once per frame
    void Update()
    {
       // Based on the bool flags in the Xable settings, perform different actions on this game object
       if (this.xable.settings.LowVision  && !IsStatic)
       {
          // TODO: Change this to an event listener based model - this is just for quick testing
          if (this.xable.input.SelectAction() && this.HasFocus())
          {
              this.EnlargeScale();
          }
          else if (this.xable.input.DeselectAction())
          {
              this.RestoreScale();
          }
       }
        if (this.xable.settings.ColorBlind)
        {
           //renderer.material.shader = colorBlindShader;
        }
        if (this.xable.settings.ColorBlind)
        {
            if (this.xable.input.SelectAction())
            {
                colorBlindToggle = (colorBlindToggle == 0.0f) ? 1.0f : 0.0f;
                
                renderer.material.SetFloat("_ProtaColorMode", colorBlindToggle);
                if (colorBlindToggle == 1)
                {
                    renderer.material.EnableKeyword("PROTANOPIA");
                }
                else
                {
                    renderer.material.DisableKeyword("PROTANOPIA");
                }
                
                //renderer.material.
                Debug.Log(colorBlindToggle);
            }
            if (this.xable.input.TouchDown())
            {

                if (this.xable.input.TouchPos().y <= 0.25f)
                {
                    colorBlindAdjustment = 1.0f;
                }
                else
                {
                    colorBlindAdjustment = 0.0f;
                }
                renderer.material.SetFloat("_AdjustmentMode", colorBlindAdjustment);
                if (colorBlindAdjustment == 1)
                {
                    renderer.material.EnableKeyword("ADJUSTMENT");
                }
                else
                {
                    renderer.material.DisableKeyword("ADJUSTMENT");
                }
                Debug.Log("touch down"+ this.xable.input.TouchPos().y+"  "+colorBlindAdjustment);
            }

        }
    }

    bool HasFocus()
    {
        // This is similar to the TabFocus HTML attribute in HTML
        return (this == this.xable.activeObject);
    }

    public void Highlight()
    {
        if (this.renderer)
        {
            this.renderer.gameObject.AddComponent<Outline>();
        }
        // TODO: prevent this from adding multiple
    }

    public void Unhighlight()
    {
        Destroy(this.renderer.gameObject.GetComponent<Outline>());
    }

    void EnlargeScale()
    {
        if (!this.enlarged)
        {
            this.originalScale = this.transform.localScale;
            this.transform.localScale = new Vector3(
                this.transform.localScale.x * this.xable.settings.EnlargeScale,
                this.transform.localScale.y * this.xable.settings.EnlargeScale,
                this.transform.localScale.z * this.xable.settings.EnlargeScale
            );

            if (this.xable.settings.BringEnlargedClose)
            {
                // Place the object in front of the camera
                this.originalPosition = this.transform.position;
                this.originalRotation = this.transform.eulerAngles;

                this.transform.parent = this.xable.camera.transform;
                this.transform.localPosition = new Vector3 (0,0,0);
                // Not going to rotate it for now because it seems to be more natural this way
                //this.transform.eulerAngles = this.xable.camera.transform.eulerAngles;
                this.transform.localPosition = Vector3.forward * this.xable.settings.UpcloseDistance;
                // TODO: The distance should take the object size into account and the upclose distance should be the distance from the edge of the object to the camera
                this.transform.parent = null;
            }
            this.enlarged = true;
            this.Unhighlight();
        }
    }

    public void RestoreScale()
    {
        if (this.enlarged)
        {
            this.transform.localScale = this.originalScale;

            if (this.xable.settings.BringEnlargedClose)
            {
                // Restore Location
                this.transform.position = this.originalPosition;
                this.transform.eulerAngles = this.originalRotation;
            }
            this.enlarged = false;
            if (this.HasFocus())
            {
                this.Highlight();
            }
        }
    }
}
