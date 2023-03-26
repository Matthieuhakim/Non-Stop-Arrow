using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AmmoType
{
    Single,
    Triple,
    Explosive
}

public class AmmoManager : MonoBehaviour
{

    public AmmoType ammoType = AmmoType.Single;


    public int remainingArrows;
    public int quiverCapacity = 5;


    private bool powerUpIsActive;
    private float timeSincePowerUp;
    private float maxPowerUpTime;

    private AmmoType defaultAmmoType = AmmoType.Single;
    private float defaultReloadSpeed = 0f;

    private AimAnimationController aimAnimationController;
    private GameObject player;

    public static AmmoManager instance;


    private void Awake()
    {
        instance = GetComponent<AmmoManager>();
    }


    void Start()
    {
        RefillQuiver();
        player = GameObject.FindGameObjectWithTag(GameObjectTags.PLAYER);
        aimAnimationController = player.GetComponentInChildren<AimAnimationController>();
        defaultReloadSpeed = player.GetComponent<PlayerStats>().agility;
        aimAnimationController.reloadAnimSpeed = defaultReloadSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLimitPowerUp();
    }


    public void RefillQuiver()
    {
        remainingArrows = quiverCapacity;
    }

    public void AddArrows(int num)
    {
        remainingArrows += num;
        if(remainingArrows > quiverCapacity)
        {
            remainingArrows = quiverCapacity;
        }
    }


    public void StartPowerUp(AmmoType powerUp, float powerUpTimeInSeconds, float reloadSpeed)
    {
        ammoType = powerUp;
        powerUpIsActive = true;

        timeSincePowerUp = 0f;
        maxPowerUpTime = powerUpTimeInSeconds;

        aimAnimationController.reloadAnimSpeed = reloadSpeed * player.GetComponent<PlayerStats>().agility;
    }


    private void TimeLimitPowerUp()
    {
        if (powerUpIsActive)
        {
            timeSincePowerUp += Time.deltaTime;

            if(timeSincePowerUp >= maxPowerUpTime)
            {
                ammoType = defaultAmmoType;
                powerUpIsActive = false;

                timeSincePowerUp = 0f;
                maxPowerUpTime = 0f;
                aimAnimationController.reloadAnimSpeed = defaultReloadSpeed;

            }
        }
    }


}
