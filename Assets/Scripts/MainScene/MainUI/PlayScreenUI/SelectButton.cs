using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonState
{
    Answer,
    Marking,
}
public class SelectButton : MonoBehaviour
{
    public System.Action<ButtonState> OnClickSelectButtonDelegate;
    public System.Action<SelectButton> ChangeSelectedButtonDelegate;

    public ButtonState buttonState;

    [SerializeField]
    private GameObject checkMark;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSelectButton);
    }

    public void Init()
    {
        if (buttonState == ButtonState.Answer)
            SetActiveCheckMark(true);
        else
            SetActiveCheckMark(false);
    }

    public void OnClickSelectButton()
    {
        OnClickSelectButtonDelegate.Invoke(buttonState);
        ChangeSelectedButtonDelegate.Invoke(this);
    }

    public void SetActiveCheckMark(bool active)
    {
        checkMark.SetActive(active);
    }
}
