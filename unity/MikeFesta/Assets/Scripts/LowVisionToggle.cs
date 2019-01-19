using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowVisionToggle : MonoBehaviour
{

    public enum VisionLevel {
        Full, 
        Low1,
        Low2,
        Low3,
        Low4,
        Low5,
        Low6
    };

    public VisionLevel initialVisionLevel;
    public Material skybox;
    public Texture lowVision1Texture;
    public Texture lowVision2Texture;
    public Texture lowVision3Texture;
    public Texture lowVision4Texture;
    public Texture lowVision5Texture;
    public Texture lowVision6Texture;
    public Texture fullTexture;

    private VisionLevel currentState;

    // Start is called before the first frame update
    void Start()
    {
        SetState(initialVisionLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetState(VisionLevel.Full);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(VisionLevel.Low1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(VisionLevel.Low2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(VisionLevel.Low3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetState(VisionLevel.Low4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetState(VisionLevel.Low5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetState(VisionLevel.Low6);
        }
    }


    void SetState(VisionLevel newState) {
        this.currentState = newState;
        switch(newState) {
            case VisionLevel.Full:
                this.skybox.SetTexture("_Tex", this.fullTexture);
                break;
            case VisionLevel.Low1:
                this.skybox.SetTexture("_Tex", this.lowVision1Texture);
                break;
            case VisionLevel.Low2:
                this.skybox.SetTexture("_Tex", this.lowVision2Texture);
                break;
            case VisionLevel.Low3:
                this.skybox.SetTexture("_Tex", this.lowVision3Texture);
                break;
            case VisionLevel.Low4:
                this.skybox.SetTexture("_Tex", this.lowVision4Texture);
                break;
            case VisionLevel.Low5:
                this.skybox.SetTexture("_Tex", this.lowVision5Texture);
                break;
            case VisionLevel.Low6:
                this.skybox.SetTexture("_Tex", this.lowVision6Texture);
                break;
        }
    }
}
