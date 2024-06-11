using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenUI : MonoBehaviour
{
    [SerializeField]
    MenuBar[] menuBars;

    [SerializeField]
    MenuPlate menuPlate;
    private void Awake()
    {
        
    }

    public void Init()
    {
       for(int i = 0; i < menuBars.Length; i++) 
       {
            menuBars[i].Init(i);
            menuBars[i].ChangeMenuDelegate = ChangeMenu;
       }

        menuPlate.Init();
    }

    private void ChangeMenu(int index)
    {
        menuPlate.ChangeBackgroundColor(index);

        for (int i = 0; i < menuBars.Length; i++)
        {
            if (index != i)
            {
                menuBars[i].Reset();
            }
        }
    }
}
