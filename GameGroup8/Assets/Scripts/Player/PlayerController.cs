using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public Slider playerHealthBar;
	public float RotateSpeed = 30f;
    public GameObject Gate;
    public GameObject BuildMenu;
    public GameObject BackButton;
    public GameObject IndicationUnits;
    public Text countText;
	private static int count;
	public GameObject player;
	private static Vector3 currentPosition;

	public Text levelText;

    public static bool pause;
    private Vector3 playerPos;

	public float regenerationTime = 20.0f;
	private float timeToRegenerate = 2.0f;

	public float energyGainingTime = 2.0f;
	private float timeToGainEnergy = 2.0f;
	
	public Slider energyBar;

	public float flashingInterval = 0.5f;
	private float timeToFlash = 0.0f;
	public Text upgradeText;

	void Start () {
        pause = false;
        BuildMenu.SetActive(false);
		count = 0;
		countText.text = "Amount of units: " + count;
		playerHealthBar.value = PlayerAttributes.getHealth ();
		energyBar.value = PlayerAttributes.getEnergy();
		levelText.text = "Level: " + PlayerAttributes.getLevel();
	}

	void Update(){
		updateBars ();
		levelText.text = "Level: " + PlayerAttributes.getLevel();
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

		if (PlayerAttributes.getEnergy() > 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
			PlayerAttributes.run();
		} else if (PlayerAttributes.getEnergy() <= 0) {
			PlayerAttributes.dontRun();
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			PlayerAttributes.dontRun();
		}

		if (PlayerAttributes.isRunning() && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)) {
			PlayerAttributes.setEnergyDown();
		}

		if (PlayerAttributes.getEnergy() < PlayerAttributes.getMaxEnergy() && Time.time > timeToGainEnergy) {
			PlayerAttributes.replenish();
			timeToGainEnergy = Time.time + energyGainingTime;
		}

		if (PlayerAttributes.getHealth() < PlayerAttributes.getMaxHealth() && Time.time > timeToRegenerate) {
			PlayerAttributes.regenerate ();
			timeToRegenerate = Time.time + regenerationTime;
		}

        if (pause)
        {
            Time.timeScale = 0;
        } 
		else {
            Time.timeScale = 1;
        }

		if (PlayerAttributes.pointsToUpgrade > 0 && Time.time > timeToFlash) {
			timeToFlash = Time.time + flashingInterval;
			if(upgradeText.color == Color.white){
					upgradeText.color = Color.red;
			}
			else{
					upgradeText.color = Color.white;
			}
		}
		if (PlayerAttributes.pointsToUpgrade == 0) {
				upgradeText.color = Color.white;
		}
	}


	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical") * Time.deltaTime;
		
		transform.Translate(PlayerAttributes.getSpeed() * moveHorizontal, 0.0f, PlayerAttributes.getSpeed() * moveVertical, Space.World);

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

	void updateBars(){
		playerHealthBar.maxValue = PlayerAttributes.getMaxHealth ();
		playerHealthBar.value = PlayerAttributes.getHealth();
		energyBar.maxValue = PlayerAttributes.getMaxEnergy ();
		energyBar.value = PlayerAttributes.getEnergy();
	}
}