using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NonogramMaker : MonoBehaviour
{
    public MakeNonogramPlate makeNonogramPlate;
    public TouchInput touchInput;

    [Header("LoadNonoPlateInfo")]
    public bool FindExistNonogram =false;
    public int nonogramSize;
    public int nonogramID;
    public string nonogramName;

    [Header("SaveNonoPlateInfo")]
    public int Size = 10;
    public string Name = "";

    void Awake()
    {
        Init();
    }

    void Start()
    {
        MakeNonogram();
    }

    public void Init()
    {
        makeNonogramPlate.Init();
        touchInput.Init(GetComponentInParent<GraphicRaycaster>(), GetComponentInParent<EventSystem>());

        BindDelegate();
        DataManager.LoadAllGameData();
    }

    public void MakeNonogram()
    {
        if (FindExistNonogram)
        {
            NonoBlockPlateInfoData nonoBlockPlateInfoData = NonoBlockPlateInfo.GetNonoBlockPlateInfoData(nonogramSize, nonogramName);
            makeNonogramPlate.MakeNonoGram(nonoBlockPlateInfoData);
        }

        else
        {
            makeNonogramPlate.MakeNonoGram(null, Size, true);
        }
    }

    private void BindDelegate()
    {
        touchInput.touchSelectFirstBlockDelegate += makeNonogramPlate.TouchSelectFirstBlock;
        touchInput.touchSelectCurrentBlockDelegate += makeNonogramPlate.TouchSelectCurrentBlock;
        touchInput.touchSelectEndtBlockDelegate += makeNonogramPlate.TouchSelectEndBlock;
    }

    public void SaveNonoplate(List<int> nonoBlockPlateInfoData)
    {
        DataManager.SaveData(nonoBlockPlateInfoData, Name, Size);
        Name = "";
    }
}

[CustomEditor(typeof(NonogramMaker))]
public class NonogramMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NonogramMaker nonogramMaker = (NonogramMaker)target;

        // 버튼 추가
        if (GUILayout.Button("Save NonoBlock"))
        {
            nonogramMaker.SaveNonoplate(nonogramMaker.makeNonogramPlate.userAnswerList);// 버튼 클릭 시 호출할 메서드
        }
    }
}
