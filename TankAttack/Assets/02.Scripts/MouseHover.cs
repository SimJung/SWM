using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //마우스 커서의 UI 항목에 대한 Hover 여부
    public bool isUIHover = false;

    public void OnPointerEnter (PointerEventData eventData)
    {
        isUIHover = true;
    }

    public void OnPointerExit ( PointerEventData eventData)
    {
        isUIHover = false;
    }
}
