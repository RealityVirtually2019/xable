using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XableObject : MonoBehaviour
{

    public string AltText;
    public Texture HighContrast;
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

    // Start is called before the first frame update
    void Start()
    {
        this.xable = Object.FindObjectOfType<XableController>();
    }

    // Update is called once per frame
    void Update()
    {
       // Based on the bool flags in the Xable settings, perform different actions on this game object
       if (this.xable.settings.LowVision)
       {
          // TODO: Change this to an event listener based model - this is just for quick testing
          if (this.xable.input.SelectAction() && this.InFocus())
          {
              this.EnlargeScale();
          }
          else if (this.xable.input.DeselectAction())
          {
              this.RestoreScale();
          }
       }
    }

    bool InFocus()
    {
        // TODO: Come up with a universal way to have one Xable object in focus at a time
        // This should be similar to the TabFocus HTML attribute in HTML
        return true;
    }

    void EnlargeScale()
    {
        if (!this.enlarged)
        {
            this.originalScale = this.transform.localScale;
            Debug.Log(this.originalScale);
            this.transform.localScale = new Vector3(this.xable.settings.EnlargeScale,this.xable.settings.EnlargeScale,this.xable.settings.EnlargeScale);

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
        }
    }

    void RestoreScale()
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
        }
    }
}
