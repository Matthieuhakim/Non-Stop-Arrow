using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordLine : MonoBehaviour
{

    private LineRenderer cordRenderer;
    public int cordSmoothness = 3;
    private Vector3 middlePosition;

    public Transform upperCorner, lowerCorner;
    public Transform rightArm;

    public Vector3 middlePos;

    public ArrowHolder arrowHolder;



    // Start is called before the first frame update
    void Awake()
    {

        cordRenderer = GetComponent<LineRenderer>();
        cordRenderer.positionCount = cordSmoothness;
    }


    private void PlacePoints()
    {
        cordRenderer.SetPosition(0, upperCorner.position);
        cordRenderer.SetPosition(1, middlePos);
        cordRenderer.SetPosition(2, lowerCorner.position);
    }


    private void AdjustCordMiddlePosition()
    {
        if (arrowHolder.FindAmmo() != null)
        {
            middlePos = rightArm.position;
        }
        else
        {
            middlePos = (upperCorner.position + lowerCorner.position) / 2;
        }
    }


    private void Start()
    {
        middlePos = rightArm.position;
    }



    // Update is called once per frame
    void Update()
    {
        AdjustCordMiddlePosition();
        PlacePoints();
    }



}
