using UnityEngine;

public class PlaceObjects : MonoBehaviour {

    private GameObject lastHitObject;
    private Material originalMat;
    private bool pause;

    public Material hoverMat;
    public GameObject placable;

	// Use this for initialization
	void Start () {
        lastHitObject = null;
	}
	
	// Update is called once per frame
	void Update () {
        
        MenuControl();
        if (pause)
        {
            BuildTurrets();
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

    void MenuControl()
    {
        pause = Controls.getPause();
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
            }
        }
    }
}

