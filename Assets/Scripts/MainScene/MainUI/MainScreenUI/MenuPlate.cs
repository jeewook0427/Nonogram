using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    [SerializeField]
    Image backgroundImage;

    [Header("----------------------------")]
    [SerializeField]
    Color Color10x10;
    [SerializeField]
    Color Color15x15;
    [SerializeField]
    Color Color20x20;
    [SerializeField]
    Color Color25X25;

    private void Awake()
    {
        
    }

    public void Init()
    {
        
    }

    public void ChangeBackgroundColor(int index)
    {
        switch(index)
        {
            case 0:
                backgroundImage.color = Color10x10;
                break;
            case 1:
                backgroundImage.color = Color15x15;
                break;
            case 2:
                backgroundImage.color = Color20x20;
                break;
            case 3:
                backgroundImage.color = Color25X25;
                break;
        }
    }
}
