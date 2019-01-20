using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XableSettings : MonoBehaviour
{
    public bool LowVision;
    public bool Deaf;
    public bool ColorBlind;
    public float EnlargeScale = 3;
    public bool BringEnlargedClose;
    public float UpcloseDistance = 2;
    public int WordVisibleTime = 100; // Number of update loops for each word - this should be changed to a time value so it is not framerate dependant


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
