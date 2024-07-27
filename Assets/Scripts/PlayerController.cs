using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -20f;
    public float stairClimbSpeed = 2f;

    public float maxStamina = 5f;
    private float currentStamina;
    public float staminaDrainRate = 1f;
    public float staminaRecoveryRate = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
       EventManager.Instance.CompleteObjective();
    }

    void Update()
    {
        if (velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
        float speed = isSprinting ? sprintSpeed : walkSpeed;
        
        controller.Move(move * speed * Time.deltaTime);

        // Stamina
        if (isSprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
       
    }
    

    //function for UI
    public float GetCurrentStamina()
    {
        return currentStamina;
    }
    
}
