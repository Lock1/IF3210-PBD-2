using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedManager : MonoBehaviour
{
    public static int speed;
    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        speed = 0;
    }


    void Update ()
    {
        text.text = "Speed " + speed;
    }
}
