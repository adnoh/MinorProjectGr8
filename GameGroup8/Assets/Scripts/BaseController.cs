using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{

    private GameObject lastHitObject;
    private Material originalMat;
    public static bool pause;
	public static bool building;
    private List<GameObject> turrets;

    public Material hoverMat;
   
	public GameObject Gate;
    public Text countText;

	public GameObject basicTurret;
	public GameObject catapult;
	public GameObject harpgoon;
	public GameObject snailGun;
	public GameObject gearShack;
	public GameObject generator;
	public GameObject weaponSmith;
	public GameObject gadgetSmith;
	public GameObject bed;
	public GameObject fatiqueBed;
	public GameObject healthBed;

	public GameObject buildMenu;
	public Text title;
	public Text buildingText1;
	public Text buildingText2;
	public Text buildingText3;
	public Text upgradeBuild1;
	public Text upgradeBuild2;
	public Text upgradeBuild3;

	private Vector3 playerPos;
	
    void Start(){
        lastHitObject = null;
        pause = false;
        turrets = new List<GameObject>(4);
		building = false;
    }
	
    void Update(){

		if (Input.GetButtonDown("Jump") && Vector3.Distance(Gate.transform.position, GameObject.Find ("player").transform.position) < 3){
			PlayerAttributes.resetFatique();
			pause = !pause;
			if (pause){
				playerPos = GameObject.Find("player").transform.position;
				transform.position = new Vector3(0, -0.501f, -11.3f);
			} 
			else {
				transform.position = playerPos;
				buildMenu.SetActive(false);
			}
		}

		if (pause)
		{
			Time.timeScale = 0;
		} 
		else {
			Time.timeScale = 1;
		}

		if (lastHitObject != null) {
			setBuildMenu();
		} else {
			buildMenu.SetActive (false);
		}

		if (pause) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
		
			if (Physics.Raycast (ray, out hit, 1000, 512) && !hit.collider.gameObject.CompareTag ("occupiedPlane") && pause) {
				if (lastHitObject) {
					lastHitObject.GetComponent<Renderer> ().material = originalMat;
				}
				lastHitObject = hit.collider.gameObject;
				originalMat = lastHitObject.GetComponent<Renderer> ().material;
				lastHitObject.GetComponent<Renderer> ().material = hoverMat;
			} else {
				ReturnColour ();
			}
		}

		if (lastHitObject != null && Input.GetKeyDown (KeyCode.Alpha1)) {
			Build1st();
		}
		if (lastHitObject != null && Input.GetKeyDown (KeyCode.Alpha2)) {
			Build2nd();
		}
		if (lastHitObject != null && Input.GetKeyDown (KeyCode.Alpha3)) {
			Build3rd();
		}
    }

    void ReturnColour(){
        if (lastHitObject){
            lastHitObject.GetComponent<Renderer>().material = originalMat;
            lastHitObject = null;
        }
    }

	void Delete(){
		float temp = 10;
		int placeOfObject = 0;
		GameObject other = null;
		for (int i = 0; i < turrets.Count; i++) {
			float distance = Vector3.Distance (lastHitObject.transform.position, turrets [i].transform.position);
			if (distance < temp) {
				temp = distance;
				other = turrets [i];
				placeOfObject = i;
			}
		}
		turrets.RemoveAt (placeOfObject);
		Destroy (other);
	}

	void Build1st(){
		string buildingToBuild = buildingText1.text;
		if (buildingToBuild.Equals ("Rock-Paper-Scissor turret")) {
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(basicTurret, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "BasicTurretPlane";
		}
		if (buildingToBuild.Equals ("Cat-a-pult")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(catapult, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
		if (buildingToBuild.Equals ("Energy Boost Bed")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(fatiqueBed, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
		if (buildingToBuild.Equals ("Generator")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(generator, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
	}

	void Build2nd(){
		string buildingToBuild = buildingText2.text;
		if (buildingToBuild.Equals ("Gearshack")) {
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(gearShack, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "GearShackPlane";
		}
		if (buildingToBuild.Equals ("Harpgoon")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(harpgoon, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
		if (buildingToBuild.Equals ("Health Boost Bed")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(healthBed, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
		if (buildingToBuild.Equals ("Gun Smith")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(weaponSmith, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
	}

	void Build3rd(){
		string buildingToBuild = buildingText3.text;
		if (buildingToBuild.Equals ("Bed")) {
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(bed, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "BedPlane";
		}
		if (buildingToBuild.Equals ("Snail Gun")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(snailGun, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
		if (buildingToBuild.Equals ("Tech Smith")) {
			Delete();
			Vector3 place = lastHitObject.transform.position;
			GameObject newObject = (GameObject)Instantiate(gadgetSmith, place, Quaternion.identity);
			newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			turrets.Add(newObject);
			lastHitObject.tag = "occupiedPlane";
		}
	}

	void setBuildMenu(){
		if (lastHitObject.CompareTag ("emptyPlane")) {
			title.text = "Empty spot";
			buildingText1.text = "Rock-Paper-Scissor turret";
			buildingText2.text = "Gearshack";
			buildingText3.text = "Bed";
			upgradeBuild1.text = "Build";
			upgradeBuild2.text = "Build";
			upgradeBuild3.text = "Build";
			buildMenu.SetActive (true);
		}
		if (lastHitObject.CompareTag ("BasicTurretPlane")) {
			title.text = "Rock-Paper-Scissor turret";
			buildingText1.text = "Cat-a-pult";
			buildingText2.text = "Harpgoon";
			buildingText3.text = "Snail Gun";
			upgradeBuild1.text = "Upgrade";
			upgradeBuild2.text = "Upgrade";
			upgradeBuild3.text = "Upgrade";
			buildMenu.SetActive (true);
		}
		if (lastHitObject.CompareTag ("BedPlane")) {
			title.text = "Bed";
			buildingText1.text = "Energy Boost Bed";
			buildingText2.text = "Health Boost Bed";
			buildingText3.text = "";
			upgradeBuild1.text = "Upgrade";
			upgradeBuild2.text = "Upgrade";
			upgradeBuild3.text = "";
			buildMenu.SetActive (true);
		}
		if (lastHitObject.CompareTag ("GearShackPlane")) {
			title.text = "Gearshack";
			buildingText1.text = "Generator";
			buildingText2.text = "Gun Smith";
			buildingText3.text = "Tech Smith";
			upgradeBuild1.text = "Upgrade";
			upgradeBuild2.text = "Upgrade";
			upgradeBuild3.text = "Upgrade";
			buildMenu.SetActive (true);
		}
	}
}

