using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/* this class actually lives in the scene, Enemy.cs not 
can also access all variables defined in Enemy.cs
*/

public class EnemyController : MonoBehaviour {

	public Enemy enemy;
	private EnemyFactory enemyFactory = new EnemyFactory();

    // Properties of the actual instance of the enemy
	public int level;
	public int health;
	public int maxHealth;
	public int attackPower;
	public float walkingSpeed;
    public float updatedSpeed;
	public Type type;

    public Vector3 position;
    public Quaternion rotation;

    public bool isWithinRange;
    public bool baseWithinRange;

	public float attackRate = 2f;
	private float nextAttack = 0.0f;

	public bool poisoned;
	public float timeToGetPoisonDamage = 5.0f;
	private float intervalToGetPoisonDamage = 5.0f;

	public bool stunned;
	public float timeToUnStun = 0.0f;
	public float stunTimeInterval = 2.0f;

	public bool destroyed = false;

	private Animator anim;

    public bool dead = false;
    public bool shotByPlayer = false;

    public Slider healthBar;
    public Slider healthBarClone;
    public Text description;
    public Text descriptionClone;

    public GameObject bullet;
    public GameObject oilSpot;
    public bool hasShotOil;

    public bool wandering = true;
    public float wanderingTime;
    public Vector3 currentWanderingDirection;
    public float timeToChangeDirection;

    private List<EnemyController> otherEnemies = new List<EnemyController>();

    AudioSource[] Sound;                                                    // sound
    private bool SoundPeem = false;

	string name;

