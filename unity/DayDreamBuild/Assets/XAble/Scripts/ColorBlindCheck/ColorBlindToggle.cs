using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlindToggle : MonoBehaviour
{
    [SerializeField]
    private bool toggleProtanopia;
    // Start is called before the first frame update
    void Start()
    {
        toggleProtanopia = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleProtanopia()
    {
        toggleProtanopia = !toggleProtanopia;
    }


}
