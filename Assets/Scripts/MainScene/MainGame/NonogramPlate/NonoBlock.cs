using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Jobs;

public enum BlockState
{
    Empty,
    Filled,
    Marked,
}

public class NonoBlock : MonoBehaviour
{
    public struct Cord
    {
        public int X;
        public int Y;
    }

    [SerializeField]
    private GameObject innterBlock;

    [Header("----------------------------------------")]
    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite markingSprite;

    Image image;
    Image innerImage;

    Color outLineColor;
    // RectTransform outRectTransform;
    RectTransform rectTransform;
    RectTransform innerRectTransform;
    // RectTransform[] tickLineRectTransform;

    Color whiteColor = Color.white;
    Color blackColor = Color.black;

    BlockState blockState;
    Cord cord;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        innerRectTransform = innterBlock.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
        innerImage = innterBlock.GetComponent<Image>();
        outLineColor = Color.yellow;
    }

    public void Init(Vector2 innerSizeDelta, Vector2 outSizeDelta, Vector2 position, int cordX, int cordY)
    {
        // tickLine.SetActive(false);
        //내부 블럭 사이즈 줄이기 (가장자리 선 만들기위함)
        innerRectTransform.sizeDelta = innerSizeDelta;
        //외부 블럭 사이즈는 간격에 맞게
        rectTransform.sizeDelta = outSizeDelta;
        rectTransform.transform.localPosition = position;

        cord.X = cordX;
        cord.Y = cordY;

        blockState = BlockState.Empty;
        SetBlockColor(whiteColor);
        image.enabled = false;
    }

    public void ChangeBlockState(BlockState innerBlockState)
    {
        blockState = innerBlockState;

        if (blockState == BlockState.Empty) 
        {
            innerImage.sprite = defaultSprite;
            SetBlockColor(whiteColor);
        }

        if (blockState == BlockState.Filled)
        {
            innerImage.sprite = defaultSprite;
            SetBlockColor(blackColor);
        }

        else if (blockState == BlockState.Marked)
        {
            innerImage.sprite = markingSprite;
            SetBlockColor(whiteColor);
        }
    }

    private void SetBlockColor(Color changeColor)
    {
        innerImage.color = changeColor;
    }

    public void ChangeOutLine(bool turnOn)
    {
        if (turnOn)
        {
            image.enabled = true;
            image.color = outLineColor;
        }
            
        else
            image.enabled = false;
    }

    public Vector2 GetWorldPosition() { return rectTransform.position; }
    public Vector2 GetLocalPosition() { return rectTransform.localPosition; }
    public Vector2 GetSizeVector() { return rectTransform.sizeDelta; }
    public BlockState GetBlockState() { return blockState; }
    public Cord GetCord() { return cord; }
}
