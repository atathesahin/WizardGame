using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthText : MonoBehaviour
{

    private static PlayerHealthText instance;
    public static PlayerHealthText Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHealthText>();
                if (instance == null)
                {
                    GameObject uiManagerObject = new GameObject("UIManager");
                    instance = uiManagerObject.AddComponent<PlayerHealthText>();
                }
            }
            return instance;
        }
    }


    public TextMeshProUGUI playerHealthText;
    
  
    public void UpdatePlayerHealthUI(int health)
    {
        playerHealthText.text = "Health: " + health.ToString();
    }

    
}
