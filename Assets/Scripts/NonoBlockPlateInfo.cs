using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NonoBlockPlateInfoDataList
{
    public List<NonoBlockPlateInfoData> nonoBlockPlateInfoDatas = new List<NonoBlockPlateInfoData>();
}

[System.Serializable]
public class NonoBlockPlateInfoData
{
    public int id;
    public string name;
    public List<int> nonoBlockPlateInfo= new List<int>();
    public bool isComplete;
}
//게임판 이름, 배열 등등을 담고 있는 클래스
public class NonoBlockPlateInfo
{
    public static NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList10;
    public static NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList15;
    public static NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList20;
    public static NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList25;
    public static NonoBlockPlateInfoData MakeNonoBlockPlateInfo(int id, string name, List<int> nonoBlockPlateValue)
    {
        NonoBlockPlateInfoData nonoBlockPlateInfoData = new NonoBlockPlateInfoData();
        nonoBlockPlateInfoData.id = id;
        nonoBlockPlateInfoData.name = name;
        nonoBlockPlateInfoData.nonoBlockPlateInfo = nonoBlockPlateValue;

        return nonoBlockPlateInfoData;
    }

    public static NonoBlockPlateInfoData GetNonoBlockPlateInfoData(int lineBlockNum, string name)
    {
        NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList = GetNonoBlockPlateInfoDataList(lineBlockNum);

        if(nonoBlockPlateInfoDataList == null) { return null; }

        for (int i = 0; i < nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Count; i++) 
        {
            if (nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i].name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
            {
                return nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i];
            }
        }

        return null;
    }

    public static NonoBlockPlateInfoDataList GetNonoBlockPlateInfoDataList(int lineBlockNum)
    {
        NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList = new NonoBlockPlateInfoDataList();
        switch (lineBlockNum)
        {
            case 10:
                nonoBlockPlateInfoDataList = NonoBlockPlateInfo.nonoBlockPlateInfoDataList10;
                break;
            case 15:
                nonoBlockPlateInfoDataList = NonoBlockPlateInfo.nonoBlockPlateInfoDataList15;
                break;
            case 20:
                nonoBlockPlateInfoDataList = NonoBlockPlateInfo.nonoBlockPlateInfoDataList20;
                break;
            case 25:
                nonoBlockPlateInfoDataList = NonoBlockPlateInfo.nonoBlockPlateInfoDataList25;
                break;
        }

        return nonoBlockPlateInfoDataList;
    }
}