	/// <summary>
	/// Sets all fields to default when an enemy is spawned.
	/// </summary>
    void Start()
    {
		name = gameObject.name;
        wanderingTime = Random.Range(5.0f, 15.0f) + Time.time;
        timeToChangeDirection = 1.0f + Time.time;
        otherEnemies = Camera.main.GetComponent<EnemySpawner>().unbuffedEnemies;
        level = this.gameObject.GetComponent<EnemyController>().getLevel();
        SoundsEnemies Source = Camera.main.GetComponent<SoundsEnemies>();   // sound
        if (name.Equals("HammerHeadPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("Hammerhead", level);
            Sound = Source.loadSharkSounds(gameObject);                     // sound
        }
        else if (name.Equals("DesertEaglePrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("DesertEagle", level);
            Sound = Source.loadEagleSounds(gameObject);                     // sound
        }
        else if (name.Equals("FireFoxPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("FireFox", level);
            Sound = Source.loadFireFoxSounds(gameObject);                   // sound
        }
        else if (name.Equals("PolarBearPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("PolarBear", level);
            Sound = Source.loadPolarBearSounds(gameObject);                 // sound
        }
        else if (name.Equals("MeepMeepPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("MeepMeep", level);
            Sound = Source.loadMeepSounds(gameObject);                      // sound
        }
        else if(name.Equals("OilphantPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("Oilphant", level);
            Sound = Source.loadOilfantSounds(gameObject);                   // sound
        }

        level = enemy.getLevel();
        maxHealth = enemy.getMaxHealth();
        health = enemy.getMaxHealth();
        attackPower = enemy.getAttackPower();
        walkingSpeed = enemy.getWalkingSpeed();
        updatedSpeed = enemy.getWalkingSpeed();
        type = enemy.getType();

        isWithinRange = false;
        MiniMapScript.enemies.Add(this);
        anim = GetComponent<Animator>();
        healthBarClone = Instantiate(healthBar);
        healthBarClone.maxValue = health;
        descriptionClone = Instantiate(description);
        descriptionClone.text = "Lvl:" + enemy.getLevel() + " . " + enemy.getType().toString();
        if (name.Equals("PolarBearPrefab(Clone)"))
        {
            gameObject.GetComponent<Seeker>().toBase = false;
            shotByPlayer = true;
        }
    }

	/// <summary>
	/// Updates the the gameObject every frame, like: walking to base, to the player or wander; attack if in range; get damage if poisoned;
	/// checks line of sight if player is close enough.
	/// </summary>
	void Update () {
        if(otherEnemies.Count == 0){
            otherEnemies = Camera.main.GetComponent<EnemySpawner>().unbuffedEnemies;
        }
		updateSpeed ();
		updateHealthBarAndDescription ();
		if (!shotByPlayer && !wandering) {
			walkToBase ();
		} else {
			walkToPlayer ();
		}
        
		if (Time.time > nextAttack && !name.Equals("MeepMeepPrefab(Clone)") && !wandering) {
            if (isWithinRange){
                nextAttack = Time.time + attackRate;
                attack();
            }
            else if (baseWithinRange){
                nextAttack = Time.time + attackRate;
                attackBase();
            }
		}

        if(name.Equals("OilphantPrefab(Clone)") && !hasShotOil && !dead && Vector3.Distance(this.gameObject.transform.position, GameObject.Find("player").transform.position) < 5){
            Instantiate(oilSpot, GameObject.Find("player").transform.position, Quaternion.identity);
            hasShotOil = true;
            StartCoroutine(attackAnimation("oliesquirt"));
        }

		if (poisoned && Time.time > timeToGetPoisonDamage) {
			timeToGetPoisonDamage = Time.time + intervalToGetPoisonDamage;
			health -= (int)(maxHealth / 20);
		}

		if (stunned && Time.time > timeToUnStun) {
			unStun ();
		}

		Vector3 EnemyPosition = gameObject.transform.position;
		setPosition (EnemyPosition);
        Quaternion EnemyRotation = gameObject.transform.rotation;
        setRotation(EnemyRotation);

        if (name.Equals("MeepMeepPrefab(Clone)") && otherEnemies.Count > 0 && !wandering){
			PeemPeemSound ();
			sortOtherEnemiesToDistance ();
            gameObject.transform.LookAt(otherEnemies[0].gameObject.transform);
            transform.position = Vector3.MoveTowards(transform.position, otherEnemies[0].gameObject.transform.position, updatedSpeed * Time.deltaTime);
        }
        else if(name.Equals("MeepMeepPrefab(Clone)") && otherEnemies.Count == 0){
			PeemPeemSound ();
            wandering = true;
            wanderingTime = 1f + Time.time;
        }

        if(wanderingTime - Time.time > 0){
			wanderAround ();
        }
        else{
            wandering = false;
        }
		setPlayerWithinRange ();
        
    }

	/// <summary>
	/// Gets the health. Used when the game is saved.
	/// </summary>
	/// <returns>The health.</returns>
	public int getHealth() {
		return health;
	}

	/// <summary>
	/// Gets the max health. Used when the game is saved.
	/// </summary>
	/// <returns>The max health.</returns>
	public int getMaxHealth() {
		return maxHealth;
	}

	/// <summary>
	/// Sets the health first time. Used when a game is loaded.
	/// </summary>
	/// <param name="health">Health.</param>
    public void setHealthFirstTime(int health){
        this.health = health;
    }

	/// <summary>
	/// Changes the health when it's attacked. Shows their health bar when it's not visible. And it stops wandering.
	/// </summary>
	/// <param name="health">Health.</param>
	public void setHealth(int health) {
		this.health = health;
        wanderingTime = -1f;
        healthBarClone.transform.SetParent(Camera.main.GetComponent<EnemySpawner>().EnemyHealthBars.transform, false);
        descriptionClone.transform.SetParent(Camera.main.GetComponent<EnemySpawner>().EnemyHealthBars.transform, false);
		if(!name.Equals("MeepMeepPrefab(Clone)")){
			gameObject.GetComponent<Seeker>().toBase = false;
			gameObject.GetComponent<Seeker>().StopAllCoroutines();
		}
    }

	/// <summary>
	/// Gets the level. Used for saving the game.
	/// </summary>
	/// <returns>The level.</returns>
	public int getLevel(){
		return level;
	}

	/// <summary>
	/// Gets the walking speed. Used for saving the game.
	/// </summary>
	/// <returns>The walking speed.</returns>
	public float getWalkingSpeed(){
		return walkingSpeed;
	}

	/// <summary>
	/// Sets the attack power. Used for loading a game.
	/// </summary>
	/// <param name="AP">A.</param>
    public void setAttackPower(int AP){
        this.attackPower = AP;
    }

	/// <summary>
	/// Sets the walking speed. Used for loading a game.
	/// </summary>
	/// <param name="wp">Wp.</param>
    public void setWalkingSpeed(float wp){
        this.walkingSpeed = wp;
    }

	/// <summary>
	/// Setmaxhealth the specified mh. Used for loading a game.
	/// </summary>
	/// <param name="mh">Mh.</param>
    public void setmaxhealth(int mh){
        this.maxHealth = mh;
    }

	/// <summary>
	/// Sets the level. Used for loading a game.
	/// </summary>
	/// <param name="level">Level.</param>
    public void setLevel(int level){
		this.level = level;
	}

	/// <summary>
	/// Gets the attack power.
	/// </summary>
	/// <returns>The attack power.</returns>
	public int getAttackPower() {
		return attackPower;
	}

	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	public Type getType() {
		return type;
	}

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
    public string getName(){
        return enemy.getName();
    }

	/// <summary>
	/// Gets the enemy object.
	/// </summary>
	/// <returns>The enemy.</returns>
	public Enemy getEnemy() {
		return enemy;
	}

	/// <summary>
	/// Sets the enemy in or out of range of the player.
	/// </summary>
	public void setWithinRange() {
		isWithinRange = !isWithinRange;
	}

	/// <summary>
	/// Gets if this enemy is within range of the player.
	/// </summary>
	/// <returns><c>true</c>, if within range was gotten, <c>false</c> otherwise.</returns>
    public bool getWithinRange(){
        return isWithinRange;
    }

	/// <summary>
	/// Used for animation.
	/// </summary>
	/// <returns>The animation.</returns>
	/// <param name="animation">Animation.</param>
    public IEnumerator attackAnimation(string animation){
        anim.SetBool(animation, true);
        yield return new WaitForSeconds(1);
        anim.SetBool(animation, false);
    }

	/// <summary>
	/// Attackes the player.
	/// </summary>
    public void attack(){
        if (!dead && !name.Equals("MeepMeepPrefab(Clone)")){
            if (name.Equals("DesertEaglePrefab(Clone)"))
            {
                GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
                bulletClone.GetComponent<Bullet>().dmg = attackPower;
                bulletClone.GetComponent<Bullet>().shotByPlayer = false;
                bulletClone.GetComponent<Bullet>().shotByEnemy = true;
                bulletClone.transform.Rotate(90, 0, 0);
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
                int nr = (Random.Range(0f, 1f) <= 0.5f) ? 1:3;      // sound
                Sound[nr].Play();                                   // sound
            }
            else if (name.Equals("FireFoxPrefab(Clone)")){
                PlayerAttributes.takeDamage(attackPower);
                CameraShaker.shakeCamera();
                StartCoroutine(die());
                EnemySpawner.enemiesDefeaten++;
                this.gameObject.GetComponent<Seeker>().StopAllCoroutines();
                this.gameObject.GetComponent<Seeker>().destroyed = true;
                MiniMapScript.enemies.Remove(this);

                health = 0;
                Sound[0].Play();                                    // sound
            }
            else if (name.Equals("PolarBearPrefab(Clone)")) { 
                anim.SetBool("attack", true);
                GameObject.Find("player").GetComponent<PlayerController>().bind(true);
                int nr = (Random.Range(0f, 1f) <= 0.5f) ? 1 : 3;    // sound
                Sound[nr].Play();                                   // sound
            }
			else if(name.Equals("HammerHeadPrefab(Clone)") || name.Equals("OilphantPrefab(Clone)")){
				PlayerAttributes.takeDamage(attackPower);
				CameraShaker.shakeCamera();
                StartCoroutine(attackAnimation("attack"));
                Sound[0].Play();                                    // sound
            } 
        }
	}

	/// <summary>
	/// Attacks the base.
	/// </summary>
    public void attackBase(){
        if (!dead && !this.gameObject.Equals("MeepMeepPrefab(Clone)")){
            if (this.gameObject.name.Equals("HammerHeadPrefab(Clone)")){
                GameObject.Find("Gate").GetComponent<BaseController>().baseHealth -= attackPower * 2;
            }
            if(this.gameObject.name.Equals("FireFoxPrefab(Clone)")){
                GameObject.Find("Gate").GetComponent<BaseController>().baseHealth -= attackPower;
                StartCoroutine(die());
                EnemySpawner.enemiesDefeaten++;
                this.gameObject.GetComponent<Seeker>().StopAllCoroutines();
                this.gameObject.GetComponent<Seeker>().destroyed = true;
                MiniMapScript.enemies.Remove(this);
                health = 0;
            }
            else {
                GameObject.Find("Gate").GetComponent<BaseController>().baseHealth -= attackPower;
            }
        }
    }

	/// <summary>
	/// Sets the poisoned boolean.
	/// </summary>
	/// <param name="poi">If set to <c>true</c> poi.</param>
	public void setPoisoned(bool poi){
		poisoned = poi;
	}

	/// <summary>
	/// Gets the poisoned.
	/// </summary>
	/// <returns><c>true</c>, if poisoned was gotten, <c>false</c> otherwise.</returns>
    public bool getPoisoned(){
        return poisoned;
    }

	/// <summary>
	/// unstuns the enemy.
	/// </summary>
	public void unStun(){
		walkingSpeed = enemy.getWalkingSpeed();
		attackPower = enemy.getAttackPower();
		stunned = false;
	}

	/// <summary>
	/// Stun the enemy true of false;
	/// </summary>
	/// <param name="st">If set to <c>true</c> st.</param>
    public void stun(bool st){
		if (st) {
			timeToUnStun = Time.time + stunTimeInterval;
			walkingSpeed = 0;
			attackPower = 0;
			stunned = true;
		}
	}

	/// <summary>
	/// Gets if the is stunned.
	/// </summary>
	/// <returns><c>true</c>, if stunned was gotten, <c>false</c> otherwise.</returns>
	public bool getStunned(){
		return stunned;
	}

	/// <summary>
	/// Sets the position. Used for loading a game.
	/// </summary>
	/// <param name="here">Here.</param>
	void setPosition(Vector3 here){
		position = here;
	}

	/// <summary>
	/// Gets the position. Used for saving a game.
	/// </summary>
	/// <returns>The position.</returns>
	public Vector3 getPosition(){
		return position;
	}

	/// <summary>
	/// Sets the rotation. Used by loading a game.
	/// </summary>
	/// <param name="rot">Rot.</param>
    public void setRotation(Quaternion rot){
        rotation = rot;
    }

	/// <summary>
	/// Gets the rotation. Used when saving a game.
	/// </summary>
	/// <returns>The rotation.</returns>
    public Quaternion getRotation(){
        return rotation;
    }

	/// <summary>
	/// Animation of dying and destroying the object.
	/// </summary>
	public IEnumerator die(){
        dead = true;
        walkingSpeed = 0;
		anim.SetBool ("dying", true);
        yield return new WaitForSeconds(1);
        PSpawner spawner = Camera.main.GetComponent<PSpawner>();
        if (Random.Range(0f, 2f) > Analytics.get_timeCTBase() / Time.time + 1f) {
            spawner.placeUnit(this.gameObject.transform.position);
        }
        healthBarClone.transform.position = new Vector3(-1000, -1000, 0);
        descriptionClone.transform.position = new Vector3(-1000, -1000, 0);
        if (this.gameObject.name.Equals("PolarBearPrefab(Clone)"))
        {
            GameObject.Find("player").GetComponent<PlayerController>().bind(false);
        }
		Camera.main.GetComponent<EnemySpawner> ().unbuffedEnemies.Remove (this);
        destroyed = true;
        Destroy (this.gameObject);
	}

	/// <summary>
	/// Stops walking to the base when it's in range and for the MeepMeep to buff other enemies.
	/// </summary>
	/// <param name="col">Col.</param>
    public void OnTriggerEnter(Collider col){
        if (col.gameObject.CompareTag("BASE")){
            baseWithinRange = true;
        }
		if (col.gameObject.CompareTag("Enemy") && this.gameObject.name.Equals("MeepMeepPrefab(Clone)") && !col.gameObject.name.Equals("MeepMeepPrefab(Clone)")){
            col.gameObject.GetComponent<EnemyController>().walkingSpeed *= 1.5f;
            otherEnemies.Remove(col.gameObject.GetComponent<EnemyController>());
            Camera.main.GetComponent<EnemySpawner>().unbuffedEnemies.Remove(col.gameObject.GetComponent<EnemyController>());
        }
    }

	/// <summary>
	/// Sets base out of range when it exits the trigger.
	/// </summary>
	/// <param name="col">Col.</param>
    public void OnTriggerExit(Collider col){
        if (col.gameObject.CompareTag("BASE")){
            baseWithinRange = false;
        }
    }

	/// <summary>
	/// Update the speed according to the shots that are hit. It gets higher the more shots are hit.
	/// </summary>
	public void updateSpeed(){
		updatedSpeed = walkingSpeed * (1f + (0.6f * ((float)((float)Analytics.getHitCount () [0] / ((float)Analytics.getShotsFired () + 1f)) - 0.5f)));
	}

	/// <summary>
	/// Sets the healthbar above the enemy every frame.
	/// </summary>
	public void updateHealthBarAndDescription(){
		healthBarClone.transform.position = Camera.main.WorldToScreenPoint (gameObject.transform.position) + new Vector3 (0, 20, 0);
		descriptionClone.transform.position = Camera.main.WorldToScreenPoint (gameObject.transform.position) + new Vector3 (0, 25, 0);
		healthBarClone.value = health;
	}

	/// <summary>
	/// Look at the base when pathfinder goes to the base.
	/// </summary>
	public void walkToBase(){
		if (!this.gameObject.name.Equals ("MeepMeepPrefab(Clone)")) {
			this.gameObject.transform.LookAt (GameObject.FindGameObjectWithTag ("BASE").transform.position);
		}
	}

	/// <summary>
	/// Looks and walks towards the player.
	/// </summary>
	public void walkToPlayer(){
		if (!name.Equals ("MeepMeepPrefab(Clone)")) {
			this.gameObject.transform.LookAt (GameObject.Find ("player").transform.position);
			if (!isWithinRange) {
				transform.position = Vector3.MoveTowards (transform.position, PlayerController.getPosition (), updatedSpeed * Time.deltaTime);
			}
		}
	}

	/// <summary>
	/// Used by MeepMeep, sorts the other enemies to distance.
	/// </summary>
	public void sortOtherEnemiesToDistance(){
		for (int i = 0; i < otherEnemies.Count; i++) {
			for (int j = 0; j < otherEnemies.Count - 1; j++) {
				if (Vector3.Distance (this.gameObject.transform.position, otherEnemies [j].gameObject.transform.position) > Vector3.Distance (this.gameObject.transform.position, otherEnemies [j + 1].gameObject.transform.position)) {
					EnemyController temp = otherEnemies [j];
					otherEnemies [j] = otherEnemies [j + 1];
					otherEnemies [j + 1] = temp;
				}
			}
		}
	}

	/// <summary>
	/// Play MeepMeep sound.
	/// </summary>
	public void PeemPeemSound(){
		if (SoundPeem == false) {                             // sound
			SoundPeem = true;
			StartCoroutine (SoundPeemPeem ());
		}
	}

	/// <summary>
	/// The IEnumerator of the MeepMeep sound.
	/// </summary>
	/// <returns>The peem peem.</returns>
    IEnumerator SoundPeemPeem()
    {
        Sound[0].Play();
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        SoundPeem = false;
        yield return null;
    }

	/// <summary>
	/// Wander around the map. Every 1 second it find a random position in a circle around and walks there.
	/// </summary>
    public void wanderAround(){
		if (currentWanderingDirection == new Vector3 (0f, 0f, 0f) || Time.time > timeToChangeDirection) {
			timeToChangeDirection += 1.0f;
			float ran = Random.Range (0f, 2f * Mathf.PI);
			currentWanderingDirection = this.gameObject.transform.position + new Vector3 (10f * Mathf.Sin (ran), 0, 10f * Mathf.Cos (ran));
		}
		gameObject.transform.LookAt (currentWanderingDirection);
		transform.position = Vector3.MoveTowards (transform.position, currentWanderingDirection, 1f * Time.deltaTime);
	}

	/// <summary>
	/// If the player enters the line of sight. The enemy hunts the player down.
	/// </summary>
	public void setPlayerWithinRange(){
		if (name.Equals("DesertEaglePrefab(Clone)") && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) < 10){
			isWithinRange = true;
			shotByPlayer = true;
			wanderingTime = -1;
			wandering = false;
			gameObject.GetComponent<Seeker> ().toBase = false;
			baseWithinRange = false;
		}
		if (name.Equals("DesertEaglePrefab(Clone)") && !wandering && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) > 14){
			isWithinRange = false;
		}
		if (!name.Equals("DesertEaglePrefab(Clone)") && !name.Equals("MeepMeepPrefab(Clone)") && Vector3.Distance(this.gameObject.transform.position, GameObject.Find("player").transform.position) < 10){
			shotByPlayer = true;
			wandering = false;
			wanderingTime = -1;
			gameObject.GetComponent<Seeker> ().toBase = false;
			baseWithinRange = false;
		}
		if (name.Equals("PolarBearPrefab(Clone)") && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) > 20){
			anim.SetBool("attack", false);
			GameObject.Find("player").GetComponent<PlayerController>().bind(false);
		}
	}
}