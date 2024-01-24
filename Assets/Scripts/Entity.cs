using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected Animator _animator;
    protected Player player;
    protected Enemy enemy;

    [Header("Health info")]
    public int maxHealth;
    public int currentHealth;

    [Header("Damage info")]
    [SerializeField] protected float attackDistance = 10f;
    [SerializeField] protected int attackDamage = 1;
    
    [Header("Move info")]
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected float rotationSpeed = 5;

    [Header("Set Score")] 
    [SerializeField] protected int enemyScore;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        
        currentHealth = maxHealth;
        
    }


    protected virtual void Update()
    {
        
    }
   
    public void TakeDamage(int damageAmount)
    {
        
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            
            Die();
        }

        if (player.currentHealth <= 2)
        {
            SceneManager.LoadScene("Scene1");
            ScoreManager.Instance.playerScore = 0;
            Destroy(gameObject);
            
        }
   
    }

  
    protected virtual void Die()
    {
        if (_animator != null)
        {
            
        }
        ScoreManager.Instance.EnemyScore(enemyScore);
        Destroy(gameObject);
    }
    
    
}
