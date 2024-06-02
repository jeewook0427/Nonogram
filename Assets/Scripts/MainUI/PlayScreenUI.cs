using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayScreenUI : MonoBehaviour
{
    [SerializeField]
    private GameObject selectButton;

    private SelectButton[] selectButtons;

    private void Awake()
    {
        selectButtons = selectButton.GetComponentsInChildren<SelectButton>();
    }

    public void Init()
    {
        foreach (var button in selectButtons) 
        {
            button.Init();
        }
        BindFunction();
    }

    private void BindFunction()
    {
        foreach (var button in selectButtons)
        {
            button.ChangeSelectedButtonDelegate = ChangeSelectedButton;
        }
    }
    private void ChangeSelectedButton(SelectButton selectButton)
    {
        foreach (var button in selectButtons)
        {
            if (button == selectButton)
                button.SetActiveCheckMark(true);
            else
                button.SetActiveCheckMark(false);
        }
    }
    public SelectButton[] GetSelectButtons()
    {
        return selectButtons;
    }
}
