using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField]
    private MainGame MainGamePrefab;
    
    [SerializeField]
    private MainUI MainUIPrefab;

    [SerializeField]
    private Transform MainCanvas;

    [SerializeField]
    private DataManager fileManager;

    private MainGame mainGame;
    private MainUI mainUI; 
 
    void Awake()
    {
        mainGame = Instantiate(MainGamePrefab, MainCanvas);
        mainUI = Instantiate(MainUIPrefab, MainCanvas);
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        mainGame.Init();
        mainUI.Init();
        fileManager.Init();
    }
}
