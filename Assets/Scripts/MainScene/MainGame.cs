using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    private MakeNonogramPlate makeNonogramPlatePrefabs;

    private MakeNonogramPlate makeNonogramPlate;

    void Awake()
    {
        makeNonogramPlate = Instantiate(makeNonogramPlatePrefabs, this.transform);
    }

    void Start()
    {
        makeNonogramPlate.MakeNonoBlocks(25,25);
    }

    public void Init()
    {
        
    }
}
