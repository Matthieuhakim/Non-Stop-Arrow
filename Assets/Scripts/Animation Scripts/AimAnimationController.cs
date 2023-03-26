using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimAnimationController : MonoBehaviour
{

    private PlayerAimAndShoot playerAimAndShoot;
    private Animation aimAnimation;


    public float returnToDefaultSpeed = 2f;
    public float pullCordSpeed = 2f;

    private Animation reloadAnim;
    private bool playedReloadAnim;
    public float reloadAnimSpeed;

    [Range(0, 0.99f)]
    public float defaultPosition = 0.17f;


    public Transform rightArm;
    public Transform leftArm;


    private Vector3 defaultRightArm = new Vector3(-1.7f, -0.8f, 0);
    private Vector3 defaultArrowHolder = new Vector3(3.2f, -0.6f, 0);


    public float maxHandPull = 1;
    public float maxArrowPull = 1;

    public Transform arrowHolder;

    // Start is called before the first frame update
    void Awake()
    {
        playerAimAndShoot = GetComponentInParent<PlayerAimAndShoot>();
        aimAnimation = GetComponent<Animation>();
        reloadAnim = rightArm.GetComponent<Animation>();
        
    }


    private void Start()
    {
        aimAnimation[AnimationTags.AIM_ANIMATION_NAME].speed = 0;
        GoToDefaultWithoutTime();
    }


    void Update()
    {
        ShowAnimationState(playerAimAndShoot.currentDirection);
    }



    private void ShowAnimationState(Vector2 input)
    {
        float animationFrame = aimAnimation[AnimationTags.AIM_ANIMATION_NAME].normalizedTime;

        if (playerAimAndShoot.isAiming)
        {
            animationFrame = ConvertVectorToFrame(input);
            animationFrame = Mathf.Clamp(animationFrame, 0, 0.99f);
            aimAnimation[AnimationTags.AIM_ANIMATION_NAME].normalizedTime = animationFrame;

            PullRightArm();

            playedReloadAnim = false;
        }
        else if (!playerAimAndShoot.isAiming && arrowHolder.gameObject.GetComponent<ArrowHolder>().FindAmmo() != null)
        {
            ReturnToDefault();
        }
        else
        {
            Reload();
        }
    }


    private void Reload()
    {
        if (!playedReloadAnim)
        {
            reloadAnim[AnimationTags.RELOAD_ANIMATION_NAME].speed = reloadAnimSpeed;
            reloadAnim.Play();

            playedReloadAnim = true;
        }

        ReturnToDefault();

        if (!reloadAnim.isPlaying)
        {
            arrowHolder.GetComponent<ArrowHolder>().SpawnArrow();
        }
    }


    private void PullRightArm()
    {
        rightArm.localPosition = Vector3.Lerp(rightArm.localPosition, defaultRightArm + maxHandPull * Vector3.left, Time.deltaTime * pullCordSpeed);
        arrowHolder.localPosition = Vector3.Lerp(arrowHolder.localPosition, defaultArrowHolder + maxArrowPull * Vector3.left, Time.deltaTime * pullCordSpeed);
    }

    private void ReturnToDefault()
    {
        playedReloadAnim = true;

        float animationFrame = aimAnimation[AnimationTags.AIM_ANIMATION_NAME].normalizedTime;

        animationFrame = Mathf.Lerp(animationFrame, defaultPosition, returnToDefaultSpeed * Time.deltaTime);

        aimAnimation[AnimationTags.AIM_ANIMATION_NAME].normalizedTime = animationFrame;

        rightArm.localPosition = Vector3.Lerp(rightArm.localPosition, defaultRightArm, Time.deltaTime * returnToDefaultSpeed);
        arrowHolder.localPosition = Vector3.Lerp(arrowHolder.localPosition, defaultArrowHolder, Time.deltaTime * returnToDefaultSpeed);
    }

    private void GoToDefaultWithoutTime()
    {
        playedReloadAnim = true;
        aimAnimation[AnimationTags.AIM_ANIMATION_NAME].normalizedTime = defaultPosition;

        rightArm.localPosition = defaultRightArm;
        arrowHolder.localPosition = defaultArrowHolder;
    }


    private float ConvertVectorToFrame(Vector2 input)
    {
        float inputAngle = Vector2.Angle(Vector2.down, input);

        inputAngle = Mathf.Clamp(inputAngle, 55f, 165f);
        inputAngle -= 55f;
        inputAngle /= 85;

        return inputAngle;
    }



   


}
