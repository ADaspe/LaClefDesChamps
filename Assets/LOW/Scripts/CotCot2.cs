using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotCot2 : MonoBehaviour
{
    private Animator CotA;

    [Header("Cot Parameters")]
    [SerializeField] float Speed;
    [SerializeField] float PicoreTime;
    [SerializeField] float randomPosRadius;
    [SerializeField] float TargetPosTrigger;
    [SerializeField] float obsdetectionRange;
    [SerializeField] float newpointmult;
    [SerializeField] float avoidanceAngle;

    [Header("Obstacles")]
    [SerializeField] Transform rayCastPoint;
    [SerializeField] LayerMask obstacleLayer;

    [Header("Debug")]
    [SerializeField] Transform TP;
    private Vector3 RandomPos;
    private Vector3 TargetPoint;
    private Vector3 lHitInfo;
    private float hitlenght;
    public bool Picoring;
    private bool isDone;
    private int i =0;

    Ray Fray;
    Ray Lray;
    Ray Rray;

    RaycastHit Fhit;
    RaycastHit Rhit;
    RaycastHit Lhit;

    void Start()
    {
        CotA = GetComponentInChildren<Animator>();
        Fray = new Ray(rayCastPoint.position, transform.TransformDirection(new Vector3(-1,0,0)));
        avoidanceAngle = (avoidanceAngle * Mathf.PI) / 180;
        
        Picoring = false;

        //Initialize TP
        TargetPoint = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10);
    }
    private void rayCasts()
    {
        if (
           Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)), obsdetectionRange, obstacleLayer)
        || Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0.5f)), obsdetectionRange, obstacleLayer)
        || Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, -0.5f)), obsdetectionRange, obstacleLayer)
                                                                                                                                            )
        {
            // is going crazy but on the right path
            Debug.Log(" obstacle near");
        }
        // DEBUG
        
        //front ray 
        Physics.Raycast(Fray,out Fhit , obsdetectionRange, obstacleLayer);
        //Physics.Raycast(Lray,out Rhit , obsdetectionRange, obstacleLayer);
    }
    private void ObstacleAvoidance()
    {
        Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)),out Fhit, obsdetectionRange, obstacleLayer); // Front
        Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, .5F)),out Rhit, obsdetectionRange, obstacleLayer); // Right 
        Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, -0.5F)),out Lhit, obsdetectionRange, obstacleLayer); // Left 

        if (Fhit.collider !=null && Fhit.collider != null)
        {
            i++;

            if(i == 1)
            {
                print("turning right");
                hitlenght = Fhit.distance * newpointmult;
                TargetPoint = new Vector3(hitlenght * Mathf.Cos(avoidanceAngle) + Fhit.point.x, transform.position.y , hitlenght * Mathf.Sin(avoidanceAngle) + Fhit.point.z);
            } 
        }
        else if (Fhit.collider != null && Rhit.collider != null)
        {

            print("turning left");
            hitlenght = Fhit.distance * newpointmult;
            TargetPoint = new Vector3(hitlenght * Mathf.Cos(-avoidanceAngle) + Fhit.point.x, transform.position.y, hitlenght * Mathf.Sin(-avoidanceAngle) + Fhit.point.z);
        }
        else
        {
            i = 0;
        }
    }
    private void ObstacleAvoidance2()
    {   //                                                                  Vector3(0,0,1)
        Physics.Raycast(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out Fhit, obsdetectionRange, obstacleLayer);

        if (Fhit.collider != null)
        {
            TargetPoint = new Vector3(5 * Mathf.Cos(avoidanceAngle) + Fhit.point.x, transform.position.y ,( 5 * Mathf.Sin(avoidanceAngle)) + Fhit.point.z);
        }
    }
    private void GenerateRandomPos()
    {
        RandomPos = transform.position + new Vector3((Random.insideUnitCircle.x + 2) * randomPosRadius, .3f, (Random.insideUnitCircle.y + 2) * randomPosRadius); // +position d'un object 
    }
    private void smoothLookat()
    {
        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(TargetPoint);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, 2 * Time.deltaTime);
    }
    private void newtarget()
    {
        GenerateRandomPos();
        TargetPoint = RandomPos;

        print("creating new random target P");
    }
    private void moovement()
    {
            // logic
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            smoothLookat();
            print("mooving");
           
            // Animation
            CotA.SetBool("moving",true);
    }
    void Update()
    {
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(0, 0, 1)) * obsdetectionRange, Color.red);  // Front
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(0.5F,0,1)) * obsdetectionRange, Color.red); // Right 
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(-0.5F,0,1)) * obsdetectionRange, Color.red); // Left 

        if (Vector3.Distance(transform.position, TargetPoint) > TargetPosTrigger)
        {
            moovement();

            ObstacleAvoidance();
        }

        else
        {
            StartCoroutine(picorTime());

            //Animation
            CotA.SetBool("moving", false);
        }

        // call new target only once
        if(Picoring)
        {
            StopAllCoroutines();   // super mega importante or coroutine goes wild
            Picoring = false;
            newtarget();
        }

        // Debug pos
        TP.position = TargetPoint;

        #region Coroutines

        IEnumerator picorTime()
        {
            yield return new WaitForSeconds(PicoreTime);
            Picoring = true;
        }
        #endregion
    }
}