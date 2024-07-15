using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;

    public float maxStamina = 5f;
    private float currentStamina;
    public float staminaDrainRate = 1f;
    public float staminaRecoveryRate = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        
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
    
    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}
