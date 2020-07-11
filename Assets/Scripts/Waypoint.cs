using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public static Transform[] waypointList;
    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;
        //Debug.Log("Child count is " + childCount);
        waypointList = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            waypointList[i] = transform.GetChild(i);
            //Debug.Log(waypointList[i].position);
        }

    }
    
}
