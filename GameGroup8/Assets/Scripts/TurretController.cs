using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    private GameObject enemy;
    private Vector3 enemyPosition;
    
    void Update()
    {
        if (enemy)
        {
            enemyPosition = enemy.transform.position;
            enemyPosition.y = 0;
            transform.LookAt(enemyPosition);
            transform.Rotate(new Vector3 (0, 1, 0), 90);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vector3 LastPostition = other.transform.position;
            enemyPosition = LastPostition;
            enemy = null;
        }
         
    }
}
