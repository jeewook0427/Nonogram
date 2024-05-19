using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HintUIText : MonoBehaviour
{
    private RectTransform rectTransform;

    private Text text;
    private void Awake()
    {
        if(!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        if(!text)
            text = GetComponent<Text>();
    }
    public void Init()
    {
        
    }

    public void ChangeTextColor()
    {
        text.color = Color.white;
    }
}
