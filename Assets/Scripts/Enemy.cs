using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{ 
    public Transform _player;
    private BoxCollider _boxCollider;
    
    public float minimumDistance = 2f; // Oyuncudan minimum mesafe
    private bool _isAttacking = false; 
  
    protected override void Start()
    {
        base.Start();
        
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    protected override void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_player == null)
        {
           
            return;
        }

        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

  
        if (!_isAttacking) 
        {
            transform.Translate(directionToPlayer * (moveSpeed * Time.deltaTime), Space.World);
    
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.15f);
        }

  
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer < attackDistance && distanceToPlayer > minimumDistance && !_isAttacking)
        {
            PerformAttack();
        }
        else
        {
            PerformMovement();
        }
    }

    private void PerformAttack()
    {
        _animator.SetBool("Attack", true);
        _animator.SetBool("Run", false);
        _isAttacking = true; 
        Debug.Log("Düşman saldırıyor!");
    }

    private void PerformMovement()
    {
        _animator.SetBool("Run", true);
    }


    public void EndAttackAnimation()
    {
        _isAttacking = false; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
    
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player != null)
            {
                player.TakeDamage(attackDamage);
      
            }
        }
        
    }
    
    
}

