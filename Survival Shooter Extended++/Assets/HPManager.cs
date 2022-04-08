using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPManager : MonoBehaviour
{
    public static int hp;
    public static int maxhp;
    Text text;


    void Awake ()
    {
        text = GetComponent <Text>();
    }


    void Update ()
    {
        text.text = "HP " + hp + " / " + maxhp;
    }

}
