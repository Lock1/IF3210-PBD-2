using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject playerName;
    private string dbName = "URI=file:Scoreboard.db";

    private void Start()
    {
        CreateZenTable();
        CreateWaveTable();
    }

    public void CreateZenTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS zen_scoreboards (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255), time VARCHAR(255));";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void CreateWaveTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS wave_scoreboards (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255), num_wave INT, score INT);";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void PlayZen()
    {
        GetName();
        SceneManager.LoadScene(1);
    }

    public void PlayWave()
    {
        GetName();
        SceneManager.LoadScene(4);
    }

    public void ShowZenScoreBoard ()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Zen Scoreboard");
    }

    public void ShowWaveScoreBoard ()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Wave Scoreboard");
    }

    public void ShowMainMenu ()
    {
        Debug.Log("asdas");
        SceneManager.LoadScene(0);
    }

    public void GetName()
    {
        string pName = playerName.GetComponent<TMP_InputField>().text;
        if (string.IsNullOrEmpty(pName) || string.IsNullOrWhiteSpace(pName))
        {
            pName = "Anonymous User";
        }
        PlayerPrefs.SetString("NICKNAME", pName);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
