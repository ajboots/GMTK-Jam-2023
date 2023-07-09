using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightText : MonoBehaviour
{
    [TextArea]
    public string TextDescription;

    public string GetText()
    {
        return TextDescription;
    }
}
