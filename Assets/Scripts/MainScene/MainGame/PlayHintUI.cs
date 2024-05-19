using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHintUI : MonoBehaviour
{
    [SerializeField]
    private HintUIText hintUIText;

    public List<HintUIText> hintUITextList;
    private void Awake()
    {
        for(int i=0; i<500; i++) 
        {
            HintUIText innerHintUIText = Instantiate<HintUIText>(hintUIText, this.transform);
            hintUITextList.Add(innerHintUIText);
        }        
    }

    public void Init()
    {
        
    }
}
