using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 10f;

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            scrollRect.horizontalNormalizedPosition -= scrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            scrollRect.horizontalNormalizedPosition += scrollSpeed * Time.deltaTime;
        }
    }
}
