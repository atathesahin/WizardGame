using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Player : Entity
{
    public string enemyTag = "Enemy";
    private CapsuleCollider _capsuleCollider;
    private bool isAttacking = false;
    public float attackInterval = 2;
    private PlayerHealthText _playerHealthText;
    private GameObject targetEnemy;
    public int playerDamage;
    [SerializeField] private VfxManager vfxManager;
    
    private float healTime;
    public float hpReg = 1f;

    protected override void Start()
    {
        base.Start();
        _playerHealthText = GetComponent<PlayerHealthText>();
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        InvokeRepeating("PerformAttack", 0f, attackInterval);
        DontDestroyOnLoad(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        LookAtClosestEnemy();
        HealthBarUpdate();
        HealReg();
       
    }

    void LookAtClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length == 0)
        {
     
            isAttacking = false;
            return;
        }

        targetEnemy = FindClosestEnemy(enemies);


        Vector3 directionToEnemy = (targetEnemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);


        float closestDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
     

        if (closestDistance < attackDistance && !isAttacking)
        { 
                StartAttack();

        }
        else if (closestDistance > attackDistance)
        {
            StopAttack();
        }

    }

    GameObject FindClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        
        return closestEnemy;
    }

    void StartAttack()
    {
            isAttacking = true;
            _animator.SetBool("Attack", true);
    }
    void StopAttack()
    {
        isAttacking = false;
        _animator.SetBool("Attack", false);
        _animator.SetBool("idle",true);
    }

    void PerformAttack()
    {
        
        if (isAttacking && targetEnemy != null)
        {
           
            Attack(targetEnemy);
        }
    }


    void Attack(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(playerDamage);
            
            if (vfxManager != null)
            {
                vfxManager.PlayHitVFX(enemy.transform.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
 

    void HealthBarUpdate()
    {
        PlayerHealthText.Instance.UpdatePlayerHealthUI(currentHealth);
    }

    private void HealReg()
    {
        if (currentHealth <= maxHealth && Time.time > healTime)
        {
            Heal(1);
            healTime = Time.time + hpReg;
    

        }
    }
    private void Heal(int amount)
    {
        if (currentHealth <= 0)
        {
            return;
        }
        
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        
    }
}
