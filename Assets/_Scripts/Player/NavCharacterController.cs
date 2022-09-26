using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCharacterController : MonoBehaviour
{
    private NavMeshAgent NavmeshAgent;

    [SerializeField] Camera MainCamera;

    public LayerMask Sol;

    public Vector3 characterDestination;
    Vector3 currentPosition;
    Vector3 nextFramePosition;
    public Vector3 playerTargetDestination;

    [Header("Charecater parameters")]
    public float Speed = 10f;

    RaycastHit hit = new RaycastHit();
    void Start()
    {
        NavmeshAgent = GetComponent<NavMeshAgent>();
    }
    
    public bool IsMoving() => NavmeshAgent.velocity.magnitude != 0; 
    
    private Vector3 Setdestination()
    {
        Vector3 toReturn = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity, Sol);
            toReturn = hit.point;

            //print("cliked" + hit.point);
        }

        return toReturn;
    }
    private void MoveToDestination() => NavmeshAgent.destination = hit.point;
    void Update()
    {
        IsMoving();
        Setdestination();
        MoveToDestination();
    }
}
