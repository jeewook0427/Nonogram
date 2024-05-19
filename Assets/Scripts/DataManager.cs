using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    string applicationSavePath;
    string gameDataPath;
    string userDataPath;

    void Awake()
    {
        string jsonFilePath = "Data";
        applicationSavePath = Application.dataPath;

        gameDataPath = Path.Combine(jsonFilePath, "gamedatapath");
        userDataPath = Path.Combine(applicationSavePath, "userdatapath.json");

        LoadGameData();
    }
    void Start()
    {
        
    }
    public void Init()
    {
        
    }

    private void LoadGameData()
    {
        var loadData = Resources.Load<TextAsset>(gameDataPath);
        NonoBlockPlateInfo.nonoBlockPlateInfoDataList = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData.ToString());
    }

    private void LoadUserData()
    {

    }

    private void LoadData(string filePath)
    {
        if (File.Exists(filePath))
        {
            string loadData = File.ReadAllText(filePath);
            NonoBlockPlateInfo.nonoBlockPlateInfoDataList = JsonUtility.FromJson<NonoBlockPlateInfoDataList>(loadData);
        }

        else
            Debug.Log("파일이 존재하지 않습니다.");
    }

    private void SaveData()
    {
        string saveData = JsonUtility.ToJson(NonoBlockPlateInfo.nonoBlockPlateInfoDataList);
        File.WriteAllText(gameDataPath, saveData);
    }

    public void SaveCurrentNonoPlateInfo()
    {

    }
}
