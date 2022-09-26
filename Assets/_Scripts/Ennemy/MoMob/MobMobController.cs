using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MobMobController : MonoBehaviour
{
    private MobMobState mobMobState;
    private MobMobAnimationManager mobMobAnimationManager;

    public NavMeshAgent navMeshAgent;

    // private Vector3 _targetPoint;
    public  Vector3 destinationpoint = Vector3.zero;

    public Vector3 characterDestination;
    private Vector3 _initialPosition;

    public float distanceToWanderPoint; // DEBUG 
    public float distanceToTarget;
    //private RaycastHit[] hitPlayer = new RaycastHit[].Length = 10;
    // debug keep an eye on dist betwenn mod and Player
    private float _initialSpeed;
    
    public Transform targetPosition;

    public Transform RaycastPoint;

    void Start()
    {
        _initialPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        mobMobState = GetComponent<MobMobState>();
        mobMobAnimationManager = GetComponentInChildren<MobMobAnimationManager>();
        _initialSpeed = navMeshAgent.speed;
        destinationpoint = new Vector3(transform.position.x + 4, 0, transform.position.z + 4);
    }
    
    public bool IsMoving() => navMeshAgent.velocity.magnitude != 0; 

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)   // is this like not mine :'((
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }   

    public void WanderAround()
    {
        if  (distanceToWanderPoint > 1) 
        {
            StopAllCoroutines();
            MoveToDestination(destinationpoint);
            //print("moving TO wander point");    
        }
 
         if (distanceToWanderPoint < 1)
        {
            StartCoroutine(waitForNExtPoint());
            //print("destination point : " + destinationpoint);
            //print(" making NEW wander point");
        }
    }
    public void CloseToPLayer()  { if (mobMobState.closeToPlayer()) { navMeshAgent.speed = 0; } }

    public void ChaseSpeeds()
    {
       if(distanceToTarget < mobMobState.suspectRange && distanceToTarget > mobMobState.detectionRange)
        {
            navMeshAgent.speed = _initialSpeed /2;
            MoveToDestination(targetPosition.position);
        }
      
        else if ( Vector3.Distance(transform.position, targetPosition.position) < mobMobState.detectionRange)
        {
            navMeshAgent.speed = _initialSpeed;
            MoveToDestination(targetPosition.position);
        }
    }

    public Vector3 MoveToDestination( Vector3 _targetPoint) => navMeshAgent.destination = _targetPoint;
    
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPosition.position);
        distanceToWanderPoint = Vector3.Distance(transform.position, destinationpoint);

        mobMobAnimationManager.mobAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
    
    IEnumerator waitForNExtPoint()
    {
        yield return new WaitForSeconds(mobMobState.wanderTime);
        destinationpoint = RandomNavSphere(_initialPosition, 10, 15);
    }
}