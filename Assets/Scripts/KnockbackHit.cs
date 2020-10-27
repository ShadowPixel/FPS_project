using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHit : MonoBehaviour
{
    public Vector3 moveDirection;
    public Rigidbody bodyToPushBack;

    private void Start()
    {
        bodyToPushBack = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moveDirection = bodyToPushBack.transform.position - other.transform.position;
            bodyToPushBack.AddForce(moveDirection.normalized * 5000f);
        }
    }
}
