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
        this.transform.localScale = new Vector3(this.xable.settings.EnlargeScale,this.xable.settings.EnlargeScale,this.xable.settings.EnlargeScale);
        // TODO: Center in front of the user
    }

    void RestoreScale()
    {
        this.transform.localScale = new Vector3(1,1,1);
    }
}
