using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public string type;

    GameObject player;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag ("Player");
        // TODO : Decay
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            if (type == "Power")
                powerUp();
            else if (type == "Speed")
                speedUp();
            else if (type == "Health")
                healthUp();
            Destroy(gameObject, 0f);
        }
    }

    private void powerUp() {
        // TODO : Limiter
        PlayerShooting gunStat = player.GetComponentInChildren<PlayerShooting>();
        gunStat.damagePerShot += 10;
    }

    private void speedUp() {
        PlayerMovement moveStat = player.GetComponentInChildren<PlayerMovement>();
        moveStat.speed += 1;
    }
    
    private void healthUp() {
        PlayerHealth healthStat = player.GetComponentInChildren<PlayerHealth>();
        healthStat.currentHealth += 20;
    }
}
