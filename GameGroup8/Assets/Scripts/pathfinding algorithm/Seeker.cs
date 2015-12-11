using UnityEngine;
using System.Collections;

/* Class that requests a path and follows that path 
Has to be attached to the seeker-object (eg enemies)
*/

public class Seeker : MonoBehaviour
{

    // public / dynamic properties
    private Transform target;
    private Transform currentPos;
    private float speed;


    Vector3[] path;
    int targetIndex; // current index in the path array 

    // get target
    void Awake()
    {
        target = GameObject.Find("player").GetComponent<Transform>();
		currentPos = this.gameObject.GetComponent<Transform> ();
    }

    // Request path

    void Start()
    {
		if (this.gameObject != null) {
			StartCoroutine (UpdatePath ());
		}
		speed = this.gameObject.GetComponent<EnemyController> ().walkingSpeed;
    }

	void Update(){
		speed = this.gameObject.GetComponent<EnemyController> ().walkingSpeed;
	}

    IEnumerator UpdatePath()
    {
        // wait for x seconds before 
        float refreshRate = 0.25f;

        while (target != null && this.gameObject != null)
        {
            if (target != currentPos)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            }
            // request new path and follow this path till new path found.
            yield return new WaitForSeconds(refreshRate);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && this.gameObject != null)
        {
            path = newPath;
            // Stop the Coroutine before starting.
            StopCoroutine("FollowPath");
            if (path.Length > 0)
            {
                StartCoroutine("FollowPath");
            }
            
        }
    }

    IEnumerator FollowPath()
    {
        
        
            Vector3 currentWaypoint = path[0];

            while (true)
            {
                if (transform.position == currentWaypoint && this.gameObject != null)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        // reset targetindex counter + path
                        targetIndex = 0;
                        path = new Vector3[0];

                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
        
    }



    // Visual aid, draws the path in cubes

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}