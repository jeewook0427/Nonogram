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

    private float horizonLength = 800;
    private float verticalLength = 800;

    private float lineThicknessValue = 4;

    private List<NonoBlock> nonoBlockList = new List<NonoBlock>();
    private List<int> userAnswerList = new List<int>();
    private NonoBlock touchSelectFirstBlock;
    private NonoBlock currentBlock;
    private NonoBlock previousBlock;
    private List<NonoBlock> touchSelectBlockList = new List<NonoBlock>();
    private List<NonoBlock> allBlockList = new List<NonoBlock>();
    private NonoBlockPlateInfoData currentNonoBlockPlateInfoData;

    private int lineBlockNum;

    private int firstBlockState;

    void Awake()
    {

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
    public void MakeNonoGram(NonoBlockPlateInfoData nonoBlockPlateInfoData)
    {
        for (int i = 0; i < Constants.MAXBLOCKCOUNT_Y * Constants.MAXBLOCKCOUNT_X; i++)
        {
            allBlockList[i].gameObject.SetActive(false); 
        }

        currentNonoBlockPlateInfoData = nonoBlockPlateInfoData;

        lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfoData.Count);
        lineBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfoData.Count);

        NonoBlock nonoBlock;
        RectTransform backgroundPlateRectTrans = backgroundPlate.GetComponent<RectTransform>();

        horizonLength = backgroundPlateRectTrans.sizeDelta.x;
        verticalLength = backgroundPlateRectTrans.sizeDelta.y;

        float horizonInterval = horizonLength / (float)lineBlockNum;
        float verticalInterval = verticalLength / (float)lineBlockNum;

        float horizonOffset = (horizonInterval - horizonLength) * 0.5f;
        float verticalOffset = (-verticalInterval + verticalLength) * 0.5f;

        float innerBlockWidth = horizonInterval - lineThicknessValue;
        float innerBlockHeight = verticalInterval - lineThicknessValue;

        for (int i = 0; i < lineBlockNum; i++)
        {
            for (int j = 0; j < lineBlockNum; j++)
            {
                nonoBlock = allBlockList[i * lineBlockNum + j];
                nonoBlock.Init(new Vector2(innerBlockWidth, innerBlockHeight), new Vector2(horizonInterval, verticalInterval), 
                    new Vector2(horizonInterval * j + horizonOffset, -verticalInterval * i + verticalOffset), j, i);
                nonoBlockList.Add(nonoBlock);
                userAnswerList.Add(0); // 유저정답 초기값 0을 넣어준다.
                nonoBlock.gameObject.SetActive(true);
            }
        }

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
        bool CheckAnswer = puzzleVerifier.CheckUserAnswer(userAnswerList, playHintUI.HorizonHintUITextList, playHintUI.VerticalHintUITextList);

        Debug.Log(CheckAnswer);
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
