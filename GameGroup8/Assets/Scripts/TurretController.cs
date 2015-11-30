using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    private GameObject Vijand;
    private Vector3 VijandPositie;
    
    void Update()
    {
        if (Vijand)
        {
            VijandPositie = Vijand.transform.position;
            VijandPositie.y = 0;
            transform.LookAt(VijandPositie);
            transform.Rotate(new Vector3 (0,1,0), 90);
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
