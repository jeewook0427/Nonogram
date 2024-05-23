using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHintUI : MonoBehaviour
{
    [SerializeField]
    private HintUIText hintUIText;

    [HideInInspector]
    public List<HintUIText> hintUITextList = new List<HintUIText>();
    
    [HideInInspector]
    public List<List<HintUIText>> HorizonHintUITextList = new List<List<HintUIText>>();
    
    [HideInInspector]
    public List<List<HintUIText>> VerticalHintUITextList = new List<List<HintUIText>>();
    private void Awake()
    {
        for (int i = 0; i < 500; i++)
        {
            HintUIText innerHintUIText = Instantiate<HintUIText>(hintUIText, this.transform);
            hintUITextList.Add(innerHintUIText);
        }
    }

    public void Init()
    {
        for (int i = 0; i < 500; i++)
        {
            hintUITextList[i].gameObject.SetActive(false);
        }
    }

    public void MakeHintUI(NonoBlockPlateInfoData nonoBlockPlateInfoData, List<NonoBlock> nonoBlockList)
    {
        int hitTextIndex = 0;
        int lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfoData.Count);

        List<int> tmpNonoBlockPlateInfoData = nonoBlockPlateInfoData.nonoBlockPlateInfoData;
        int textValue = 0;
        int checkEmpty = 0;

        //가로 UI만들기
        for (int i = 0; i < lineBlockNum; i++)
        {
            List<HintUIText> tmpTextList = new List<HintUIText>();

            for (int j = 0; j < lineBlockNum; j++)
            {
                if (tmpNonoBlockPlateInfoData[j + i * lineBlockNum] == 0)
                {
                    checkEmpty++;
                    if (textValue != 0)
                    {
                        HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                        InnerHintUIText.SetText(textValue);
                        InnerHintUIText.gameObject.SetActive(true);
                        tmpTextList.Add(InnerHintUIText);
                        hitTextIndex++;
                        textValue = 0;
                    }

                    else
                    {
                        if(checkEmpty == lineBlockNum)
                        {
                            HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                            InnerHintUIText.SetText(0);
                            InnerHintUIText.gameObject.SetActive(true);
                            tmpTextList.Add(InnerHintUIText);
                            hitTextIndex++;
                        }
                    }
                   
                }

                else
                {
                    textValue++;
                    checkEmpty = 0;

                    if (j == lineBlockNum - 1)
                    {
                        HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                        InnerHintUIText.SetText(textValue);
                        InnerHintUIText.gameObject.SetActive(true);
                        tmpTextList.Add(InnerHintUIText);
                        hitTextIndex++;
                        textValue = 0;
                    }
                }
            }

            HorizonHintUITextList.Add(tmpTextList);
        }

        //세로 UI만들기
        for (int i = 0; i < lineBlockNum; i++)
        {
            List<HintUIText> tmpTextList = new List<HintUIText>();

            for (int j = 0; j < lineBlockNum; j++)
            {
                if (tmpNonoBlockPlateInfoData[(lineBlockNum - 1 - j) * lineBlockNum + i] == 0)
                {
                    checkEmpty++;
                    if (textValue != 0)
                    {
                        HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                        InnerHintUIText.SetText(textValue);
                        InnerHintUIText.gameObject.SetActive(true);
                        tmpTextList.Add(InnerHintUIText);
                        hitTextIndex++;
                        textValue = 0;
                    }

                    else
                    {
                        if (checkEmpty == lineBlockNum)
                        {
                            HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                            InnerHintUIText.SetText(0);
                            InnerHintUIText.gameObject.SetActive(true);
                            tmpTextList.Add(InnerHintUIText);
                            hitTextIndex++;
                        }
                    }
                }

                else
                {
                    textValue++;

                    checkEmpty = 0;

                    if (j == lineBlockNum - 1)
                    {
                        HintUIText InnerHintUIText = hintUITextList[hitTextIndex];
                        InnerHintUIText.SetText(textValue);
                        InnerHintUIText.gameObject.SetActive(true);
                        tmpTextList.Add(InnerHintUIText);
                        hitTextIndex++;
                        textValue = 0;
                    }
                }
            }

            VerticalHintUITextList.Add(tmpTextList);
        }

        SetTextPosition(nonoBlockList);
    }

    public void SetTextPosition(List<NonoBlock> nonoBlockList)
    {
        int lineBlockNum = (int)Mathf.Sqrt(nonoBlockList.Count);
        Vector2 blockPosition;
        Vector2 blockSize = nonoBlockList[0].GetSizeVector();
        blockSize *= 0.5f;
        Vector2 offsetX = new Vector2(Constants.HINTUIBLOCKOFFSET, 0);
        Vector2 offsetY = new Vector2(0, Constants.HINTUIBLOCKOFFSET);
        Vector2 previousTextLocalPosition = Vector2.zero;

        for (int i = 0; i < HorizonHintUITextList.Count; i++)
        {
            blockPosition = nonoBlockList[i * lineBlockNum].GetLocalPosition();
         
            for (int j = 0; j < HorizonHintUITextList[i].Count; j++)
            {
                if (j == 0)
                {
                    HorizonHintUITextList[i][j].SetLocalPosition(blockPosition - new Vector2(Constants.HINTUIBLOCKOFFSET + blockSize.x, 0) * (j + 1));
                    previousTextLocalPosition = HorizonHintUITextList[i][j].GetLocalPosition();
                }

                else
                    HorizonHintUITextList[i][j].SetLocalPosition(previousTextLocalPosition - new Vector2(Constants.HINTUITEXTOFFSET, 0) * (j + 1));
            }
        }

        for (int i = 0; i < VerticalHintUITextList.Count; i++)
        {
            blockPosition = nonoBlockList[i].GetLocalPosition();

            for (int j = 0; j < VerticalHintUITextList[i].Count; j++)
            {
                if(j==0)
                {
                    VerticalHintUITextList[i][j].SetLocalPosition(blockPosition + new Vector2(0, Constants.HINTUIBLOCKOFFSET + blockSize.y) * (j + 1));
                    previousTextLocalPosition = VerticalHintUITextList[i][j].GetLocalPosition();
                }
                
                else
                    VerticalHintUITextList[i][j].SetLocalPosition(previousTextLocalPosition - new Vector2(0, Constants.HINTUITEXTOFFSET) * (j + 1));
            }
        }
    }
}
