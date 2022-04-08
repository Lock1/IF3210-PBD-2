using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject playerName;

    public void PlayZen()
    {
        GetName();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayWave()
    {
        GetName();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ShowZenScoreBoard ()
    {
        Debug.Log("Zen Scoreboard");
    }

    public void ShowWaveScoreBoard ()
    {
        Debug.Log("Wave Scoreboard");
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
