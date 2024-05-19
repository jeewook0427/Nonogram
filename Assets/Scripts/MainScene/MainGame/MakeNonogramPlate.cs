using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class MakeNonogramPlate : MonoBehaviour
{
    [SerializeField]
    private PlayHintUI playHintUI;

    [SerializeField]
    private NonoBlock nonoBlockPrefab;

    [SerializeField]
    private Transform backgroundPlate;

    [SerializeField]
    private Transform blockParents;

    //----------------------------------------------------------

    private float horizonLength = 800;
    private float verticalLength = 800;

    private float lineThicknessValue = 4;

    private List<NonoBlock> nonoBlockList;
    private NonoBlock touchSelectFirstBlock;
    private NonoBlock currentBlock;
    private NonoBlock previousBlock;
    private List<NonoBlock> touchSelectBlockList;
    private List<NonoBlock> allBlockList;
    private NonoBlockPlateInfoData currentNonoBlockPlateInfoData;

    private int horizonBlockNum;
    private int verticalBlockNum;

    private bool firstBlockState;

    void Awake()
    {

    }
    public void Init()
    {
        allBlockList = new List<NonoBlock>();
        nonoBlockList = new List<NonoBlock>();
        touchSelectBlockList = new List<NonoBlock>();

        MakeNonoBlocks();
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

        horizonBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfoData.Count);
        verticalBlockNum = (int)Mathf.Sqrt(nonoBlockPlateInfoData.nonoBlockPlateInfoData.Count);

        NonoBlock nonoBlock;
        RectTransform backgroundPlateRectTrans = backgroundPlate.GetComponent<RectTransform>();

        horizonLength = backgroundPlateRectTrans.sizeDelta.x;
        verticalLength = backgroundPlateRectTrans.sizeDelta.y;

        float horizonInterval = horizonLength / (float)horizonBlockNum;
        float verticalInterval = verticalLength / (float)verticalBlockNum;

        float horizonOffset = (horizonInterval - horizonLength) * 0.5f;
        float verticalOffset = (-verticalInterval + verticalLength) * 0.5f;

        float innerBlockWidth = horizonInterval - lineThicknessValue;
        float innerBlockHeight = verticalInterval - lineThicknessValue;

        for (int i = 0; i < verticalBlockNum; i++)
        {
            for (int j = 0; j < horizonBlockNum; j++)
            {
                nonoBlock = allBlockList[i * horizonBlockNum + j];
                nonoBlock.Init(new Vector2(innerBlockWidth, innerBlockHeight), new Vector2(horizonInterval, verticalInterval), 
                    new Vector2(horizonInterval * j + horizonOffset, -verticalInterval * i + verticalOffset), j, i);
                nonoBlockList.Add(nonoBlock);
                nonoBlock.gameObject.SetActive(true);
            }
        }
    }

    //MouseButtonDown
    public void TouchSelectFirstBlock(NonoBlock selectBlock)
    {
        touchSelectFirstBlock = selectBlock;
        firstBlockState = touchSelectFirstBlock.GetBlockState();
        SetSelectedBlockList(true, 0);
        ChangeSelectedBlocksState(!firstBlockState);
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

            ChangeSelectedBlocksState(!firstBlockState);
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
    }

    private void ChangeSelectedBlocksState(bool fillBlock)
    {
        foreach (var block in touchSelectBlockList)
        {
            block.ChangeBlockState(fillBlock);
        }
    }

    private void SetSelectedBlockList(bool xDirection, int selectBlockNum)
    {
        touchSelectBlockList.Clear();
        int startCord = touchSelectFirstBlock.GetCord().X + horizonBlockNum * touchSelectFirstBlock.GetCord().Y;

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
                    touchSelectBlockList.Add(nonoBlockList[startCord + horizonBlockNum * i]);
                }
            }
            else
            {
                selectBlockNum -= 1;
                for (int i = 0; i > selectBlockNum; i--)
                {
                    touchSelectBlockList.Add(nonoBlockList[startCord + horizonBlockNum * i]);
                }
            }

        }
    }
}
