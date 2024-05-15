using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MakeNonogramPlate : MonoBehaviour
{
    [SerializeField]
    private GameObject nonoBlockPrefab;

    [SerializeField]
    private Transform backgroundPlate;

    [SerializeField]
    private Transform blockParents;

    //----------------------------------------------------------

    private float horizonLength = 800;
    private float verticalLength = 800;

    private float lineThicknessValue = 4;
    // Start is called before the first frame update
    void Start()
    {
        MakeBlocks(10,10);
    }
    
    public void MakeBlocks(int horizonBlockNum, int verticalBlockNum)
    {
        GameObject nonoBlock;
        RectTransform nonoBlockRectTrans;
        RectTransform backgroundPlateRectTrans = backgroundPlate.GetComponent<RectTransform>();

        horizonLength = backgroundPlateRectTrans.sizeDelta.x;
        verticalLength = backgroundPlateRectTrans.sizeDelta.y;

        float horizonInterval = horizonLength / (float)horizonBlockNum;
        float verticalInterval = verticalLength / (float)verticalBlockNum;

        float horizonOffset = (horizonInterval - horizonLength) * 0.5f;
        float verticalOffset = (-verticalInterval + verticalLength) * 0.5f;

        float blockWidth = horizonInterval - lineThicknessValue;
        float blockHeight = verticalInterval - lineThicknessValue;

        for (int i = 0; i < verticalBlockNum; i++)
        {
            for (int j = 0; j < horizonBlockNum; j++)
            {
                nonoBlock = Instantiate(nonoBlockPrefab, blockParents);
                nonoBlockRectTrans = nonoBlock.GetComponent<RectTransform>();
                nonoBlockRectTrans.sizeDelta = new Vector2(blockWidth, blockHeight);
                nonoBlock.transform.localPosition = new Vector2(horizonInterval * j + horizonOffset, -verticalInterval * i + verticalOffset);
            }
        }
    }
}
