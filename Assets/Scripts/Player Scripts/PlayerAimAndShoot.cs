using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{

    public Transform shootPoint;

    public float min_X = 0.2f;
    public float min_Y = 0.3f;

    private Vector2 mouseStartPosition, mouseEndPosition;
    private Vector2 targetMouseDrag;
    private Vector2 oldMouseTarget;

    private Vector2 newTarget;
    public Vector2 currentDirection;


    private Camera myCamera;


    public LineRenderer trajectoryRenderer;
    public int lineSmoothness = 10;
    public int lineLength = 2;

    public LayerMask canHit;


    public bool isAiming;
    private Animator animator;

    public float slowMotionMultiplier = 0.5f;

    public bool willShoot = false;
    private bool pressedDown = false;

    private PlayerStats playerStats;
    private ArrowHolder arrowHolder;


    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        arrowHolder = GetComponentInChildren<ArrowHolder>();
    }


    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;

        trajectoryRenderer.positionCount = lineLength * lineSmoothness;
        trajectoryRenderer.gameObject.SetActive(false);
    }


    private void GetMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;

            if(arrowHolder.FindAmmo() != null)
            {
                Time.timeScale = slowMotionMultiplier;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                pressedDown = true;
                isAiming = true;
            }

        }
        if (Input.GetMouseButton(0) && arrowHolder.FindAmmo() != null)
        {
            if (!pressedDown)
            {
                Time.timeScale = slowMotionMultiplier;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                pressedDown = true;
                isAiming = true;
            }


            mouseEndPosition = Input.mousePosition;

            ConvertInputToDirection();
        }
        if (Input.GetMouseButtonUp(0) && arrowHolder.FindAmmo() != null)
        {
            pressedDown = false;
            isAiming = false;
            trajectoryRenderer.gameObject.SetActive(false);


            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            if (willShoot)
            {
                Shoot();
            }
            willShoot = false;

            oldMouseTarget = new Vector2(0, 0);
            currentDirection = oldMouseTarget;
        }
    }



    private void ConvertInputToDirection()
    {
        targetMouseDrag = mouseStartPosition - mouseEndPosition;

        if (targetMouseDrag.x <= min_X * playerStats.strength && targetMouseDrag.y <= min_Y * playerStats.strength && targetMouseDrag.y >= -min_Y * playerStats.strength)
        {
            trajectoryRenderer.gameObject.SetActive(false);

            isAiming = false;
            willShoot = false;

        }
        else if (!trajectoryRenderer.gameObject.activeInHierarchy)
        {
            trajectoryRenderer.gameObject.SetActive(true);

            isAiming = true;
            willShoot = true;
        }

        newTarget = DelayResponsiveness(oldMouseTarget, targetMouseDrag);
        currentDirection = ConstraintDirectionAndMagnitude(newTarget);

        oldMouseTarget = newTarget;

        VisualizeTrajectory(currentDirection);

    }



    private Vector2 ConstraintDirectionAndMagnitude(Vector2 vector2)
    {

        Vector2 direction = Vector2.ClampMagnitude(vector2, playerStats.strength);


        direction.y = Mathf.Clamp(direction.y, -min_Y * playerStats.strength, min_Y * playerStats.strength);


        if(direction.x < min_X * playerStats.strength)
        {
            direction.x = min_X * playerStats.strength;
        }

        return direction;
    }
    private Vector2 DelayResponsiveness(Vector2 oldVector, Vector2 rawVector)
    {

        Vector2 direction = Vector2.Lerp(oldVector, rawVector, playerStats.agility * Time.deltaTime);
        return direction;
    }


    private Vector3 CalculatePointInTrajectory(Vector3 shotVelocity, float time)
    {
        Vector3 result = new Vector3();

        result.x = shootPoint.position.x + shotVelocity.x * time;
        result.y = (-0.5f * Mathf.Abs(Physics2D.gravity.y)) * (time * time) + (shotVelocity.y * time) + shootPoint.position.y;

        return result;
    }
    private void VisualizeTrajectory(Vector3 shotVelocity)
    {
        bool hasFoundCollision = false;
        float collisionTime = 0;

        for(int i = 0; i < lineLength * lineSmoothness; i++)
        {

            var hit = Physics2D.Linecast(CalculatePointInTrajectory(shotVelocity, i / (float)lineSmoothness), CalculatePointInTrajectory(shotVelocity, (i+1) / (float)lineSmoothness), canHit);

            Vector3 pos = CalculatePointInTrajectory(shotVelocity, i / (float)lineSmoothness);

            if (hasFoundCollision)
            {
                pos = CalculatePointInTrajectory(shotVelocity, collisionTime);
            }

            if (hit && !hasFoundCollision)
            {
                pos = CalculatePointInTrajectory(shotVelocity, (i + 1) / (float)lineSmoothness);
                hasFoundCollision = true;
                collisionTime = (i + 1) / (float)lineSmoothness;
            }


            trajectoryRenderer.SetPosition(i, pos);
        }
    }



    private void Shoot()
    {
        Arrow arrow = arrowHolder.FindAmmo();
        if(arrow != null)
        {
            arrow.Shoot(currentDirection);

            AmmoManager.instance.remainingArrows -= arrow.GetComponent<ArrowStats>().cost;
            if (AmmoManager.instance.remainingArrows < 0)
            {
                AmmoManager.instance.remainingArrows = 0;
            }
        }
    }




    // Update is called once per frame
    void Update()
    { 
        GetMouseDrag();
    }






}