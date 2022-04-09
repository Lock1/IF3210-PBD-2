using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public string type;
    public Button upgradeButton;
    public static void upgradeAttackSpeed() {
        PlayerShooting.timeBetweenBullets *= 0.8f;
    }

    public static void upgradeShootCount() {
        PlayerShooting.shootCount += 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton = GetComponent<Button>();
        if (type == "AS")
            upgradeButton.onClick.AddListener(upgradeAttackSpeed);
        else
            upgradeButton.onClick.AddListener(upgradeShootCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
