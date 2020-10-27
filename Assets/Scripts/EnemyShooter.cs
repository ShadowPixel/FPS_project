using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform playerToFollow = null;
    [SerializeField] Rigidbody playerRigidbody = null;
    public Vector3 moveDirection;
    public Rigidbody bodyToPushBack;

    int health = 100;

    private void Start()
    {
        bodyToPushBack = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookAtPlayer();
    }
    
    public void TakeDamage(int _damageToTake)
    {
        health -= _damageToTake;
        UnityEngine.Debug.Log(health + " health left");

        if (health == 0)
        {
            UnityEngine.Debug.Log("Enemy killed!");
            gameObject.SetActive(false);
        }
    }

    public void TakeKnockback(float _pushBack)
    {
        moveDirection = bodyToPushBack.transform.position - playerRigidbody.transform.position;
        bodyToPushBack.AddForce(moveDirection.normalized * _pushBack);
    }

    void LookAtPlayer()
    {
        transform.LookAt(playerToFollow);
    }
}
