using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    [SerializeField]
    NonogramButton nonogramButtonPrefab;

    [SerializeField]
    Image backgroundImage;

    [SerializeField]
    Transform nonogramButtonLayout;

    [Header("----------------------------")]
    [SerializeField]
    Color Color10x10;
    [SerializeField]
    Color Color15x15;
    [SerializeField]
    Color Color20x20;
    [SerializeField]
    Color Color25X25;

    private List<NonogramButton> nonogramButtonList = new List<NonogramButton>();

    private void Awake()
    {
        for (int i =0; i < 20; i++) 
        {
            nonogramButtonList.Add(Instantiate(nonogramButtonPrefab, nonogramButtonLayout));
        }
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
