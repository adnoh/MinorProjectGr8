using UnityEngine;
using System.Collections;

/* Class that requests a path and follows that path 
Has to be attached to the seeker-object (eg enemies)
*/

public class Seeker : MonoBehaviour
{

    // public / dynamic properties
    public Transform target;
    public Transform currentPos;
    public float speed;
	public bool destroyed = false;

    public bool toBase = true;
    public bool withinBaseRange = false;


    Vector3[] path;
    int targetIndex; // current index in the path array 
    bool pathIsFound = false;

    // get target
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Wall").GetComponent<Transform>();
    }

    // Request path

    void Start()
    {
	  	PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		speed = this.gameObject.GetComponent<EnemyController> ().walkingSpeed;
        if (this.gameObject.name.Equals("PolarBearPrefab(Clone)")){
            toBase = false;
        }
    }

	void Update(){
		speed = this.gameObject.GetComponent<EnemyController> ().updatedSpeed;
        currentPos = this.gameObject.transform;
        if (!this.gameObject.GetComponent<EnemyController>().wandering && !pathIsFound)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            pathIsFound = true;
        }
	}

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && !destroyed && toBase && !withinBaseRange && !this.gameObject.GetComponent<EnemyController>().wandering)
        {
            Debug.Log(0);
            path = newPath;
            // Stop the Coroutine before starting.
            StopCoroutine("FollowPath");
            Debug.Log(path.Length > 0);
            if (path.Length > 0){
                StartCoroutine("FollowPath");
            }
        }
    }

    IEnumerator FollowPath(){
              
            Vector3 currentWaypoint = path[0];

		while (!destroyed && toBase && !withinBaseRange && !this.gameObject.GetComponent<EnemyController>().wandering)
        {
            if (transform.position == currentWaypoint && !destroyed && toBase && !withinBaseRange && !this.gameObject.GetComponent<EnemyController>().wandering)
            {
                    targetIndex++;
                    if (targetIndex >= path.Length){
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

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("BASE"))
        {
            withinBaseRange = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("BASE"))
        {
            withinBaseRange = false;
        }
    }
}