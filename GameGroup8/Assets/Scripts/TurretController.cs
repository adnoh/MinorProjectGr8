using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    private GameObject Vijand;
    private Vector3 VijandPositie;

    void Start()
    {

    }
    
    void Update()
    {
        if (Vijand)
        {
            VijandPositie = Vijand.transform.position;
            transform.LookAt(VijandPositie);
            Debug.Log(VijandPositie);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vijand = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vector3 LastPostition = other.transform.position;
            VijandPositie = LastPostition;
            Vijand = null;
        }
         
    }
}
