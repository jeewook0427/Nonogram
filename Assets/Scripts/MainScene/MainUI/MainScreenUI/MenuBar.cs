using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuBar : MonoBehaviour
{
    public System.Action<int> ChangeMenuDelegate;
   
    [SerializeField]
    private GameObject dim;

    Button button;
    RectTransform rectTransform;
    int index;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClickSelectButton);
    }

    public void Init(int innerIndex)
    {
        index = innerIndex;
    }

    public void Reset()
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
        dim.SetActive(true);
    }
    private void OnClickSelectButton()
    {
        dim.SetActive(false);
        rectTransform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        ChangeMenuDelegate.Invoke(index);
    }
}
