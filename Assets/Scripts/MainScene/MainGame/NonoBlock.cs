using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Jobs;

public class NonoBlock : MonoBehaviour
{
    public struct Cord
    {
        public int X;
        public int Y;
    }

    Image image;
    RectTransform rectTransform;

    Color whiteColor = Color.white;
    Color blackColor = Color.black;

    bool isBlockFilled;
    Cord cord;

    public void Init(Vector2 sizeDelta, Vector2 position, int cordX, int cordY)
    {
        if(!image)
            image = GetComponent<Image>();

        if(!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = sizeDelta;
        rectTransform.transform.localPosition = position;

        cord.X = cordX;
        cord.Y = cordY;

        isBlockFilled = false;
        SetBlockColor(whiteColor);
    }

    public void ChangeBlockState(bool fillBlock)
    {
        isBlockFilled = fillBlock;

        if (isBlockFilled)
        {
           SetBlockColor(blackColor);
        }

        else
        {
            SetBlockColor(whiteColor);
        }
    }

    private void SetBlockColor(Color changeColor)
    {   
        image.color = changeColor;
    }

    public bool GetBlockState() { return isBlockFilled; }
    public Cord GetCord() { return cord; }
}
