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

    private MainGame mainGame;
    private MainUI mainUI; 
 
    void Awake()
    {
        mainGame = Instantiate(MainGamePrefab, MainCanvas);
        mainUI = Instantiate(MainUIPrefab, MainCanvas);
    }
    void Start()
    {
        
    }

    void Init()
    {
        mainGame.Init();
        mainUI.Init();
    }
}
