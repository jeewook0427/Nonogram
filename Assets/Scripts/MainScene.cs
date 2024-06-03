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

        DataManager.LoadAllGameData();
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        BindFunction();

        mainGame.Init();
        mainUI.Init();
    }

    void BindFunction()
    {
        SelectButton[] buttons = mainUI.PlayScreenUI.GetSelectButtons();

        foreach (var button in buttons)
        {
            button.ChangeButtonStateDelegate = mainGame.makeNonogramPlate.ChangeButtonState;
        }

        mainUI.PlayScreenUI.joyStick.moveCurrentBlockDelegate = mainGame.makeNonogramPlate.MoveCurrentBlock;
    }
}
