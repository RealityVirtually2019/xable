using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xable : MonoBehaviour
{
    public List<string> list;
    public string AltText;
    public Texture HighContrast;
    
    //ColorPallet;
    //AudioFile;
    //Haptics;
    //ClickAction - enlarge, animate

    private ADASettings adaSettings;
    private ADAInput adaInput;
    

    // Start is called before the first frame update
    void Start()
    {
        this.adaSettings = Object.FindObjectOfType<ADASettings>();
        this.adaInput = Object.FindObjectOfType<ADAInput>();
    }



    // Update is called once per frame
    void Update()
    {
       // Based on the bool flags in the ADA settings, perform different actions on this game object
       if (this.adaSettings.LowVision)
       {
          // TODO: Change this to an event listener based model - this is just for quick testing
          if (this.adaInput.SelectAction() && this.InFocus())
          {
              this.EnlargeScale();
          }
          else if (this.adaInput.DeselectAction())
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
        this.transform.localScale = new Vector3(this.adaSettings.EnlargeScale,this.adaSettings.EnlargeScale,this.adaSettings.EnlargeScale);
        // TODO: Center in front of the user
    }

    void RestoreScale()
    {
        this.transform.localScale = new Vector3(1,1,1);
    }
}
