using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	public Slider playerHealthBar;
	public float RotateSpeed = 30f;
    
    public Text countText;
	private static int count;
	public GameObject player;
	private static Vector3 currentPosition;
	private static Quaternion currentrotation;

	public Text levelText;

	public Slider fatiqueBar;    

	public float regenerationTime = 20.0f;
	public float regenerationTimeInBase = 20.0f;
	private float timeToRegenerate = 20.0f;

	public static int amountOfBeds = 0;

	public float energyGainingTime = 2.0f;
	private float timeToGainEnergy = 2.0f;
	
	public Slider energyBar;

	public float flashingInterval = 0.5f;
	private float timeToFlash = 0.0f;
	public Text upgradeText;

	public int amountOfDeaths;
	public bool death = false;
	public GameObject deathScreen;

	public Text textOnDeathScreen;
	public Text scoreOnDeathScreen;

	public Animator playerAnimator;

    public bool binded;
    public float speedMultiplier = 1f;

    private SoundsPlayer PlayerSounds;
    private bool running;

    public Text healthText;
    public Text energyText;

	public void Start(){
        playerAnimator = GetComponent<Animator>();
        PlayerSounds = gameObject.GetComponent<SoundsPlayer>();         // LoadSound
        running = false;
    }

	public void FirstLoad() {
        speedMultiplier = 1f;
        count = 0;
		countText.text = "Amount of units: " + count;
		playerHealthBar.value = PlayerAttributes.getHealth ();
		energyBar.value = PlayerAttributes.getEnergy();
		levelText.text = "Level: " + PlayerAttributes.getLevel();
        PlayerAttributes.reset();
        PlayerAttacker.unlocked[1] = true;
        for (int i = 2; i < 8; i++)
        {
            PlayerAttacker.unlocked[i] = false;
        }
        PlayerAttacker.currentWeapon = new WeaponFactory().getPistol();
        
    }

	void Update(){
        healthText.text = PlayerAttributes.getHealth() + " / " + PlayerAttributes.getMaxHealth();
        energyText.text = PlayerAttributes.getEnergy() + " / " + PlayerAttributes.getMaxEnergy();
        Analytics.set_timeOutside();
        Analytics.setScore(Camera.main.GetComponent<Score>().getScore());

		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			playerAnimator.SetInteger ("weapon", 2);
		}
		updateBars ();
		countText.text = "Amount of units: " + count;
		levelText.text = "Level: " + PlayerAttributes.getLevel();
		Plane playerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;
		if (playerPlane.Raycast (ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint (hitdist);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
		}

		if (PlayerAttributes.getEnergy() > 0 && Input.GetKeyDown (KeyCode.LeftShift)) {
			playerAnimator.SetBool ("running", true);
			PlayerAttributes.run();
            running = true;                                         // For sound only
		} else if (PlayerAttributes.getEnergy() <= 0) {
			playerAnimator.SetBool ("running", false);
			PlayerAttributes.dontRun();
            running = false;                                        // For sound only
        }

		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			playerAnimator.SetBool ("running", false);
			PlayerAttributes.dontRun();
            running = false;                                        // For sound only
        }

		if (PlayerAttributes.isRunning() && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)) {
			PlayerAttributes.setEnergyDown();
		}

		if (PlayerAttributes.getEnergy() < PlayerAttributes.getMaxEnergy() && Time.time > timeToGainEnergy) {
			PlayerAttributes.replenish();
			timeToGainEnergy = Time.time + energyGainingTime;
		}

		if (PlayerAttributes.getHealth() < PlayerAttributes.getMaxHealth() && Time.time > timeToRegenerate && !BaseController.pause) {
			PlayerAttributes.regenerate ();
			timeToRegenerate = Time.time + regenerationTime;
		}
		if (PlayerAttributes.getHealth() < PlayerAttributes.getMaxHealth() && Time.time > timeToRegenerate && BaseController.pause) {
			PlayerAttributes.regenerate ();
			timeToRegenerate = Time.time + regenerationTimeInBase / (1 + amountOfBeds);
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
		PlayerAttributes.getTired ();

		if (PlayerAttributes.getHealth() <= 0) {
			playerAnimator.SetBool("dieing", true);
			death = true;
            PlayerSounds.PlayDead();                                // Sound
			PlayerAttributes.setHealth(1);
			deathScreen.SetActive(true);
			amountOfDeaths ++;
			textOnDeathScreen.text = "You've died " + amountOfDeaths + "time(s) so far /n Do you want to play again?";
			scoreOnDeathScreen.text = "You scored: " + Score.score + "/n" + "Do you want to submit your score?";
		}

		if (death) {
			Time.timeScale = 0;
		}
		if (!death) {
			Time.timeScale = 1;
		}
	}


	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical") * Time.deltaTime;
		if ((moveHorizontal != 0 || moveVertical != 0) && !binded) {
			playerAnimator.SetBool ("walking", true);
			transform.Translate (speedMultiplier * PlayerAttributes.getSpeed () * moveHorizontal, 0.0f, speedMultiplier * PlayerAttributes.getSpeed () * moveVertical, Space.World);
            if (running == false)
            {
                PlayerSounds.PlayWalk();                            // Sound
            }
            else if (running == true)
            {
                PlayerSounds.PlayRun();                             // Sound
            }
		} else {
			playerAnimator.SetBool ("walking", false);
            PlayerSounds.StopWalk();                                // Sound
		}
			
        
		Vector3 playerPos = player.transform.position;
		setPosition (playerPos);

		var playerRot = player.transform.rotation;
		setRotation (playerRot);

	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Pick-Up") && this.gameObject.name.Equals ("player")){
			Destroy (collider.gameObject);
			count ++;
			countText.text = "Amount of units: " + count;
		}
        if (collider.gameObject.CompareTag("Health-Pick-Up") && this.gameObject.name.Equals("player"))
        {
            Destroy(collider.gameObject);
            if((PlayerAttributes.getMaxHealth() - PlayerAttributes.getHealth()) > 20)
            {
                PlayerAttributes.setHealth(PlayerAttributes.getHealth() + 20);
            }
            else
            {
                PlayerAttributes.setHealth(PlayerAttributes.getMaxHealth());
            }
        }
        if (collider.gameObject.CompareTag("Energy-Pick-Up") && this.gameObject.name.Equals("player"))
        {
            Destroy(collider.gameObject);
            if ((PlayerAttributes.getMaxEnergy() - PlayerAttributes.getEnergy()) > 20)
            {
                PlayerAttributes.setEnergy(PlayerAttributes.getEnergy() + 20);
            }
            else
            {
                PlayerAttributes.setEnergy(PlayerAttributes.getMaxEnergy());
            }
        }
        if (collider.gameObject.CompareTag("Fatique-Pick-Up") && this.gameObject.name.Equals("player"))
        {
            Destroy(collider.gameObject);
            PlayerAttributes.resetFatique();
        }
        if (collider.gameObject.CompareTag ("Enemy")) {
			collider.gameObject.GetComponent<EnemyController> ().setWithinRange();
		}
	}

	void OnTriggerExit(Collider collider){
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (collider.gameObject.GetComponent<EnemyController>().getWithinRange())
            {
                collider.gameObject.GetComponent<EnemyController>().setWithinRange();
            }
        }
    }

	void setPosition(Vector3 here){
		currentPosition = here;
	}

	void setRotation(Quaternion rot){
		currentrotation = rot;
	}

	public static Vector3 getPosition(){
		return currentPosition;
	}

	public static Quaternion getRotation(){
		return currentrotation;
	}

    public static int getCount(){
        return count;
    }

    public static void setCount(int change){
        count -= change;
    }

    public static void setCount_2(int change)
    {
        count = change;
    }

    public void bind(bool b)
    {
        binded = b;
    }

    void updateBars(){
		playerHealthBar.maxValue = PlayerAttributes.getMaxHealth ();
		playerHealthBar.value = PlayerAttributes.getHealth();
		energyBar.maxValue = PlayerAttributes.getMaxEnergy ();
		energyBar.value = PlayerAttributes.getEnergy();
        fatiqueBar.maxValue = PlayerAttributes.getMaxFatique();
		fatiqueBar.value = PlayerAttributes.getFatique ();
	}

	public void PlayAgain(){
		MiniMapScript.clearEnemies ();		
		GameStateController.setNewgame (true);
		// Application.LoadLevel (1);
		SceneManager.LoadScene(1);
	}

	public void Quit(){
		MiniMapScript.clearEnemies ();
		SceneManager.LoadScene (0);
	}
		
}