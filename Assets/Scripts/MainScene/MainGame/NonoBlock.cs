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

    bool isBlockFilled;
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
        //���� �� ������ ���̱� (�����ڸ� �� ���������)
        innerRectTransform.sizeDelta = innerSizeDelta;
        //�ܺ� �� ������� ���ݿ� �°�
        outRectTransform.sizeDelta = outSizeDelta;
        outRectTransform.transform.localPosition = position;

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
        innerImage.color = changeColor;
    }

    public Vector2 GetWorldPosition() { return outRectTransform.position; }
    public Vector2 GetLocalPosition() { return outRectTransform.localPosition; }
    public Vector2 GetSizeVector() { return outRectTransform.sizeDelta; }
    public bool GetBlockState() { return isBlockFilled; }
    public Cord GetCord() { return cord; }
}
