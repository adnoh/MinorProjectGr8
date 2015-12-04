using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* Class that represents a PathRequestManager.
    Using a Queue and process one path at a time

*/

public class PathRequestManager : MonoBehaviour {
    /* Mini-class that represents a PathRequest
    */

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> succesful;

        public PathRequest(Vector3 startPos, Vector3 targetPos, Action<Vector3[], bool> callback)
        {
            pathStart = startPos;
            pathEnd = targetPos;
            succesful = callback;
        }
    }

    // using Queue makes it simple
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;  // for storing the current path request

    static PathRequestManager instance;

    Pathfinding pathfinding; // reference to pathfinding class
    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }


    // Action -> stores the method
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> succesful)
    {
        // if ie al op positie is, ga niet nog een x daarheen. 
        if (pathStart != pathEnd)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, succesful);
            instance.pathRequestQueue.Enqueue(newRequest);
        }
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        // If not processing a path and que is not empty
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue(); // takes first item out of the queue 
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        } 
    }

    // 
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.succesful(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }    
  }
