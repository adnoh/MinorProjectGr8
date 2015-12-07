using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObjects : MonoBehaviour
{

    private GameObject lastHitObject;
    private Material originalMat;
    private bool pause;
    private int menu;
    private int BorD;
    private int unitCount;
    private List<GameObject> turrets;

    public Material hoverMat;
    public GameObject placable;
    public GameObject buildMenu;
    public GameObject BackBtn;
    public GameObject IndicationUnits;
    public Text countText;
	
    void Start(){
        lastHitObject = null;
        menu = 0;
        pause = false;
        BorD = 0;
        turrets = new List<GameObject>(4);
    }

    // Update is called once per frame
    void Update(){
        pause = PlayerController.getPause();
        if (pause){
            switch (menu){
                case 0: 
                    buildMenu.SetActive(true);
                    BackBtn.SetActive(false);
                    break;
                case 1:
                    buildMenu.SetActive(false);
                    BackBtn.SetActive(true);
                    break;
            }       

            switch (BorD){    
				case 0: break;
                case 1: BuildTurrets(); break;
                case 2: DeleteTurrets(); break;
            }            
		}
        else{
            ReturnColour();
        }
    }

    void ReturnColour(){
        if (lastHitObject){
            lastHitObject.GetComponent<Renderer>().material = originalMat;
            lastHitObject = null;
        }
    }

    void BuildTurrets(){
        unitCount = PlayerController.getCount();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000,512) && hit.collider.gameObject.CompareTag("emptyPlane")){
            if (lastHitObject){
                lastHitObject.GetComponent<Renderer>().material = originalMat;
            }
            lastHitObject = hit.collider.gameObject;
            originalMat = lastHitObject.GetComponent<Renderer>().material;
            lastHitObject.GetComponent<Renderer>().material = hoverMat;
        }
        else{
            ReturnColour();
        }

        if (Input.GetMouseButtonDown(0) && lastHitObject){
            if (lastHitObject.tag == "emptyPlane"){
                if (unitCount >= 2){
                    Vector3 place = lastHitObject.transform.position;
                    //plaats.y = 0.5f;
                    GameObject newObject = (GameObject)Instantiate(placable, place, Quaternion.identity);
                    newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
                    lastHitObject.tag = "occupiedPlane";
                    turrets.Add(newObject);
                    PlayerController.setCount(2);
                    unitCount = unitCount - 2;
                    countText.text = "Amount of units: " + unitCount.ToString();
                } 
                else if (unitCount < 2){
                    StartCoroutine("TooLittleUnits");
                }
            }
        }
    }

    void DeleteTurrets(){
        buildMenu.SetActive(false);
        BackBtn.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, 512) && hit.collider.gameObject.CompareTag("occupiedPlane")){

            if (lastHitObject){
                lastHitObject.GetComponent<Renderer>().material = originalMat;
            }
            lastHitObject = hit.collider.gameObject;
            originalMat = lastHitObject.GetComponent<Renderer>().material;
            lastHitObject.GetComponent<Renderer>().material = hoverMat;
        }
        else{
            ReturnColour();
        }

        if (Input.GetMouseButtonDown(0) && lastHitObject){
            if (lastHitObject.tag == "occupiedPlane"){
                float temp = 10;
                int placeOfObject = 0;
                GameObject other = null;
                lastHitObject.tag = "emptyPlane";
                for (int i = 0; i<turrets.Count; i++){
                    float distance = Vector3.Distance(lastHitObject.transform.position,turrets[i].transform.position);
                    if (distance < temp){
                        temp = distance;
                        other = turrets[i];
                        placeOfObject = i;
                    }
                }
                turrets.RemoveAt(placeOfObject);
                Destroy(other);
                PlayerController.setCount(-1);
                unitCount = unitCount + 1;
                countText.text = "Count: " + unitCount.ToString();
            }
        }
    }

    public void BackButton(){
        menu = 0;
        BorD = 0;
        IndicationUnits.SetActive(false);
    }

    public void BuildButton(){
        unitCount = PlayerController.getCount();
        if (unitCount >= 2){
            menu = 1;
            BorD = 1;
        }
        else{
            StartCoroutine("TooLittleUnits");
        }
    }

    public void DestroyButton(){
        menu = 1;
        BorD = 2;
        IndicationUnits.SetActive(false);
    }

    IEnumerator TooLittleUnits(){
        IndicationUnits.SetActive(true);
        yield return null;
    }
}

