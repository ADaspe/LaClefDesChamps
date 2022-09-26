using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotCot : MonoBehaviour
{
    private Animator CotA;

    [Header("Cot Parameters")]
    [SerializeField] float Speed;
    [SerializeField] float PicoreTime;
    [SerializeField] float randomPosRadius;
    [SerializeField] float TargetPosTrigger;
    [SerializeField] float obsdetectionRange;

    [Header("Obstacles")]
    [SerializeField] Transform rayCastPoint;
    [SerializeField] LayerMask obstacleLayer;

    [Header("Debug")]
    [SerializeField] Transform TP;
    private Vector3 RandomPos;
    private Vector3 TargetPoint;

    public bool Picoring;

    void Start()
    {
        CotA = GetComponentInChildren<Animator>();
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
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0)) * 5, Color.red);
        //right ray 
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, 0.5f)) * 5, Color.red);
        //Left ray 
        Debug.DrawRay(rayCastPoint.position, transform.TransformDirection(new Vector3(-1, 0, -0.5f)) * 5, Color.red);
    }
    private void GenerateRandomPos()
    {
        RandomPos = transform.position + new Vector3((Random.insideUnitCircle.x + .5f) * randomPosRadius, .3f, (Random.insideUnitCircle.y + .5f) * randomPosRadius); // +position d'un object 
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
        CotA.SetBool("moving", true);

    }
    void Update()
    {
        if (Vector3.Distance(transform.position, TargetPoint) > TargetPosTrigger)
        {
            moovement();
        }
        else
        {
            StartCoroutine(picorTime());

            //Animation
            CotA.SetBool("moving", false);
        }

        // call new target only once
        if (Picoring)
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
