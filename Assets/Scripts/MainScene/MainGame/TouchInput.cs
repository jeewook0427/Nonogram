using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    // Delegate
    public System.Action<NonoBlock> touchSelectFirstBlockDelegate;
    public System.Action<NonoBlock> touchSelectCurrentBlockDelegate;
    public System.Action touchSelectEndtBlockDelegate;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Awake()
    {

    }

    public void Init(GraphicRaycaster canvasGraphicRaycaster, EventSystem canvasEventSystem)
    {
        graphicRaycaster = canvasGraphicRaycaster;
        eventSystem = canvasEventSystem;
        pointerEventData = new PointerEventData(eventSystem);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //�����ȯ�� ��Ʈ��
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    NonoBlock nonoBlock = GetNonoBlockWithTouch(touch.position);

                    if (nonoBlock)
                    {
                        touchSelectFirstBlockDelegate.Invoke(nonoBlock);
                        return;
                    }
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    NonoBlock nonoBlock = GetNonoBlockWithTouch(touch.position);

                    if (nonoBlock)
                    {
                        touchSelectCurrentBlockDelegate.Invoke(nonoBlock);
                        return;
                    }
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    touchSelectEndtBlockDelegate.Invoke();
                }
                return;
            }
        }

#if UNITY_EDITOR
        // ������ȯ�� ��Ʈ��
        if (Input.GetMouseButtonDown(0))
        {
            NonoBlock nonoBlock = GetNonoBlockWithTouch(Input.mousePosition);

            if (nonoBlock)
            {
                touchSelectFirstBlockDelegate.Invoke(nonoBlock);
                return;
            }

        }

        else if (Input.GetMouseButton(0))
        {
            NonoBlock nonoBlock = GetNonoBlockWithTouch(Input.mousePosition);

            if (nonoBlock)
            {
                touchSelectCurrentBlockDelegate.Invoke(nonoBlock);
                return;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            touchSelectEndtBlockDelegate.Invoke();
        }
#endif
        return;

    }

    //���̷� ��� ���� Ŭ���ߴ��� �Ǵ�
    private NonoBlock GetNonoBlockWithTouch(Vector3 mousePosition)
    {
        NonoBlock nonoBlock = null;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        pointerEventData.position = mousePosition;
        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        foreach (var rayResult in raycastResults)
        {
            nonoBlock = rayResult.gameObject.GetComponentInParent<NonoBlock>();

            if(nonoBlock)
            {
                return nonoBlock;
            }
        }

        return nonoBlock;
    }
}
