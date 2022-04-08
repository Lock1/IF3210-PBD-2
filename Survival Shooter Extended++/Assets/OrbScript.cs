using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public string type;
    public float orbAliveDuration;

    Light light;
    GameObject player;
    float timer;
    int lastSecond = 0;

    // Start is called before the first frame update
    void Start() {
        timer = 0f;
        player = GameObject.FindGameObjectWithTag ("Player");
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        
        if ((int) timer > lastSecond) {
            light.intensity -= 0.5f;
            lastSecond++;
        }

        if (timer > orbAliveDuration)
            Destroy(gameObject, 0f);
    }

    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            bool success = false;
            if (type == "Power")
                success = powerUp();
            else if (type == "Speed")
                success = speedUp();
            else if (type == "Health")
                success = healthUp();

            if (success)
                Destroy(gameObject, 0f);
        }
    }

    private bool powerUp() {
        PlayerShooting gunStat = player.GetComponentInChildren<PlayerShooting>();
        if (gunStat.damagePerShot + 10 <= 100) {
            gunStat.damagePerShot += 10;
            return true;
        }
        else
            return false;
    }

    private bool speedUp() {
        PlayerMovement moveStat = player.GetComponentInChildren<PlayerMovement>();
        if (moveStat.speed + 1 <= 12) {
            moveStat.speed += 1;
            return true;
        }
        else
            return false;
    }
    
    private bool healthUp() {
        PlayerHealth healthStat = player.GetComponentInChildren<PlayerHealth>();
        if (healthStat.currentHealth + 20 <= 100) {
            healthStat.currentHealth += 20;
            return true;
        }
        else
            return false;
    }
}
