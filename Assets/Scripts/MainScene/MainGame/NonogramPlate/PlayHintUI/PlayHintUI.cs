using System;
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

    int hintTextIndex;
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

        HorizonHintUITextList = new List<List<HintUIText>>();
        VerticalHintUITextList = new List<List<HintUIText>>();

        hintTextIndex = 0;
    }

    public void MakeHintUI(NonoBlockPlateInfoData nonoBlockPlateInfoData, List<NonoBlock> nonoBlockList)
    {
        HorizonHintUITextList = MakeHorizonHintUITextList(nonoBlockPlateInfoData.nonoBlockPlateInfo);
        VerticalHintUITextList = MakeVerticalHintUITextList(nonoBlockPlateInfoData.nonoBlockPlateInfo);
        SetTextPosition(nonoBlockList);
    }

    public List<List<T>> MakeHorizonHintList<T>(List<int> nonoBlockPlateInfo, Func<int, int, T> createHint)
    {
        int lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfo.Count);

        List<List<T>> InnerHintList = new List<List<T>>();
        int textValue = 0;
        int checkEmpty = 0;

        // 가로 UI 만들기
        for (int i = 0; i < lineBlockNum; i++)
        {
            List<T> tmpTextList = new List<T>();

            for (int j = 0; j < lineBlockNum; j++)
            {
                if (nonoBlockPlateInfo[i * lineBlockNum + ( lineBlockNum - 1) - j] == 0)
                {
                    checkEmpty++;
                    if (textValue != 0)
                    {
                        tmpTextList.Add(createHint(hintTextIndex, textValue));
                        hintTextIndex++;
                        textValue = 0;
                    }
                    else
                    {
                        if (checkEmpty == lineBlockNum)
                        {
                            tmpTextList.Add(createHint(hintTextIndex, 0));
                            hintTextIndex++;
                        }
                    }
                }
                else
                {
                    textValue++;
                    checkEmpty = 0;

                    if (j == lineBlockNum - 1)
                    {
                        tmpTextList.Add(createHint(hintTextIndex, textValue));
                        hintTextIndex++;
                        textValue = 0;
                    }
                }
            }

            InnerHintList.Add(tmpTextList);
        }

        return InnerHintList;
    }

    public List<List<HintUIText>> MakeHorizonHintUITextList(List<int> nonoBlockPlateInfo)
    {
        return MakeHorizonHintList(nonoBlockPlateInfo, (index, value) => SetHintUIText(index, value));
    }

    public List<List<int>> MakeHorizonAnswerList(List<int> nonoBlockPlateInfo)
    {
        return MakeHorizonHintList(nonoBlockPlateInfo, (index, value) => value);

    }
    public List<List<T>> MakeVerticalHintList<T>(List<int> nonoBlockPlateInfo, Func<int, int, T> createHint)
    {
        int lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfo.Count);

        List<List<T>> InnerHintList = new List<List<T>>();
        int textValue = 0;
        int checkEmpty = 0;

        // 세로 UI 만들기
        for (int i = 0; i < lineBlockNum; i++)
        {
            List<T> tmpTextList = new List<T>();

            for (int j = 0; j < lineBlockNum; j++)
            {
                if (nonoBlockPlateInfo[(lineBlockNum - 1 - j) * lineBlockNum + i] == 0)
                {
                    checkEmpty++;
                    if (textValue != 0)
                    {
                        tmpTextList.Add(createHint(hintTextIndex, textValue));
                        hintTextIndex++;
                        textValue = 0;
                    }
                    else
                    {
                        if (checkEmpty == lineBlockNum)
                        {
                            tmpTextList.Add(createHint(hintTextIndex, 0));
                            hintTextIndex++;
                        }
                    }
                }
                else
                {
                    textValue++;
                    checkEmpty = 0;

                    if (j == lineBlockNum - 1)
                    {
                        tmpTextList.Add(createHint(hintTextIndex, textValue));
                        hintTextIndex++;
                        textValue = 0;
                    }
                }
            }

            InnerHintList.Add(tmpTextList);
        }

        return InnerHintList;
    }

    public List<List<HintUIText>> MakeVerticalHintUITextList(List<int> nonoBlockPlateInfo)
    {
        return MakeVerticalHintList(nonoBlockPlateInfo, (index, value) => SetHintUIText(index, value));
    }

    public List<List<int>> MakeVerticalAnswerList(List<int> nonoBlockPlateInfo)
    {
        return MakeVerticalHintList(nonoBlockPlateInfo, (index, value) => value);
    }

    private HintUIText SetHintUIText(int hintTextIndex, int textValue)
    {
        HintUIText InnerHintUIText = hintUITextList[hintTextIndex];
        InnerHintUIText.SetText(textValue);
        InnerHintUIText.gameObject.SetActive(true);

        return InnerHintUIText;
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
                    HorizonHintUITextList[i][j].SetLocalPosition(blockPosition - new Vector2(Constants.HINTUIBLOCKOFFSET + blockSize.x, 0));
                    previousTextLocalPosition = HorizonHintUITextList[i][j].GetLocalPosition();
                }

                else
                {
                    HorizonHintUITextList[i][j].SetLocalPosition(previousTextLocalPosition - new Vector2(Constants.HINTUITEXTOFFSET, 0));
                    previousTextLocalPosition = HorizonHintUITextList[i][j].GetLocalPosition();
                }
                    
            }
        }

        for (int i = 0; i < VerticalHintUITextList.Count; i++)
        {
            blockPosition = nonoBlockList[i].GetLocalPosition();

            for (int j = 0; j < VerticalHintUITextList[i].Count; j++)
            {
                if(j==0)
                {
                    VerticalHintUITextList[i][j].SetLocalPosition(blockPosition + new Vector2(0, Constants.HINTUIBLOCKOFFSET + blockSize.y));
                    previousTextLocalPosition = VerticalHintUITextList[i][j].GetLocalPosition();
                }
                
                else
                {
                    VerticalHintUITextList[i][j].SetLocalPosition(previousTextLocalPosition + new Vector2(0, Constants.HINTUITEXTOFFSET));
                    previousTextLocalPosition = VerticalHintUITextList[i][j].GetLocalPosition();
                }
                    
            }
        }
    }
}
