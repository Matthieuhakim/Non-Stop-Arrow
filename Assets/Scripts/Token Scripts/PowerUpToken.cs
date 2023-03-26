using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpToken : Token
{

    public AmmoType powerUp;
    public float powerUpTimeInSeconds = 5f;
    public float reloadSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void ExecuteSpecialty()
    {
        base.ExecuteSpecialty();
        AmmoManager.instance.StartPowerUp(powerUp, powerUpTimeInSeconds, reloadSpeed);
    }




}
