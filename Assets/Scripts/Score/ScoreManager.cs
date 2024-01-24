using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private Player _player;
    private Entity _entity;
    public int passiveUpgradeCost = 100;
    public int passiveMultiplier = 1;
    public int passiveHpRegCost = 250;
    public int playerScore = 0; 
    public int passiveAttackCost = 250;
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

    private void Start()
    {
        

    }

    private void Update()
    {
        _player = FindObjectOfType<Player>();
    }

    public void EnemyScore(int enemyPoint)
    {
        playerScore += enemyPoint;
    }
    public void UpgradePassive()
    {
        if (playerScore >= passiveUpgradeCost)
        {
            playerScore -= passiveUpgradeCost;
            passiveMultiplier += 1;
            _player.maxHealth += 5;
            _player.currentHealth += 5;
            Debug.Log("Pasif yükseltildi! Yeni Pasif Çarpan: x" + passiveMultiplier);
        }
    }

    public void UpgradeHealthReg()
    {
        if (playerScore >= passiveHpRegCost)
        {     
            playerScore -= passiveHpRegCost;
            passiveMultiplier += 1;
            _player.hpReg -= 0.05f;
            
        }
   
    }

    public void AttackSpeed()
    {
        if (playerScore >= passiveAttackCost)
        {     
            playerScore -= passiveAttackCost;
            passiveMultiplier += 1;
            _player.attackInterval -= 0.1f;
            _player.playerDamage += 2;

        }
    }
    public void SpendCoins()
    {
        if (playerScore >= passiveUpgradeCost)
        {
            UpgradePassive();
        }
        else
        {
            Debug.Log("warning");
        }
    }

    
}
