using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{

    private GameObject lastHitObject;
    private Material originalMat;
    private bool pause;
    private int menu;
    private int BorD;
    private List<GameObject> turrets;

    public Material hoverMat;
    public GameObject placable;
    public GameObject buildMenu;
    public GameObject BackBtn;

    // Use this for initialization
    void Start()
    {
        lastHitObject = null;
        menu = 0;
        pause = false;
        BorD = 0;
        turrets = new List<GameObject>(4);
    }

    // Update is called once per frame
    void Update()
    {
        pause = Controls.getPause();
        if (pause)
        {
            switch (menu)
            {
                case 0: 
                    buildMenu.SetActive(true);
                    BackBtn.SetActive(false);
                    break;
                case 1:
                    buildMenu.SetActive(false);
                    BackBtn.SetActive(true);
                    break;
            }       

            switch (BorD)
            {
                case 0: break;
                case 1: BuildTurrets(); break;
                case 2: DeleteTurrets(); break;
            }
            
        }
        else
        {
            ReturnColour();
        }
    }

    void ReturnColour()
    {
        if (lastHitObject)
        {
            lastHitObject.GetComponent<Renderer>().material = originalMat;
            lastHitObject = null;
        }
    }

    void BuildTurrets()
    {
        Ray Straal = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(Straal, out hit, 1000) && hit.collider.gameObject.CompareTag("emptyPlane"))
        {

            if (lastHitObject)
            {
                lastHitObject.GetComponent<Renderer>().material = originalMat;
            }
            lastHitObject = hit.collider.gameObject;
            originalMat = lastHitObject.GetComponent<Renderer>().material;
            lastHitObject.GetComponent<Renderer>().material = hoverMat;
        }
        else
        {
            ReturnColour();
        }

        if (Input.GetMouseButtonDown(0) && lastHitObject)
        {
            if (lastHitObject.tag == "emptyPlane")
            {
                Vector3 plaats = lastHitObject.transform.position;
                plaats.y = 0.5f;
                GameObject nieuw = (GameObject)Instantiate(placable, plaats, Quaternion.identity);
                lastHitObject.tag = "occupiedPlane";
                turrets.Add(nieuw);
            }
        }
    }

    void DeleteTurrets()
    {
        buildMenu.SetActive(false);
        BackBtn.SetActive(true);
        Ray Straal = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(Straal, out hit, 1000) && hit.collider.gameObject.CompareTag("occupiedPlane"))
        {

            if (lastHitObject)
            {
                lastHitObject.GetComponent<Renderer>().material = originalMat;
            }
            lastHitObject = hit.collider.gameObject;
            originalMat = lastHitObject.GetComponent<Renderer>().material;
            lastHitObject.GetComponent<Renderer>().material = hoverMat;
        }
        else
        {
            ReturnColour();
        }

        if (Input.GetMouseButtonDown(0) && lastHitObject)
        {
            if (lastHitObject.tag == "occupiedPlane")
            {
                float temp = 10;
                int placeOfObject = 0;
                GameObject other = null;
                lastHitObject.tag = "emptyPlane";
                for (int i = 0; i<turrets.Count; i++)
                {
                    float distance = Vector3.Distance(lastHitObject.transform.position,turrets[i].transform.position);
                    if (distance < temp)
                    {
                        temp = distance;
                        other = turrets[i];
                        placeOfObject = i;
                    }
                }
                turrets.RemoveAt(placeOfObject);
                Destroy(other);
            }
        }
    }

    public void BackButton()
    {
        menu = 0;
        BorD = 0;
    }

    public void BuildButton()
    {
        menu = 1;
        BorD = 1;
    }

    public void DestroyButton()
    {
        menu = 1;
        BorD = 2;
    }
}

