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

	void Update () {
        if(otherEnemies.Count == 0)
        {
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

        if (name.Equals("MeepMeepPrefab(Clone)") && otherEnemies.Count > 0 && !wandering)
        {
			PeemPeemSound ();
			sortOtherEnemiesToDistance ();
            gameObject.transform.LookAt(otherEnemies[0].gameObject.transform);
            transform.position = Vector3.MoveTowards(transform.position, otherEnemies[0].gameObject.transform.position, updatedSpeed * Time.deltaTime);
        }
        else if(name.Equals("MeepMeepPrefab(Clone)") && otherEnemies.Count == 0)
        {
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

    // Getters and setters

	public int getHealth() {
		return health;
	}

	public int getMaxHealth() {
		return maxHealth;
	}

    public void setHealthFirstTime(int health){
        this.health = health;
    }

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

	public int getLevel(){
		return level;
	}

	public float getWalkingSpeed(){
		return walkingSpeed;
	}

    public void setAttackPower(int AP){
        this.attackPower = AP;
    }

    public void setWalkingSpeed(float wp){
        this.walkingSpeed = wp;
    }

    public void setmaxhealth(int mh){
        this.maxHealth = mh;
    }

    public void setLevel(int level){
		this.level = level;
	}

	public int getAttackPower() {
		return attackPower;
	}

	public Type getType() {
		return type;
	}

    public string getName(){
        return enemy.getName();
    }

	public Enemy getEnemy() {
		return enemy;
	}

	public void setWithinRange() {
		isWithinRange = !isWithinRange;
		nextAttack = 1.0f;
	}

    public bool getWithinRange(){
        return isWithinRange;
    }

    public IEnumerator attackAnimation(string animation){
        anim.SetBool(animation, true);
        yield return new WaitForSeconds(1);
        anim.SetBool(animation, false);
    }

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
                walkingSpeed = 0f;
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

    public void attackBase()
    {
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

	public void setPoisoned(bool poi){
		poisoned = poi;
	}

    public bool getPoisoned(){
        return poisoned;
    }

	public void unStun(){
		walkingSpeed = enemy.getWalkingSpeed();
		attackPower = enemy.getAttackPower();
		stunned = false;
	}

    public void stun(bool st){
		if (st) {
			timeToUnStun = Time.time + stunTimeInterval;
			walkingSpeed = 0;
			attackPower = 0;
			stunned = true;
		}
	}

	public bool getStunned(){
		return stunned;
	}

	void setPosition(Vector3 here){
		position = here;
	}
	
	public Vector3 getPosition(){
		return position;
	}

    public void setRotation(Quaternion rot){
        rotation = rot;
    }

    public Quaternion getRotation(){
        return rotation;
    }

	public IEnumerator die(){
        dead = true;
        walkingSpeed = 0;
		anim.SetBool ("dying", true);
        yield return new WaitForSeconds(1);
        PSpawner spawner = Camera.main.GetComponent<PSpawner>();
        if (Random.Range(0f, 1f) > Analytics.get_timeCTBase() / Time.time) {
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

    public void OnTriggerExit(Collider col){
        if (col.gameObject.CompareTag("BASE")){
            baseWithinRange = false;
        }
    }

    IEnumerator SoundPeemPeem(){
        Sound[0].Play();
        yield return new WaitForSeconds(Random.Range(5f, 15f));
        SoundPeem = false;
        yield return null;
    }

	public void updateSpeed(){
		updatedSpeed = walkingSpeed * (1f + (0.6f * ((float)((float)Analytics.getHitCount () [0] / ((float)Analytics.getShotsFired () + 1f)) - 0.5f)));
	}

	public void updateHealthBarAndDescription(){
		healthBarClone.transform.position = Camera.main.WorldToScreenPoint (gameObject.transform.position) + new Vector3 (0, 20, 0);
		descriptionClone.transform.position = Camera.main.WorldToScreenPoint (gameObject.transform.position) + new Vector3 (0, 25, 0);
		healthBarClone.value = health;
	}

	public void walkToBase(){
		if (!this.gameObject.name.Equals ("MeepMeepPrefab(Clone)")) {
			this.gameObject.transform.LookAt (GameObject.FindGameObjectWithTag ("BASE").transform.position);
		}
	}

	public void walkToPlayer(){
		if (!name.Equals ("MeepMeepPrefab(Clone)")) {
			this.gameObject.transform.LookAt (GameObject.Find ("player").transform.position);
			if (!isWithinRange) {
				transform.position = Vector3.MoveTowards (transform.position, PlayerController.getPosition (), updatedSpeed * Time.deltaTime);
			}
		}
	}

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

	public void PeemPeemSound(){
		if (SoundPeem == false) {                             // sound
			SoundPeem = true;
			StartCoroutine (SoundPeemPeem ());
		}
	}

	public void wanderAround(){
		if (currentWanderingDirection == new Vector3 (0f, 0f, 0f) || Time.time > timeToChangeDirection) {
			timeToChangeDirection += 1.0f;
			float ran = Random.Range (0f, 2f * Mathf.PI);
			currentWanderingDirection = this.gameObject.transform.position + new Vector3 (10f * Mathf.Sin (ran), 0, 10f * Mathf.Cos (ran));
		}
		gameObject.transform.LookAt (currentWanderingDirection);
		transform.position = Vector3.MoveTowards (transform.position, currentWanderingDirection, 1f * Time.deltaTime);
	}

	public void setPlayerWithinRange(){
		if (name.Equals("DesertEaglePrefab(Clone)") && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) < 12){
			isWithinRange = true;
			shotByPlayer = true;
			wanderingTime = -1;
			wandering = false;
			gameObject.GetComponent<Seeker> ().toBase = false;
			baseWithinRange = false;
		}
		if (name.Equals("DesertEaglePrefab(Clone)") && !wandering && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) > 20){
			isWithinRange = false;
		}
		if (!name.Equals("DesertEaglePrefab(Clone)") && !name.Equals("MeepMeepPrefab(Clone)") && Vector3.Distance(this.gameObject.transform.position, GameObject.Find("player").transform.position) < 10){
			shotByPlayer = true;
			wandering = false;
			wanderingTime = -1;
			gameObject.GetComponent<Seeker> ().toBase = false;
			baseWithinRange = false;
		}
		if (name.Equals("PolarBearPrefab(Clone)") && Vector3.Distance(gameObject.transform.position, GameObject.Find("player").transform.position) > 10){
			anim.SetBool("attack", false);
			GameObject.Find("player").GetComponent<PlayerController>().bind(false);
			walkingSpeed = enemy.getWalkingSpeed();
		}
	}
}