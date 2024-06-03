using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Direction
{
    UP,
    DOWN, 
    LEFT, 
    RIGHT
}
public class JoyStick : MonoBehaviour
{
    public System.Action<Direction> moveCurrentBlockDelegate;

    private DirectionButton[] directionButtons;

    private void Awake()
    {
        directionButtons = GetComponentsInChildren<DirectionButton>();
    }

    public void Init()
    {
        foreach (DirectionButton button in directionButtons)
        {
            button.moveCurrentBlockDelegate = moveCurrentBlockDelegate;
        }
    }


}
