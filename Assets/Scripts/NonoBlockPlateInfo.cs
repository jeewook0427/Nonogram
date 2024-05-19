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
    public string name;
    public List<int> nonoBlockPlateInfoData = new List<int>();
    public bool isComplete;
}
//게임판 이름, 배열 등등을 담고 있는 클래스
public class NonoBlockPlateInfo
{
    public static NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList;
    public static NonoBlockPlateInfoData MakeNonoBlockPlateInfo(string name, List<int> nonoBlockPlateValue)
    {
        NonoBlockPlateInfoData nonoBlockPlateInfoData = new NonoBlockPlateInfoData();
        nonoBlockPlateInfoData.name = name;
        nonoBlockPlateInfoData.nonoBlockPlateInfoData = nonoBlockPlateValue;

        return nonoBlockPlateInfoData;
    }

    public static NonoBlockPlateInfoData GetNonoBlockPlateInfoData(string name) 
    {
        for(int i = 0; i < nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Count; i++)
        {
           if(nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i].name == name)
           {
                return nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i];
           }
        }

        return null;
    }

    public static NonoBlockPlateInfoDataList GetNonoBlockPlateInfoDataList() { return nonoBlockPlateInfoDataList;}

}
