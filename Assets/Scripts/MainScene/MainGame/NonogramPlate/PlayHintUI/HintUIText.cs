using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HintUIText : MonoBehaviour
{
    private RectTransform rectTransform;

    private Text text;
    private bool isCorrect;
    private int textValue;
    private void Awake()
    {
        if (!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        if (!text)
            text = GetComponent<Text>();
    }
    public void Init()
    {

    }

    public void ChangeState(bool innerIsCorrect)
    {
        isCorrect = innerIsCorrect;

        if(isCorrect)
        {
            text.color = Color.gray;
        }
        
        else
            text.color = Color.black;
    }

    public void SetText(int value) 
    {
        textValue = value;
        text.text = textValue.ToString(); 
    }

    public void SetLocalPosition(Vector2 position) { rectTransform.localPosition = position; }
    public Vector2 GetLocalPosition() { return rectTransform.localPosition; }
    public bool GetIsCorrect() { return isCorrect; }
    public int GetTextValue() { return textValue; }
}
