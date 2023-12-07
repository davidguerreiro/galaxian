using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour
{
    private Text _content;                                          // Text component reference.

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    /// <summary>
    /// Get current text
    /// content.
    /// </summary>
    /// <returns>string</returns>
    public string GetContent()
    {
        if (_content == null)
        {
            _content = GetComponent<Text>();
        }

        return _content.text;
    }

    /// <summary>
    /// Update content.
    /// </summary>
    /// <param name="newContent">string - new content to be displayed in this text component.</param>
    public void UpdateContent(string newContent)
    {

        if (_content == null)
        {
            _content = GetComponent<Text>();
        }

        _content.text = newContent;
    }

    /// <summary>
    /// Update text colour.
    /// </summary>
    /// <param name="colour">color - Colour to apply to the text</param>
    public void UpdateColour(Color colour)
    {
        if (_content == null)
        {
            _content = GetComponent<Text>();
        }

        _content.color = colour;
    }

    /// <summary>
    /// Get current text colour.
    /// </summary>
    public Color GetColour()
    {
        if (_content == null)
        {
            _content = GetComponent<Text>();
        }

        return _content.color;
    }

    /// <summary>
    /// Set text align.
    /// </summary>
    /// <param name="anchor">TextAnchor - text anchor.
    public void SetAlign(TextAnchor anchor)
    {
        _content.alignment = anchor;
    }

    /// <summary>
    /// Set font size.
    /// </summary>
    /// <param name="size">int - new font size</param>
    public void SetFontSize(int size)
    {
        _content.fontSize = size;
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init()
    {

        // get text component reference.
        if (_content == null)
        {
            _content = GetComponent<Text>();
        }
    }
}
