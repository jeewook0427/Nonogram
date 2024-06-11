using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private MainScreenUI mainScreenUIPrefab;
    [SerializeField]
    private PlayScreenUI playScreenUIPrefab;

    [HideInInspector]
    public PlayScreenUI PlayScreenUI;
    [HideInInspector]
    public MainScreenUI mainScreenUI;

    private void Awake()
    {
        mainScreenUI = Instantiate(mainScreenUIPrefab, this.transform);
        PlayScreenUI = Instantiate(playScreenUIPrefab, this.transform);
    }

    public void Init()
    {
        mainScreenUI.Init();
        PlayScreenUI.Init();

        ShowMainScreen();
    }

    public void ShowMainScreen()
    {
        mainScreenUI.gameObject.SetActive(true);
        PlayScreenUI.gameObject.SetActive(false);
    }

    public void ShowPlayScreen()
    {
        mainScreenUI.gameObject.SetActive(false);
        PlayScreenUI.gameObject.SetActive(true);
    }
}
