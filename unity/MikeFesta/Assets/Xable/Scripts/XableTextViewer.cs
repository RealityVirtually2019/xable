using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XableTextViewer : MonoBehaviour
{

    public TextMeshPro whiteText;
    public TextMeshPro redText;
    private XableController xable;
    private Renderer[] renderers;
    private string fullText;
    private string[] textArray;
    private bool isVisible;
    private int timeRemaining;
    private int currentWordIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.renderers = this.gameObject.GetComponentsInChildren<Renderer>();
        this.xable = Object.FindObjectOfType<XableController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isVisible)
        {
            if (this.timeRemaining <= 0)
            {
                SetNextWord();
                this.timeRemaining = this.xable.settings.WordVisibleTime;
            }
            else
            {
                this.timeRemaining = this.timeRemaining - 1;
            }
        }
    }

    public void Hide()
    {
        foreach (Renderer r in this.renderers)
        {
            r.enabled = false;
        }
    }

    public void LoadText(string text)
    {
        // Break the string into an array of space separated strings (also explode if too long)
        this.fullText = text;
        this.whiteText.text = "";
        this.redText.text = "";
        this.textArray = text.Split(' ');
        this.Show();
        this.timeRemaining = 0;
        this.currentWordIndex = 0;
    }

    void Show()
    {
        this.isVisible = true;
        foreach (Renderer r in this.renderers)
        {
            r.enabled = true;
        }
    }

    void SetNextWord()
    {
        if (this.currentWordIndex == this.textArray.Length)
        {
            // Extra time for the last word
        }
        else if (this.currentWordIndex > this.textArray.Length)
        {
            // All Text is finished, hide
            this.Hide();
        }
        else
        {
            this.whiteText.text = this.SpritzWordWhite(this.textArray[this.currentWordIndex]);
            this.redText.text = this.SpritzWordRed(this.textArray[this.currentWordIndex]);
        }
        this.currentWordIndex = this.currentWordIndex + 1;
    }

    string SpritzWordWhite(string word)
    {
        string spritz = this.SpritzFormat(word);
        return spritz.Substring(0, 4) + " " + spritz.Substring(5);
    }

    string SpritzWordRed(string word)
    {
        // Red is always the 5th character
        return "    " + this.SpritzFormat(word).Substring(4, 1);
    }

    string SpritzFormat(string word)
    {
        // Crude estimate of how to center words to make them easier to read fast one word at a time
        switch(word.Length)
        {
            case 1:
                return "    " + word; // 4 Spaces
            case 2:
            case 3:
            case 4:
            case 5:
                return "   " + word; // 3 Spaces
                return word;
            case 6:
            case 7:
                return "  " + word; // 2 Spaces
            case 8:
            case 9:
            case 10:
                return " " + word; // 1 Space

            default:
                return word;

        }
    }
}
