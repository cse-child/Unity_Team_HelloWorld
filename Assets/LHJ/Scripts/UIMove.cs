using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClick = false;
    Vector3 clickPos = new Vector3(0,0,0);
    private GameObject parent;

    private void Awake()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        clickPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
        clickPos = new Vector3(0, 0,0);
    }

    public void Update()
    {
        if(isClick == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 0;
            parent.transform.position += Input.mousePosition - clickPos;
            clickPos = temp;
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Mouse0) && isClick == true)
    //        isClick = false;
    //}

}
