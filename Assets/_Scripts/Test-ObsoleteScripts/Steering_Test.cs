using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering_Test : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private float maxForce = 15;
    [SerializeField] private float maxSpeed = 30;
    [SerializeField] private float maxVelocity = 200;
    [SerializeField] private float mass = 15;

    Vector3 velocity;

    void Start()
    {
        velocity = Vector3.zero;
    }

    void Update()
    {
        transform.LookAt(target.transform);

        var desiredVelocity = target.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxVelocity * Time.deltaTime;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);
        steering = steering / mass;
        steering.y = 0;

        velocity = Vector3.ClampMagnitude(velocity + steering, maxSpeed);
        transform.position += velocity * Time.deltaTime;

    }
}
