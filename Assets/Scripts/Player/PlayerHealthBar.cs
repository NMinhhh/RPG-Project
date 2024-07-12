using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private float decreaseTime;
    void Start()
    {
        healthImage.fillAmount = 1;
    }

    public void UpdateHealthBar()
    {
    }

   
    
}
