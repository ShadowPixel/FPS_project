using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public int damagePerTick = 1;
    public float damageTickTime = 1f;

    private List<PlayerController> playersInVolume = new List<PlayerController>();
    private float damageTickCooldown;

    void OnTriggerEnter(Collider other)
    {
        //check if player componenet, if so damage them
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            playersInVolume.Add(player);
        }
        //immediately start damging player
        damageTickCooldown = 0f;
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null)
        {
            playersInVolume.Remove(player);
        }
    }

    void Update()
    {
        //only proceed if player is in volume
        if (playersInVolume.Count > 0)
        {
            if (damageTickCooldown <= 0f)
            {
                foreach (PlayerController player in playersInVolume)
                {
                    player.AlterHealth(-1 * damagePerTick);
                }

                //start cooldown
                damageTickCooldown = damageTickTime;
            }
            else
            {
                damageTickCooldown -= Time.deltaTime;
            }
        }
    }
}
