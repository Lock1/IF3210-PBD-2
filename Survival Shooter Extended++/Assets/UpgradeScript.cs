using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    GameObject upUI;
    public string type;
    public Button upgradeButton;
    public void upgradeAttackSpeed() {
        PlayerShooting.timeBetweenBullets *= 0.8f;
        PlayerHealth.upgradeAvail--;
    }

    public void upgradeShootCount() {
        PlayerShooting.shootCount += 2;
        PlayerHealth.upgradeAvail--;
    }

    public void upgradeExplode() {
        PlayerShooting.explosive = true;
        PlayerShooting.explosionRadius += 1f;
        PlayerHealth.upgradeAvail--;
    }

    // Start is called before the first frame update
    void Start()
    {
        upUI = GameObject.FindGameObjectWithTag("UpgradeUI");
        upgradeButton = GetComponent<Button>();
        if (type == "AS")
            upgradeButton.onClick.AddListener(upgradeAttackSpeed);
        else if (type == "Multi")
            upgradeButton.onClick.AddListener(upgradeShootCount);
        else
            upgradeButton.onClick.AddListener(upgradeExplode);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.upgradeAvail > 0)
            upUI.SetActive(true);
        else
            upUI.SetActive(false);
    }
}
