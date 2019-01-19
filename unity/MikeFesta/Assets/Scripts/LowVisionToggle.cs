using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowVisionToggle : MonoBehaviour
{

    public enum VisionLevel {Full, Blur_1x_10, Blur_2x_10, Blur_3x_10};

    public VisionLevel initialVisionLevel;
    public Material skybox;
    public Texture lowVision1Texture;
    public Texture lowVision2Texture;
    public Texture lowVision3Texture;
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
            SetState(VisionLevel.Blur_1x_10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(VisionLevel.Blur_2x_10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(VisionLevel.Blur_3x_10);
        }
    }


    void SetState(VisionLevel newState) {
        this.currentState = newState;
        switch(newState) {
            case VisionLevel.Blur_1x_10:
                this.skybox.SetTexture("_Tex", this.lowVision1Texture);
                break;
            case VisionLevel.Blur_2x_10:
                this.skybox.SetTexture("_Tex", this.lowVision2Texture);
                break;
            case VisionLevel.Blur_3x_10:
                this.skybox.SetTexture("_Tex", this.lowVision3Texture);
                break;
            case VisionLevel.Full:
                this.skybox.SetTexture("_Tex", this.fullTexture);
                break;
        }
    }
}
