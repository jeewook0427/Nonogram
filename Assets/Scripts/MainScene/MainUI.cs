using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private PlayScreenUI playScreenUIPrefab;

    [HideInInspector]
    public PlayScreenUI PlayScreenUI;

    private void Awake()
    {
        PlayScreenUI = Instantiate(playScreenUIPrefab, this.transform);
    }

    public void Init()
    {
        PlayScreenUI.Init();
    }
}
