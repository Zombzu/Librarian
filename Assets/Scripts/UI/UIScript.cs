using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider staminaSlider; 
    public PlayerController playerController; 
    public float hideDelay = 2f; 

    private Coroutine hideCoroutine;
    public Color normalColor = Color.white; 
    public Color lowStaminaColor = Color.red; 
    public float lowStaminaThreshold = 1f;

    private Image _fillImage;
    
    void Start()
    {
        staminaSlider.maxValue = playerController.maxStamina;
        staminaSlider.value = playerController.GetCurrentStamina();
        staminaSlider.gameObject.SetActive(false);
        
        _fillImage = staminaSlider.fillRect.GetComponent<Image>();
        _fillImage.color = normalColor;
    }

   
    void Update()
    {
        staminaSlider.value = playerController.GetCurrentStamina();
        if (Input.GetKey(KeyCode.LeftShift) && playerController.GetCurrentStamina() > 0)
        {
            ShowStaminaSlider();
        }
        
        if (playerController.GetCurrentStamina() <= lowStaminaThreshold)
        {
            _fillImage.color = lowStaminaColor;
        }
        else
        {
            _fillImage.color = normalColor;
        }

    }
    
    private void ShowStaminaSlider()
    {
        staminaSlider.gameObject.SetActive(true);
        
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideStaminaSliderAfterDelay());
    }

    private IEnumerator HideStaminaSliderAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        staminaSlider.gameObject.SetActive(false);
    }
}
