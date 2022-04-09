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
