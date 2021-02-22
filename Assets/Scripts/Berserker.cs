﻿using System.Collections;
using UnityEngine;

public class Berserker : Log
{
    // Start is called before the first frame update
    public bool berserker;
    public int cpBaseAttack;
    public float cpMoveSpeed;
    public int bkBaseAttack;
    public float bkMoveSpeed;
    public Stats playerStats;
    public float timer;

    protected override void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //video 18
        target = GameObject.FindWithTag("Player").transform;
        berserker = false;
        cpBaseAttack = baseAttack;
        cpMoveSpeed = moveSpeed;
        bkBaseAttack = baseAttack * 2;
        bkMoveSpeed = moveSpeed * 2;
    }

    void Update()
    {
        ChangeStat();
        if (berserker)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                berserker = false;
                timer = 10f;
            }
        } else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                berserker = true;
                timer = 10f;
            }
        }
    }

    public void ChangeStat()
    {
        if (berserker)
        {
            baseAttack = 2;
            moveSpeed = 3;
        }
        else
        {
            baseAttack = 1;
            moveSpeed = 1.5f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("attack"))
        {
            float damage = other.GetComponent<PlayerHit>().damage;

            if (gameObject != null)
            {
                if (berserker) {
                    TakeDamage(damage / 2);
                } else
                {
                    TakeDamage(damage);
                }
            }
        }
    }

    protected override void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            playerStats.increaseStats();
            Destroy(gameObject);
        }
    }

    public override IEnumerator changeStateCo()
    {
        if (berserker)
        {
            berserker = false;
            timer = 10f;
            currentState = EnemyState.stagger;
            yield return new WaitForSeconds(0.75f);
        } else
        {
            currentState = EnemyState.stagger;
            yield return new WaitForSeconds(1.5f);
        }
        currentState = EnemyState.idle;
    }
}