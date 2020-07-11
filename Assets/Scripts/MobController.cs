using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStats))]
public class MobController : MonoBehaviour
{
    
    //RaycastHit hit;
    //Ray detectionRay;
    

    private Transform nextWaypoint;
    private int waypointIndex = 0;
    private BuildManager buildManager;
    private MobStats mobStats;
    void Start()
    {
        nextWaypoint = Waypoint.waypointList[waypointIndex];
        buildManager = BuildManager.instance;
        mobStats = GetComponent<MobStats>();
    }

    Vector3[] getEndPoints(Vector3 a,Vector3 scale)
    {
        Vector3[] result = new Vector3[2];
        result[0] = new Vector3(a.x - scale.x / 2,1, a.z - scale.z / 2);
        result[1] = new Vector3(a.x + scale.x / 2,1, a.z + scale.z / 2);
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        

        Vector3 dir = nextWaypoint.position - transform.position;
        transform.Translate(dir.normalized * mobStats.speed * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position,nextWaypoint.position) < 0.5f)
        {
            waypointIndex++;
            if (waypointIndex > Waypoint.waypointList.Length - 1)
            {
                ReachEnd();
                
                return;
            }
            nextWaypoint = Waypoint.waypointList[waypointIndex];

        }
        mobStats.speed = mobStats.startSpeed;
        /*
        detectionRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(detectionRay, out hit, 1f)) {
            Vector3[] endPoints = getEndPoints(hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.localScale);
            Vector3 direction = (endPoints[1] - endPoints[0]).normalized;
            rb.AddForce(direction * speed * Time.deltaTime);
            Debug.Log(direction);
        }
        */
    }

    
    void ReachEnd()
    {
        PlayerStats.instance.TakeDamage(mobStats.MobDoesDamageAmount);
        Destroy(gameObject);
    }
}
