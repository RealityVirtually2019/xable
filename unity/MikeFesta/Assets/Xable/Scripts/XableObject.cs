﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class XableObject : MonoBehaviour
{

    public string AltText;
    public Texture HighContrast;
    public AudioClip AudioClip;
    //ColorPallet;
    //AudioFile;
    //Haptics;
    //ClickAction - enlarge, animate

    private XableController xable;

    // Save original when enlarging and/or brining the object close
    private bool enlarged;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Vector3 originalScale;
    private Renderer renderer;
    private bool showingText;

    // Start is called before the first frame update
    void Start()
    {
        this.xable = Object.FindObjectOfType<XableController>();
        this.renderer = this.gameObject.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Based on the bool flags in the Xable settings, perform different actions on this game object
        if (this.xable.settings.LowVision)
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

            // Also a Low Vision Aid - Displaying the alt text up close
            if (this.xable.input.ShowAltTextAction() && this.HasFocus())
            {
                this.ShowAltText();
            }
            else if (this.xable.input.HideAltTextAction())
            {
                this.HideAltText();
            }

            // Audio
            if (this.xable.input.PlayAudioAction() && this.HasFocus())
            {
                this.PlayAudio();
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
        else
        {
            // This object hasn't been initialized yet, so the cached renderer isn't available
            this.gameObject.GetComponentInChildren<Renderer>().gameObject.AddComponent<Outline>();
        }
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

    public void ShowAltText()
    {
        if (!this.showingText)
        {
            Debug.Log(this.AltText);
            this.xable.TextViewer.LoadText(this.AltText);
            this.showingText = true;
        }

    }

    public void HideAltText()
    {
        this.showingText = false;
        this.xable.TextViewer.Hide();
    }

    public void PlayAudio()
    {
        if (this.AudioClip != null)
        {
            // Play the audio clip (if there is one)
            //this.audioClip.Play();
            this.xable.PlayAudio(this.AudioClip);
        }
        else
        {
            // If there is no audio clip, use a Text-to-speech service to play thet Alt Text
            Debug.Log("No Audio Clip Found");
            // TODO: Text to speech service
        }
    }
}
