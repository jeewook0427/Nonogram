using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class MakeNonogramPlate : MonoBehaviour
{
    [SerializeField]
    private NonoBlock nonoBlockPrefab;

    [SerializeField]
    private PlayHintUI playHintUI;

    [SerializeField]
    private PuzzleVerifier puzzleVerifier;

    [SerializeField]
    private Transform backgroundPlate;

    [SerializeField]
    private Transform blockParents;

    //----------------------------------------------------------

    [HideInInspector]
    public List<int> userAnswerList = new List<int>();

    private float horizonLength = 800;
    private float verticalLength = 800;
    private float lineThicknessValue = 3;

    private List<NonoBlock> nonoBlockList           = new List<NonoBlock>();
    private List<NonoBlock> touchSelectBlockList    = new List<NonoBlock>();
    private List<NonoBlock> allBlockList            = new List<NonoBlock>();

    private NonoBlock touchSelectFirstBlock;
    private NonoBlock currentBlock;
    private NonoBlock previousBlock;
   
    private NonoBlockPlateInfoData currentNonoBlockPlateInfoData;

    private RectTransform rectTransform;

    private int lineBlockNum;
    private int firstBlockState;

    private bool isMakerPlate;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Init()
    {
        BindDelegate();

        playHintUI.Init();
        MakeNonoBlocks();
    }
    public void BindDelegate()
    {
        puzzleVerifier.MakeHorizonAnswerListDelegate = playHintUI.MakeHorizonAnswerList;
        puzzleVerifier.MakeVerticalAnswerListDelegate = playHintUI.MakeVerticalAnswerList;
    }

    public void MakeNonoBlocks()
    {
        NonoBlock nonoBlock;

        for (int i = 0; i < Constants.MAXBLOCKCOUNT_Y; i++)
        {
            for (int j = 0; j < Constants.MAXBLOCKCOUNT_X; j++)
            {
                nonoBlock = Instantiate(nonoBlockPrefab, blockParents);
                allBlockList.Add(nonoBlock);
                nonoBlock.gameObject.SetActive(false);
            }
        }
    }

    //노노그램 게임판을 만든다.
    public void MakeNonoGram(NonoBlockPlateInfoData nonoBlockPlateInfoData, int innerLineBlockNum = 0, bool innerIsMakerPlate = false)
    {
        for (int i = 0; i < Constants.MAXBLOCKCOUNT_Y * Constants.MAXBLOCKCOUNT_X; i++)
        {
            allBlockList[i].gameObject.SetActive(false); 
        }

        currentNonoBlockPlateInfoData = nonoBlockPlateInfoData;

        if (nonoBlockPlateInfoData == null && !innerIsMakerPlate)
            return;

        if(innerIsMakerPlate)
        {
            isMakerPlate = innerIsMakerPlate;
            lineBlockNum = innerLineBlockNum;
        }

        else
        {
            lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfo.Count);
            lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfo.Count);
        }
            
        NonoBlock nonoBlock;
        RectTransform backgroundPlateRectTrans = backgroundPlate.GetComponent<RectTransform>();

        horizonLength = Constants.BACKGROUNDWIDTH;
        verticalLength = Constants.BACKGROUNDWIDTH;

        float additionalLineThickness = 8f;

        float horizonInterval = horizonLength / (float)lineBlockNum;
        float verticalInterval = verticalLength / (float)lineBlockNum;

        float horizonOffset = (horizonLength - horizonInterval) * 0.5f;
        float verticalOffset = (verticalLength - verticalInterval) * 0.5f;

        float innerBlockWidth = horizonInterval - lineThicknessValue;
        float innerBlockHeight = verticalInterval - lineThicknessValue;
        float additionalWidth = (lineBlockNum / 5 + 1) * additionalLineThickness + 2 * lineThicknessValue;

        Vector2 preBlockPosition = new Vector2(0, -(lineThicknessValue + additionalLineThickness)) + new Vector2(0, verticalOffset + additionalWidth);
        Vector2 currentBlockPosition = Vector2.zero;

        for (int i = 0; i < lineBlockNum; i++)
        {
            preBlockPosition.x = ((lineThicknessValue + additionalLineThickness) - horizonOffset - additionalWidth);
            
            if(i !=0)
                preBlockPosition += new Vector2(0, -verticalInterval);

            if ((i + 1) % 5 == 1 && i != 0)
                preBlockPosition += new Vector2(0, -additionalLineThickness);

            for (int j = 0; j < lineBlockNum; j++)
            {
                nonoBlock = allBlockList[i * lineBlockNum + j];
               
                if((j +1) % 5 == 1 && j != 0)
                {
                    preBlockPosition += new Vector2(additionalLineThickness, 0);
                }

                if (j == 0)
                {
                    currentBlockPosition = preBlockPosition; 
                }
                  
                else
                {
                   currentBlockPosition = preBlockPosition + new Vector2(horizonInterval, 0);
                }

                nonoBlock.Init(new Vector2(innerBlockWidth, innerBlockHeight), new Vector2(horizonInterval - lineThicknessValue, verticalInterval - lineThicknessValue),
                  currentBlockPosition, j, i);

                nonoBlockList.Add(nonoBlock);
                userAnswerList.Add(0); // 유저정답 초기값 0을 넣어준다.

                preBlockPosition = currentBlockPosition;
                nonoBlock.gameObject.SetActive(true);
            }
        }

        backgroundPlateRectTrans.sizeDelta = new Vector2(horizonLength + additionalWidth, verticalLength + additionalWidth);
        backgroundPlateRectTrans.anchoredPosition = new Vector2(-additionalWidth * 0.5f, additionalWidth * 0.5f);

        rectTransform.anchoredPosition = new Vector2(-(horizonLength) * 0.5f - 20f ,100f);

        if (!innerIsMakerPlate)
            playHintUI.MakeHintUI(nonoBlockPlateInfoData, nonoBlockList);
    }

    //MouseButtonDown
    public void TouchSelectFirstBlock(NonoBlock selectBlock)
    {
        touchSelectFirstBlock = selectBlock;
        firstBlockState = touchSelectFirstBlock.GetBlockState();
        SetSelectedBlockList(true, 0);
        ChangeSelectedBlocksState(Math.Abs(firstBlockState - 1));
    }

    //MouseButton
    public void TouchSelectCurrentBlock(NonoBlock selectBlock)
    {
        if (!selectBlock)
        {
            return;
        }

        currentBlock = selectBlock;
        int SelectedBlockNum;
        if (previousBlock != currentBlock)
        {
            if (currentBlock.GetCord().X == touchSelectFirstBlock.GetCord().X)
            {
                SelectedBlockNum = currentBlock.GetCord().Y - touchSelectFirstBlock.GetCord().Y;
                SetSelectedBlockList(false, SelectedBlockNum);
            }

            else if (currentBlock.GetCord().Y == touchSelectFirstBlock.GetCord().Y)
            {
                SelectedBlockNum = currentBlock.GetCord().X - touchSelectFirstBlock.GetCord().X;
                SetSelectedBlockList(true, SelectedBlockNum);
            }

            ChangeSelectedBlocksState(Math.Abs(firstBlockState - 1));
        }

        previousBlock = currentBlock;
    }

    //MouseButtonUp
    public void TouchSelectEndBlock()
    {
        touchSelectBlockList.Clear();
        touchSelectFirstBlock = null;
        previousBlock = null;
        currentBlock = null;

        // 컨트롤이 끝난 후 유저 정답 저장
        SynchronizeUserAnswer();

        if(isMakerPlate)
           return;
        
        bool CheckAnswer = puzzleVerifier.CheckUserAnswer(userAnswerList, playHintUI.HorizonHintUITextList, playHintUI.VerticalHintUITextList);
    }

    private void SynchronizeUserAnswer()
    {
        for (int i = 0; i < lineBlockNum; i++)
        {
            for (int j = 0; j < lineBlockNum; j++)
            {
                userAnswerList[i * lineBlockNum + j] = nonoBlockList[i * lineBlockNum + j].GetBlockState();
            }
        }     
    }

    private void ChangeSelectedBlocksState(int blockState)
    {
        foreach (var block in touchSelectBlockList)
        {
            block.ChangeBlockState(blockState);
        }
    }

    private void SetSelectedBlockList(bool xDirection, int selectBlockNum)
    {
        touchSelectBlockList.Clear();
        int startCord = touchSelectFirstBlock.GetCord().X + lineBlockNum * touchSelectFirstBlock.GetCord().Y;

        if (xDirection)
        {
            if (selectBlockNum >= 0)
            {
                selectBlockNum += 1;
                for (int i = startCord; i < startCord + selectBlockNum; i++)
                {
                    touchSelectBlockList.Add(nonoBlockList[i]);
                }
            }

            else
            {
                selectBlockNum -= 1;
                for (int i = startCord; i > startCord + selectBlockNum; i--)
                {
                    touchSelectBlockList.Add(nonoBlockList[i]);
                }
            }

        }

        else
        {
            if (selectBlockNum >= 0)
            {
                selectBlockNum += 1;
                for (int i = 0; i < selectBlockNum; i++)
                {
                    touchSelectBlockList.Add(nonoBlockList[startCord + lineBlockNum * i]);
                }
            }
            else
            {
                selectBlockNum -= 1;
                for (int i = 0; i > selectBlockNum; i--)
                {
                    touchSelectBlockList.Add(nonoBlockList[startCord + lineBlockNum * i]);
                }
            }

        }
    }
}
