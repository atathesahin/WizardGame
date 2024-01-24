using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{
    public static ShowPoints Instance;
    [SerializeField] private GameObject openMenu;
    
    [SerializeField] private TextMeshProUGUI menuPointsText;
    [SerializeField] private TextMeshProUGUI passivePointsText;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    [SerializeField] private TextMeshProUGUI hpRegPointsText;
    [SerializeField] private TextMeshProUGUI hpRegPriceText;
    [SerializeField] private TextMeshProUGUI attackSpeedPriceText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()    
    {
        ShowPassivePrice();
    }

    private void Update()
    {
        UpdateMenuScoreUI();
        ShowPassiveText();

        if (Input.GetKeyDown(KeyCode.F))
        {
            openMenu.SetActive(true);
 
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                openMenu.SetActive(false);
            }
        }
            
    }

    void UpdateMenuScoreUI()
    {
        menuPointsText.text = "Gold: " + ScoreManager.Instance.playerScore.ToString();
        
    }

    void ShowPassivePrice()
    {
        passivePointsText.text = "Passive price: " + "(" + ScoreManager.Instance.passiveUpgradeCost.ToString() +")" + " gold";
        hpRegPriceText.text = "Passive price: " + "(" + ScoreManager.Instance.passiveHpRegCost.ToString() +")" + " gold";
        attackSpeedPriceText.text = "Passive price: " + "(" + ScoreManager.Instance.passiveAttackCost.ToString() +")" + " gold";
    }

    void ShowPassiveText()
    {
        upgradePointsText.text = "Max and current health will increase +5 : ";
        hpRegPointsText.text = "Hp Reg Cd will decrease -0.05 : ";
        attackSpeedText.text = "Damage will increase +2 and Attack Speed Cd will decrease -0.1: ";
    }
}
