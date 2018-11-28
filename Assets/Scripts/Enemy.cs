using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    WaveConfig waveConfig;
    float moveVelocity = 10f;

    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;
    Vector2 targetPosition;

	// Use this for initialization
	void Start () {
        foreach (Transform waypoint in waveConfig.GetPathPrefab().transform)
        {
            waypoints.Add(waypoint);
        }
        transform.position = waypoints[waypointIndex].position;

        targetPosition = waypoints[waypointIndex+1].position;
        moveVelocity = waveConfig.GetEnemySpeed();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
	
	// Update is called once per frame
	void Update () {
        Move();	
	}

    private void Move()
    {
        if(waypointIndex < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveVelocity * Time.deltaTime);
            
            if((Vector2)transform.position == targetPosition)
            {
                waypointIndex++;
                if (waypointIndex < waypoints.Count)
                {
                    targetPosition = waypoints[waypointIndex].position;
                }                                
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
