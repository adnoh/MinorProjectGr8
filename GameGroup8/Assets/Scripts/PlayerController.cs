using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private static int playerHealth;
	public Slider playerHealthBar;
	public float walkingSpeed;
	public float RotateSpeed = 30f;
    public GameObject Gate;
    public GameObject BuildMenu;
    public GameObject BackButton;
    public GameObject IndicationUnits;
    public Text countText;
	private static int count;
	public GameObject player;
	private static Vector3 currentPosition;

    private static bool pause;
    private Vector3 playerPos;

	public float regenerationTime = 20.0f;
	private float timeToRegenerate = 0.0f;

	public float energyGainingTime = 2.0f;
	private float timeToGainEnergy = 0.0f;

	public int energy;
	public Slider energyBar;

	void Start () {
        pause = false;
        BuildMenu.SetActive(false);
		count = 0;
		countText.text = "Amount of units: " + count;
		playerHealth = 100;
		playerHealthBar.value = playerHealth;
		walkingSpeed = 5;
		energy = 100;
		energyBar.value = energy;
	}

	void Update(){
		updateBars ();
		Plane playerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;
		if (playerPlane.Raycast (ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint (hitdist);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
		}

        if (Input.GetButtonDown("Jump") && Vector3.Distance(Gate.transform.position,transform.position) < 3){
            pause = !pause;
            if (pause){
                playerPos = transform.position;
                transform.position = new Vector3(0, -0.501f, -11.3f);
            } 
			else {
                transform.position = playerPos;
                BuildMenu.SetActive(false);
                BackButton.SetActive(false);
                IndicationUnits.SetActive(false);
            }
        }

		if (energy > 0 && Input.GetKeyDown (KeyCode.R)) {
			walkingSpeed = 10;
		} else if (energy <= 0) {
			walkingSpeed = 5;
		}

		if (Input.GetKeyUp(KeyCode.R)) {
			walkingSpeed = 5;
		}

		if (walkingSpeed == 10 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)) {
			energy --;
		}

		if (energy < 100 && Time.time > timeToGainEnergy) {
			energy++;
			timeToGainEnergy = Time.time + energyGainingTime;
		}

		if (playerHealth < 100 && Time.time > timeToRegenerate) {
			playerHealth++;
			timeToRegenerate = Time.time + regenerationTime;
		}

        if (pause)
        {
            Time.timeScale = 0;
        } 
		else {
            Time.timeScale = 1;
        }
	}


	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical") * Time.deltaTime;
		
		transform.Translate(walkingSpeed * moveHorizontal, 0.0f, walkingSpeed * moveVertical, Space.World);

		Vector3 playerPos = player.transform.position;
		setPosition (playerPos);
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Pick-Up")){
			Destroy (collider.gameObject);
			count ++;
			countText.text = "Amount of units: " + count;
		}
		if (collider.gameObject.CompareTag ("Enemy")) {
			collider.gameObject.GetComponent<EnemyController> ().setWithinRange();
		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.gameObject.CompareTag ("Enemy")) {
			collider.gameObject.GetComponent<EnemyController> ().setWithinRange ();
		}
	}

    static public bool getPause(){
        return pause;
    }

	void setPosition(Vector3 here){
		currentPosition = here;
	}

	public static Vector3 getPosition(){
		return currentPosition;
	}

    public static int getCount(){
        return count;
    }

    public static void setCount(int change){
        count -= change;
    }

	public static void setHealth(int damage){
		playerHealth -= damage;
	}

	void updateBars(){
		playerHealthBar.value = playerHealth;
		energyBar.value = energy;
	}
}