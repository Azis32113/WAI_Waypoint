using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotateSpeed = 6f;

    // the rotation target for the current frame
    Quaternion rotationGoal;
    // the direction to the next position / waypoint
    Vector3 directionToWaypoint;


    Vector3 nextPosition;

    Transform currentWaypoint;
    int currentWaypointIndex = 0;
    [SerializeField] float waypointTolerance = 1f;

    void Start() {
        nextPosition = transform.position;
    }

    void Update()
    {
        PatrolBehaviour();
        
        MoveTowardsWaypoint();

        RotateTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
    }

    private void PatrolBehaviour()
    {
        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
            }

            nextPosition = GetCurrentWaypoint();
        }
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    // will slowly rotate this agent towards the currents waypoint it is moving towards
    private void RotateTowardsWaypoint()
    {
        nextPosition = (GetCurrentWaypoint() - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(nextPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotateSpeed * Time.deltaTime);
    }
}
