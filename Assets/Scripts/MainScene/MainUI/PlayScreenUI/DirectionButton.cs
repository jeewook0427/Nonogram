using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DirectionButton : MonoBehaviour
{
    public System.Action<Direction> moveCurrentBlockDelegate;

    [SerializeField]
    private Direction direction;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickDirectionButton);
    }

    private void OnClickDirectionButton()
    {
        moveCurrentBlockDelegate.Invoke(direction);
    }

}
