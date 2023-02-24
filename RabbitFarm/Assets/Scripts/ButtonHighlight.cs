using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlight: MonoBehaviour
{
    public Sprite origenSprite;
    public Sprite highlightSprite;


    public void SetHighlightSprite()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = highlightSprite;
    }

    public void SetOriginSprite()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = origenSprite;
    }

}
