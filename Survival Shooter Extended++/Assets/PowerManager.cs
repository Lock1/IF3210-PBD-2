using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerManager : MonoBehaviour
{
    public static int power;
    Text text;


    void Awake ()
    {
        text = GetComponent <Text>();
        power = 0;
    }


    void Update ()
    {
        text.text = "Power " + power;
    }

}
