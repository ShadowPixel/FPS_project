using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 15f;
    [SerializeField] GameObject visualFeedbackObject;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] float weaponKnockback = 5000f;

    RaycastHit objectHit;  //stores info about what object is hit

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    //fire the weapon using a raycast
    void Shoot()
    {
        //calculate direction to shoot
        Vector3 rayDirection = cameraController.transform.forward;
        //cast a debug ray
        UnityEngine.Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        //do the raycast
        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            //do stuff when hitting an object
            UnityEngine.Debug.Log("You hit the " + objectHit.transform.name + "!");
            //visual feedback
            visualFeedbackObject.transform.position = objectHit.point;
            //apply damage to enemy
            EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
            if(enemyShooter != null)
            {
                enemyShooter.TakeDamage(weaponDamage);
                enemyShooter.TakeKnockback(weaponKnockback);
            }
        }
        else
        {
            //do stuff when missing objects
            UnityEngine.Debug.Log("Miss");
        }
    }
}
