using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [Header("General Settings:")]
    public PlayerBook book; //Maybe I don't need this
    public Transform origin;    //Maybe this.transform.position
    public Transform revertPoint;
    //public Transform grapplePoint;   //Passed In with a public fonction
    public LineRenderer lineRenderer;
    [SerializeField] private int precision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.1f, 20f)] [SerializeField] private float StartWaveSize = 2;
    
    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1f, 5f)] private float ropeProgressionSpeed = 1;
    public AnimationCurve ropeBackingCurve;
    [SerializeField] [Range(1f, 5f)] private float ropeBackingSpeed = 1;
    public float ropeBackingTime = 1f;

    private float moveTime = 0;
    float waveSize = 0;
    private bool straightLine = true;
    private Vector3 grapplePoint;
    [HideInInspector] public bool isGrappling = false;
    private bool ropeBack = false;
    private float secondTimer;
    private bool activateSecondTimer = false;


    private void OnEnable()
    {
        moveTime = 0;
        secondTimer = 0;
        lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        straightLine = false;
        isGrappling = false;
        activateSecondTimer = false;

        LinePointsToFirePoint();

        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        //Reset();
    }

    public void SetGrapplePoint(Vector3 grapplePoint, bool defaulting)
    {
        this.grapplePoint = grapplePoint;
        ropeBack = defaulting;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            lineRenderer.SetPosition(i, origin.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;

        if (activateSecondTimer) secondTimer += Time.deltaTime;
        DrawRope();
    }

    private void DrawRope()
    {
        if (!straightLine)
        {
            if (lineRenderer.GetPosition(precision - 1).z == grapplePoint.z)
            {
                straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            //Permet à la logique du grappin de savoir quand la corde est "tendue"
            if (!isGrappling)
            {
                //grapplingGun.Grapple();
                isGrappling = true;
            }

            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (lineRenderer.positionCount != 2) { lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    private void DrawRopeNoWaves()
    {
        lineRenderer.SetPosition(0, origin.position);

        //Si la corde est tendue et qu'il y a un retour, Lerp la position du point 1 jusqu'a la position d'origine
        if (ropeBack)
        {
            activateSecondTimer = true;
            Vector3 currentPosition = Vector3.Lerp(grapplePoint, origin.position, ropeBackingCurve.Evaluate(secondTimer) * ropeBackingSpeed);
            lineRenderer.SetPosition(1, currentPosition);

            if (lineRenderer.GetPosition(1) == lineRenderer.GetPosition(0)) lineRenderer.enabled = false;
        }
        else lineRenderer.SetPosition(1, grapplePoint);
    }

    private void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);

            Vector3 baseOffset = Vector3.Cross(grapplePoint, origin.position).normalized;
            Vector3 offsetReverse = new Vector3(baseOffset.x, baseOffset.z, baseOffset.y);
            Vector3 offset = offsetReverse * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector3 targetPos = Vector3.Lerp(origin.position, grapplePoint, delta) + offset;
            Vector3 currentPosition = Vector3.Lerp(origin.position, targetPos, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }
}
