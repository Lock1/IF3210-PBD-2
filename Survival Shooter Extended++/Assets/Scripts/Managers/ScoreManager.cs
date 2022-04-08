using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public string mode;
    public static float survivalDuration;
    public static int wave;
    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
        wave = 1;
    }


    void Update ()
    {
        if (mode == "Zen") {
            text.text = string.Format("{0}:{1}", (int) survivalDuration / 60, (int) survivalDuration % 60f);
        }
        else {

            text.text = "Wave " + wave + " / Score " + score;
        }
    }
}
