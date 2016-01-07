using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour{

    private GameObject lastHitObject;
    private GameObject buildObject;
    private Material originalMat;
    public static bool pause;
	public static bool building;
    public static List<GameObject> turrets;

    public Material hoverMat;
    
    public GameObject BaseMenu;
    public GameObject WeaponMenu;
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
	public GameObject healthBed;
	public GameObject energyBed;

	public GameObject buildMenu;
	public Text title;
	public Text buildingText1;
	public Text buildingText2;
	public Text buildingText3;
	public Text unitCost1;
	public Text unitCost2;
	public Text unitCost3;
	public Text upgradeBuild1;
	public Text upgradeBuild2;
	public Text upgradeBuild3;

	private Vector3 playerPos;
    
    private int First_Building = 5;
    private int Second_Building = 10;
    private int Third_Buidling = 15;
    private int Fourth_Building = 20;

    Score score_;

    public int baseHealth = 1000;
    public int wall = 0;
    public Slider healthSlider;

    public GameObject Wall_1;
    public GameObject Wall_2;
    public GameObject Wall_3;

    void Awake(){

        score_ = Camera.main.GetComponent<Score>();
        lastHitObject = null;
        pause = false;
        turrets = new List<GameObject>(4);
		building = false;
    }
	
    void Update(){

        healthSlider.value = baseHealth;
        
        if (Vector3.Distance(Gate.transform.position, GameObject.Find("player").transform.position) < 50)
            Analytics.set_timeCTBase();
        
		if (Input.GetButtonDown("Jump") && Vector3.Distance(Gate.transform.position, GameObject.Find ("player").transform.position) < 3){
			PlayerAttributes.resetFatique();
			pause = !pause;
			if (pause){
				playerPos = GameObject.Find("player").transform.position;
                Vector3 TempPlayerPos = GameObject.FindGameObjectWithTag("BASE").transform.position - new Vector3(0,1f,5.31f);
				GameObject.Find("player").transform.position = TempPlayerPos;
                BaseMenu.SetActive(true);
                Analytics.set_timeBase();
			} 
			else {
                GameObject.Find("player").transform.position = playerPos;
				GameObject.Find ("player").GetComponent<PlayerAttacker>().weaponUnlockScreen.SetActive(false);
                BaseMenu.SetActive(false);
                ReturnColour();
			}
		}

		if (pause)
		{
			Time.timeScale = 0;
		} 
		if(!pause && !GameObject.Find("player").GetComponent<PlayerController>().death) {
			Time.timeScale = 1;
		}

		if (lastHitObject != null && Input.GetMouseButton(0)) {
			setBuildMenu();
            building = true;
		} 

		if (pause & !building) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
		
			if (Physics.Raycast (ray, out hit, 1000, 512) /*&& !hit.collider.gameObject.CompareTag ("occupiedPlane")*/ && pause) {
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
        if (lastHitObject != null && Input.GetKeyDown(KeyCode.Alpha4))
        {
            RemoveBuilding();
        }
    }

    void ReturnColour(){
        if (lastHitObject){
            lastHitObject.GetComponent<Renderer>().material = originalMat;
            lastHitObject = null;
        }
    }

	void Delete(){
        if (turrets.Count != 0)
        {
            float temp = 3;
            int placeOfObject = 0;
            GameObject other = null;
            for (int i = 0; i < turrets.Count; i++)
            {
                float distance = Vector3.Distance(lastHitObject.transform.position, turrets[i].transform.position);
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

    public void RemoveBuilding()
    {
        //Debug.Log("delete");
        float temp = 3;
        int placeOfObject = 0;
        GameObject other = null;
        for (int i = 0; i < turrets.Count; i++)
        {
            float distance = Vector3.Distance(lastHitObject.transform.position, turrets[i].transform.position);
            if (distance < temp)
            {
                temp = distance;
                other = turrets[i];
                placeOfObject = i;
            }
        }
        if (other != null)
        {
            Building building = other.GetComponent<BuildingController>().getBuilding();
            PlayerController.setCount((int)Mathf.Floor(-building.getCost() * 0.5f));
            countText.text = "Amount of units: " + PlayerController.getCount();

            turrets.RemoveAt(placeOfObject);
            Destroy(other);
            lastHitObject.tag = "emptyPlane";
            setBuildMenu();
        }
    }

    public void Build1st(){
        //Debug.Log("build 1");
		string buildingToBuild = buildingText1.text;
		BuildingFactory buildingFactory = new BuildingFactory ();
		Building building = buildingFactory.getBuilding (buildingToBuild);
		if(PlayerController.getCount() >= building.getCost()){
		    if (buildingToBuild.Equals ("Rock-Paper-Scissor turret")) {
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(basicTurret, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "BasicTurretPlane";
                Analytics.setBuildings(1);
            }
		    if (buildingToBuild.Equals ("Cat-a-pult")) {
			    Delete();
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(catapult, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(0, (Random.Range(0, 360)), 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "occupiedPlane";
                Analytics.setBuildings(2);
            }
		    if (buildingToBuild.Equals ("Energy Boost Bed")) {
			    Delete();
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(energyBed, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(-90, 0, 0));
			    newObject.transform.Translate(new Vector3(0, 1.055501f, 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "occupiedPlane";
		    }
		    if (buildingToBuild.Equals ("Generator")) {
			    Delete();
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(generator, place, Quaternion.identity);
			    newObject.transform.Translate(new Vector3(0, 0.2050993f, 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "occupiedPlane";
		    }
            UpdateUnits(building);
            score_.addScoreBuilding(building.getCost());
            setBuildMenu();
        }
	}

	public void Build2nd(){
        //Debug.Log("build 2");
        string buildingToBuild = buildingText2.text;
		BuildingFactory buildingFactory = new BuildingFactory ();
		Building building = buildingFactory.getBuilding (buildingToBuild);
		if(PlayerController.getCount() >= building.getCost()){
		    if (buildingToBuild.Equals ("Gearshack")) {
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(gearShack, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(-90, (Random.Range(0, 360)), 0));
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
                Analytics.setBuildings(4);
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
			    newObject.transform.Rotate(new Vector3(-90, 0, 0));
			    newObject.transform.Translate(new Vector3(0, 0.1957196f, 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "Gunsmith";
		    }
            UpdateUnits(building);
            score_.addScoreBuilding(building.getCost());
            setBuildMenu();
        }
	}

	public void Build3rd(){
        //Debug.Log("build 3");
        string buildingToBuild = buildingText3.text;
		BuildingFactory buildingFactory = new BuildingFactory ();
		Building building = buildingFactory.getBuilding (buildingToBuild);
		if(PlayerController.getCount() >= building.getCost()){
		    if (buildingToBuild.Equals ("Bed")) {
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(bed, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(-90, 0, 0));
			    newObject.transform.Translate(new Vector3(0, 0.06193482f, 0));
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
                Analytics.setBuildings(3);
		    }
		    if (buildingToBuild.Equals ("Tech Smith")) {
			    Delete();
			    Vector3 place = lastHitObject.transform.position;
			    GameObject newObject = (GameObject)Instantiate(gadgetSmith, place, Quaternion.identity);
			    newObject.transform.Rotate(new Vector3(-90, (Random.Range(0, 360)), 0));
			    turrets.Add(newObject);
			    lastHitObject.tag = "occupiedPlane";
		    }
            UpdateUnits(building);
            score_.addScoreBuilding(building.getCost());
            setBuildMenu();
        }
	}

	void setBuildMenu(){
		if (lastHitObject.CompareTag ("emptyPlane")) {
			title.text = "Empty spot";
			buildingText1.text = "Rock-Paper-Scissor turret";
			buildingText2.text = "Gearshack";
			buildingText3.text = "Bed";
			unitCost1.text = "Cost: " + First_Building;
			unitCost2.text = "Cost: " + First_Building;
			unitCost3.text = "Cost: " + Second_Building;
			upgradeBuild1.text = "Build(1)";
			upgradeBuild2.text = "Build(2)";
			upgradeBuild3.text = "Build(3)";
			buildMenu.SetActive (true);
            WeaponMenu.SetActive(false);
		}
		if (lastHitObject.CompareTag ("BasicTurretPlane")) {
			title.text = "Rock-Paper-Scissor turret";
			buildingText1.text = "Cat-a-pult";
			buildingText2.text = "Harpgoon";
			buildingText3.text = "Snail Gun";
			unitCost1.text = "Cost: " + Second_Building; 
			unitCost2.text = "Cost: " + Second_Building;
            unitCost3.text = "Cost: " + Second_Building;
            upgradeBuild1.text = "Upgrade(1)";
			upgradeBuild2.text = "Upgrade(2)";
			upgradeBuild3.text = "Upgrade(3)";
			buildMenu.SetActive (true);
		}
		if (lastHitObject.CompareTag ("BedPlane")) {
			title.text = "Bed";
			buildingText1.text = "Energy Boost Bed";
			buildingText2.text = "Health Boost Bed";
			buildingText3.text = "";
			unitCost1.text = "Cost: " + Third_Buidling;
			unitCost2.text = "Cost: " + Third_Buidling;
			unitCost3.text = "";
			upgradeBuild1.text = "Upgrade(1)";
			upgradeBuild2.text = "Upgrade(2)";
			upgradeBuild3.text = "";
			buildMenu.SetActive (true);
		}
		if (lastHitObject.CompareTag ("GearShackPlane")) {
			title.text = "Gearshack";
			buildingText1.text = "Generator";
			buildingText2.text = "Gun Smith";
			buildingText3.text = "Tech Smith";
			unitCost1.text = "Cost: " + Fourth_Building;
			unitCost2.text = "Cost: " + Fourth_Building;
            unitCost3.text = "Cost: " + Fourth_Building;
            upgradeBuild1.text = "Upgrade";
			upgradeBuild2.text = "Upgrade";
			upgradeBuild3.text = "Upgrade";
			buildMenu.SetActive (true);
		}
        if (lastHitObject.CompareTag("occupiedPlane"))
        {
            title.text = "Maxed out";
            buildingText1.text = "";
            buildingText2.text = "";
            buildingText3.text = "";
            unitCost1.text = "";
            unitCost2.text = "";
            unitCost3.text = "";
            upgradeBuild1.text = "";
            upgradeBuild2.text = "";
            upgradeBuild3.text = "";
            buildMenu.SetActive(true);
        }
        if (lastHitObject.CompareTag("Gunsmith"))
        {
            WeaponMenu.SetActive(true);
            buildMenu.SetActive(false);
        }
    }

    void UpdateUnits(Building building)
    {
        PlayerController.setCount(building.getCost());
        countText.text = "Amount of units: " + PlayerController.getCount();
    }

    public void buildFromSave(){

        matchWalls();

		var Temp = MonsterCollection.turretLoad("Assets/saves/turrets.xml");
		var TurretList = Temp.getTurretList();

		for(int i = 0; i < TurretList.Length; i++){
			switch(TurretList[i].name)
			{
				case "Rock-paper-scissor turret":
				{
					GameObject basicTurretClone = (GameObject)Instantiate(basicTurret, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					basicTurretClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					basicTurretClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(basicTurretClone);
					break;
				}
				case "Snailgun":
				{
					GameObject snailGunClone = (GameObject)Instantiate(snailGun, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					snailGunClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					snailGunClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(snailGunClone);
					break;
				}
				case "Harpgoon":
				{
					GameObject harpgoonClone = (GameObject)Instantiate(harpgoon, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					harpgoonClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					harpgoonClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(harpgoonClone);
					break;
				}
				case "Cat-a-pult":
				{
					GameObject catapultClone = (GameObject)Instantiate(catapult, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					catapultClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					catapultClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(catapultClone);
					break;
				}
				case "Bed":
				{
					GameObject bedClone = (GameObject)Instantiate(bed, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					bedClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					bedClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(bedClone);
					break;
				}
				case "EnergyBed":
				{
					GameObject energyBedClone = (GameObject)Instantiate(energyBed, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					energyBedClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					energyBedClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(energyBedClone);
					break;
				}
				case "HealthBed":
				{
					GameObject healthBedClone = (GameObject)Instantiate(healthBed, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					healthBedClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					healthBedClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(healthBedClone);
					break;
				}
				case "Gearshack":
				{
					GameObject gearshackClone = (GameObject)Instantiate(gearShack, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					gearshackClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					gearshackClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(gearshackClone);
					break;
				}
				case "Generator":
				{
					GameObject generatorClone = (GameObject)Instantiate(generator, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					generatorClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					generatorClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(generatorClone);
					break;
				}
				case "GunSmith":
				{
					GameObject gunSmithClone = (GameObject)Instantiate(weaponSmith, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					gunSmithClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					gunSmithClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(gunSmithClone);
					break;
				}
				case "TechSmith":
				{
					GameObject techSmithClone = (GameObject)Instantiate(gadgetSmith, new Vector3(TurretList[i].x, TurretList[i].y, TurretList[i].z), new Quaternion(TurretList[i].xRot, TurretList[i].yRot, TurretList[i].zRot, TurretList[i].wRot));
					techSmithClone.GetComponent<BuildingController>().timeToNextAttack = TurretList[i].timeTillNextAttack;
					techSmithClone.GetComponent<BuildingController>().timeInterval = TurretList[i].timeTillNext;
					turrets.Add(techSmithClone);
					break;
				}
			}
		}
        //Debug.Log("Base loaded");
	}

    public static bool getPause()
    {
        return pause;
    }

    public void closeBuildMenu()
    {
        buildMenu.SetActive(false);
        building = false;
    }
    
    public void closeWeaponMenu()
    {
        WeaponMenu.SetActive(false);
        building = false;
    }

    void matchWalls()
    {
        switch (wall)
        {
            case 0:
                healthSlider.maxValue = 1000;
                Wall_1.SetActive(true);
                Wall_2.SetActive(false);
                Wall_3.SetActive(false);
                break;
            case 1:
                healthSlider.maxValue = 2000;
                Wall_1.SetActive(false);
                Wall_2.SetActive(true);
                Wall_3.SetActive(false);
                break;
            case 2:
                healthSlider.maxValue = 3000;
                Wall_1.SetActive(false);
                Wall_2.SetActive(false);
                Wall_3.SetActive(true);
                break;
        }
    }

    public void UpgradeWalls()
    {
        if (PlayerController.getCount() >= 20)
        {
            wall++;
            baseHealth += 1000;
            matchWalls();

            PlayerController.setCount(20);
            countText.text = "Amount of units: " + PlayerController.getCount();
        }

        if (wall >= 2)
            GameObject.Find("WallUpBtn").SetActive(false);
    }

    public void RepareWalls()
    {
        baseHealth += 50;

        PlayerController.setCount(5);
        countText.text = "Amount of units: " + PlayerController.getCount();
    }
}

