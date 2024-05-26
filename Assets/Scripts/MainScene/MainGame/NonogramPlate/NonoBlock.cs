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

    [SerializeField]
    private GameObject innterBlock;

    Image innerImage;
    RectTransform outRectTransform;
    RectTransform innerRectTransform;

    Color whiteColor = Color.white;
    Color blackColor = Color.black;

    int blockState;
    Cord cord;

    private void Awake()
    {
        if (!innerImage)
            innerImage = innterBlock.GetComponent<Image>();

        if (!outRectTransform)
            outRectTransform = GetComponent<RectTransform>();

        if (!innerRectTransform)
            innerRectTransform = innterBlock.GetComponent<RectTransform>();
    }

    public void Init(Vector2 innerSizeDelta, Vector2 outSizeDelta, Vector2 position, int cordX, int cordY)
    {
        //내부 블럭 사이즈 줄이기 (가장자리 선 만들기위함)
        innerRectTransform.sizeDelta = innerSizeDelta;
        //외부 블럭 사이즈는 간격에 맞게
        outRectTransform.sizeDelta = outSizeDelta;
        outRectTransform.transform.localPosition = position;

        cord.X = cordX;
        cord.Y = cordY;

        blockState = 0;
        SetBlockColor(whiteColor);
    }

    public void ChangeBlockState(int innerBlockState)
    {
        blockState = innerBlockState;

        if (blockState == 1)
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
        innerImage.color = changeColor;
    }

    public Vector2 GetWorldPosition() { return outRectTransform.position; }
    public Vector2 GetLocalPosition() { return outRectTransform.localPosition; }
    public Vector2 GetSizeVector() { return outRectTransform.sizeDelta; }
    public int GetBlockState() { return blockState; }
    public Cord GetCord() { return cord; }
}
