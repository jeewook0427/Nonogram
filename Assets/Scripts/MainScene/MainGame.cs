using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    private MakeNonogramPlate makeNonogramPlatePrefabs;

    [SerializeField]
    private TouchInput touchInputPrefabs;

    private MakeNonogramPlate makeNonogramPlate;
    private TouchInput touchInput;

    void Awake()
    {
        makeNonogramPlate   = Instantiate(makeNonogramPlatePrefabs, this.transform);
        touchInput          = Instantiate(touchInputPrefabs, this.transform);
    }

    void Start()
    {
        
    }

    public void Init()
    {
        makeNonogramPlate.Init();
        touchInput.Init(GetComponentInParent<GraphicRaycaster>(), GetComponentInParent<EventSystem>());

        BindDelegate();

        makeNonogramPlate.MakeNonoGram(NonoBlockPlateInfo.GetNonoBlockPlateInfoData("Ahhyeon"));
    }

    private void BindDelegate()
    {
        touchInput.touchSelectFirstBlockDelegate    += makeNonogramPlate.TouchSelectFirstBlock;
        touchInput.touchSelectCurrentBlockDelegate  += makeNonogramPlate.TouchSelectCurrentBlock;
        touchInput.touchSelectEndtBlockDelegate     += makeNonogramPlate.TouchSelectEndBlock;
    }
}
