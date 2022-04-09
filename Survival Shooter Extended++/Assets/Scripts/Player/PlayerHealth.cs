using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private string dbName = "URI=file:Scoreboard.db";
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float survivalDuration;
    bool updating = true;

    int lastMinute = 0;

    public static int upgradeAvail = 0;

    GameObject upUI;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;                                                
    bool damaged;                                               

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        survivalDuration = 0f;
        currentHealth = startingHealth;
        upUI = GameObject.FindGameObjectWithTag("UpgradeUI");
    }

    public void InsertZenScore(string name, string time)
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO zen_scoreboards (name, time) VALUES ('" + name + "', '" + time + "');";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    void Update()
    {
        if (updating) {
            survivalDuration += Time.deltaTime;
            ScoreManager.survivalDuration = survivalDuration;
        }
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
        HPManager.hp = currentHealth;
        HPManager.maxhp = startingHealth;

        if ((int) survivalDuration / 60 > lastMinute) {
            lastMinute++;
            upgradeAvail++;
            upUI.SetActive(true);
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        
        

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        updating = false;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

        InsertZenScore(PlayerPrefs.GetString("NICKNAME"), ((int) survivalDuration / 60) + ":" + ((int) survivalDuration % 60));
    }
}
