using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public static void LoadAllGameData()
    {
        LoadGameData(10);
        LoadGameData(15);
        LoadGameData(20);
        LoadGameData(25);
    }

    public static void LoadGameData(int lineBlockNum)
    {
        string fileName = "gamedatapath" + lineBlockNum.ToString();
        string innerGameDataPath = Path.Combine("Data", fileName);
        var loadData = Resources.Load<TextAsset>(innerGameDataPath);

        switch(lineBlockNum)
        {
            case 10:
                NonoBlockPlateInfo.nonoBlockPlateInfoDataList10 = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData.ToString());
                break;
            case 15:
                NonoBlockPlateInfo.nonoBlockPlateInfoDataList15 = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData.ToString());
                break;
            case 20:
                NonoBlockPlateInfo.nonoBlockPlateInfoDataList20 = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData.ToString());
                break;
            case 25:
                NonoBlockPlateInfo.nonoBlockPlateInfoDataList25 = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData.ToString());
                break;
        }
 
    }

    public static void SaveData(List<int> nonoBlockPlateInfo, string name, int lineBlockNum)
    {
        string fileName = "gamedatapath" + lineBlockNum.ToString() + ".json";
        string resourcesPath = Path.Combine(Application.dataPath, "Resources/Data");
        string innerGameDataPath = Path.Combine(resourcesPath, fileName);
       
        NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList = NonoBlockPlateInfo.GetNonoBlockPlateInfoDataList(lineBlockNum);
        NonoBlockPlateInfoData nonoBlockPlateInfoData = NonoBlockPlateInfo.MakeNonoBlockPlateInfo(nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Count, name, nonoBlockPlateInfo);
        bool overrideExistNonoBlockPlate = false;

        if(CheckDuplicates(nonoBlockPlateInfoDataList, nonoBlockPlateInfoData))
        {
            for(int i = 0; i < nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Count; i++)
            {
                if(nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i].name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
                {
                    nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i].nonoBlockPlateInfo = nonoBlockPlateInfo;
                    overrideExistNonoBlockPlate = true;
                    break;
                }
            }
            if(!overrideExistNonoBlockPlate)
                nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Add(nonoBlockPlateInfoData);

            string saveData = JsonUtility.ToJson(NonoBlockPlateInfo.nonoBlockPlateInfoDataList10);
            File.WriteAllText(innerGameDataPath, saveData);
            Debug.Log("파일을 정상적으로 저장했습니다.");
        }

        else
        {
            Debug.Log("중복되는 노노그램이 있습니다.");
        }
        
    }

    private static bool CheckDuplicates(NonoBlockPlateInfoDataList nonoBlockPlateInfoDataList, NonoBlockPlateInfoData nonoBlockPlateInfoData)
    {
        for (int i = 0; i < nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas.Count; i++) 
        {
            if(nonoBlockPlateInfoDataList.nonoBlockPlateInfoDatas[i] == nonoBlockPlateInfoData)
            {
                return false;
            }
        }

        return true;
    }
}
