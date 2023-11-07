using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] const float waypointGizmoRadius = 0.3f;
    [Header("Path Settings")]
    [SerializeField] private bool canLoop = false;

        void OnDrawGizmos()
        {            
            for (int i = 0; i < transform.childCount ; ++i)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }

        }

        public int GetNextIndex(int i)
        {
            if (canLoop && i + 1 == transform.childCount)
            {
                return 0;
            }

            if (i + 1 == transform.childCount)
            {
                return i;
                
            }


            return (i + 1);
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
}
