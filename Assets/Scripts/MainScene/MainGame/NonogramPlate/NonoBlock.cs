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

   //[SerializeField]
   // private GameObject tickLine;

    Image image;
   // RectTransform outRectTransform;
    RectTransform rectTransform;
   // RectTransform[] tickLineRectTransform;

    Color whiteColor = Color.white;
    Color blackColor = Color.black;

    int blockState;
    Cord cord;

    private void Awake()
    {
       // if (!innerImage)
       //     innerImage = innterBlock.GetComponent<Image>();

       // if (!outRectTransform)
         //   outRectTransform = GetComponent<RectTransform>();

       // if (!innerRectTransform)
         //   innerRectTransform = innterBlock.GetComponent<RectTransform>();

        //tickLineRectTransform = tickLine.GetComponentsInChildren<RectTransform>();

        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void Init(Vector2 innerSizeDelta, Vector2 outSizeDelta, Vector2 position, int cordX, int cordY)
    {
        // tickLine.SetActive(false);
        //내부 블럭 사이즈 줄이기 (가장자리 선 만들기위함)
        rectTransform.sizeDelta = innerSizeDelta;
        //외부 블럭 사이즈는 간격에 맞게
        rectTransform.sizeDelta = outSizeDelta;
        rectTransform.transform.localPosition = position;

        cord.X = cordX;
        cord.Y = cordY;

        blockState = 0;
        SetBlockColor(whiteColor);
        //SetTickLine();
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
        image.color = changeColor;
    }

    //private void SetTickLine()
    //{
    //    for(int i = 0; i< tickLineRectTransform.Length; i++)
    //    {
    //        tickLineRectTransform[i].gameObject.SetActive(false);
    //    }

    //    if ((cord.X + 1) % 5 != 0 && (cord.Y + 1) % 5 != 0 && (cord.X) % 5 != 0 && (cord.Y) % 5 != 0)
    //    {
    //        return;
    //    }

    //    if ((cord.X + 1) % 5 == 0)
    //    {
    //        Vector2 sizeDelta = Vector2.zero;

    //        sizeDelta.x = (outRectTransform.sizeDelta.x - innerRectTransform.sizeDelta.x) * 0.5f;
    //        sizeDelta.y = outRectTransform.sizeDelta.y;
            
    //        float positionX = (outRectTransform.sizeDelta.x - sizeDelta.x) * 0.5f;
    //        Vector2 position = new Vector2(positionX, 0);

    //        tickLineRectTransform[1].sizeDelta = sizeDelta;
    //        tickLineRectTransform[1].localPosition = position;
    //        tickLineRectTransform[1].gameObject.SetActive(true);
    //    }

    //    if((cord.X) % 5 == 0)
    //    {
    //        Vector2 sizeDelta = Vector2.zero;

    //        sizeDelta.x = (outRectTransform.sizeDelta.x - innerRectTransform.sizeDelta.x) * 0.5f;
    //        sizeDelta.y = outRectTransform.sizeDelta.y;

    //        float positionX = (outRectTransform.sizeDelta.x - sizeDelta.x) * 0.5f;
    //        Vector2 position = new Vector2(positionX, 0);

    //        tickLineRectTransform[1].sizeDelta = sizeDelta;
    //        tickLineRectTransform[1].localPosition = -position;
    //        tickLineRectTransform[1].gameObject.SetActive(true);
    //    }

    //    if ((cord.Y) % 5 == 0)
    //    {
    //        Vector2 sizeDelta = Vector2.zero;

    //        sizeDelta.x = outRectTransform.sizeDelta.x;
    //        sizeDelta.y = (outRectTransform.sizeDelta.y - innerRectTransform.sizeDelta.y) * 0.5f;
            
    //        float positionY = (outRectTransform.sizeDelta.y - sizeDelta.y) * 0.5f;
    //        Vector2 position = new Vector2(0, positionY);

    //        tickLineRectTransform[2].sizeDelta = sizeDelta;
    //        tickLineRectTransform[2].localPosition = -position;
    //        tickLineRectTransform[2].gameObject.SetActive(true);
    //    }

    //    if ((cord.Y + 1) % 5 == 0)
    //    {
    //        Vector2 sizeDelta = Vector2.zero;

    //        sizeDelta.x = outRectTransform.sizeDelta.x;
    //        sizeDelta.y = (outRectTransform.sizeDelta.y - innerRectTransform.sizeDelta.y) * 0.5f;

    //        float positionY = (outRectTransform.sizeDelta.y - sizeDelta.y) * 0.5f;
    //        Vector2 position = new Vector2(0, positionY);

    //        tickLineRectTransform[2].sizeDelta = sizeDelta;
    //        tickLineRectTransform[2].localPosition = position;
    //        tickLineRectTransform[2].gameObject.SetActive(true);
    //    }

    //    tickLine.gameObject.SetActive(true);
    //}

    public Vector2 GetWorldPosition() { return rectTransform.position; }
    public Vector2 GetLocalPosition() { return rectTransform.localPosition; }
    public Vector2 GetSizeVector() { return rectTransform.sizeDelta; }
    public int GetBlockState() { return blockState; }
    public Cord GetCord() { return cord; }
}
