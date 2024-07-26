using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        current = this;
    }

    public event Action onPlayerFirstMove;

    public void MovementTrigger()
    {
        if (onPlayerFirstMove != null)
        {
            onPlayerFirstMove();
            Debug.Log("player move");
            {
                
            }
        }
    }
}
