using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
   public bool _cursorLocked;
    
    void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _cursorLocked = true;
        }
    
    void Update()
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                Hide_ShowMouseCursor();
            }
        }
    
    public void Hide_ShowMouseCursor()
        {
            if(!_cursorLocked)
            {
               Cursor.lockState = CursorLockMode.Locked;
               _cursorLocked = true; 
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                _cursorLocked = false;
            }
         }
       }
