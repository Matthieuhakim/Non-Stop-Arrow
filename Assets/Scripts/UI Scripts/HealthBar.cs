using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    Image playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.fillAmount = PlayerStats.instance.health / 100f;
    }
}
