using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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
	public Type type;

    public Vector3 position;
    public Quaternion rotation;

    private bool isWithinRange;
    private bool baseWithinRange;

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
    public int offset = 2000;

    public GameObject bullet;

    void Start()
    {
        level = this.gameObject.GetComponent<EnemyController>().getLevel();
        if (this.gameObject.transform.name.Equals("HammerHeadPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("Hammerhead", level);
        }
        else if (this.gameObject.transform.name.Equals("DesertEaglePrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("DesertEagle", level);
        }
        else if (this.gameObject.transform.name.Equals("FireFoxPrefab(Clone)"))
        {
            enemy = enemyFactory.getEnemy("FireFox", level);
        }

        level = enemy.getLevel();
        maxHealth = enemy.getMaxHealth();
        health = enemy.getMaxHealth();
        attackPower = enemy.getAttackPower();
        walkingSpeed = enemy.getWalkingSpeed();
        type = enemy.getType();

        isWithinRange = false;
        MiniMapScript.enemies.Add(this);
        anim = GetComponent<Animator>();
        healthBarClone = Instantiate(healthBar);
        healthBarClone.transform.SetParent(Camera.main.GetComponent<EnemySpawner>().EnemyHealthBars.transform, false);
        healthBarClone.maxValue = health;
        descriptionClone = Instantiate(description);
        descriptionClone.text = "Lvl:" + enemy.getLevel() + " . " + enemy.getType().toString();
        descriptionClone.transform.SetParent(Camera.main.GetComponent<EnemySpawner>().EnemyHealthBars.transform, false);
    }

	void Update () {
        healthBarClone.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0, 20, 0);
        descriptionClone.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0, 25, 0);
        healthBarClone.value = health;
        if (!shotByPlayer)
        {
            this.gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("BASE").transform.position);
        }
        else
        {
            this.gameObject.GetComponent<Seeker>().toBase = false;
            this.gameObject.GetComponent<Seeker>().StopAllCoroutines();
            this.gameObject.transform.LookAt(GameObject.Find("player").transform.position);
            if (!isWithinRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerController.getPosition(), walkingSpeed * Time.deltaTime);
            }
        }
		if (Time.time > nextAttack) {
            if (isWithinRange)
            {
                nextAttack = Time.time + attackRate;
                StartCoroutine(attack());
            }
            else if (baseWithinRange)
            {
                nextAttack = Time.time + attackRate;
                StartCoroutine(attackBase());
            }
		}
		if (poisoned && Time.time > timeToGetPoisonDamage) {
			timeToGetPoisonDamage = Time.time + intervalToGetPoisonDamage;
			health -= (int)(maxHealth / 20);
		}
		if (stunned && Time.time > timeToUnStun) {
			unStun ();
		}

		Vector3 EnemyPosition = this.gameObject.transform.position;
		setPosition (EnemyPosition);
        Quaternion EnemyRotation = this.gameObject.transform.rotation;
        setRotation(EnemyRotation);
    }

    // Getters and setters

	public int getHealth() {
		return health;
	}

	public int getMaxHealth() {
		return maxHealth;
	}

	public void setHealth(int health) {
		this.health = health;
	}

	public int getLevel(){
		return level;
	}

	public float getWalkingSpeed(){
		return walkingSpeed;
	}

    public void setAttackPower(int AP)
    {
        this.attackPower = AP;
    }

    public void setWalkingSpeed(float wp)
    {
        this.walkingSpeed = wp;
    }

    public void setmaxhealth(int mh)
    {
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

	public Enemy getEnemy() {
		return enemy;
	}

	public void setWithinRange() {
		isWithinRange = !isWithinRange;
		nextAttack = 1.0f;
		if (isWithinRange) {
			PlayerAttacker.lastAttackedEnemy = this; 
		} else {
			PlayerAttacker.lastAttackedEnemy = null;
		}
	}

    public bool getWithinRange()
    {
        return isWithinRange;
    }

    public IEnumerator attack(){
        if (!dead){
            if (this.gameObject.name.Equals("DesertEaglePrefab(Clone)"))
            {
                GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
                bulletClone.GetComponent<Bullet>().dmg = attackPower;
                bulletClone.GetComponent<Bullet>().shotByPlayer = false;
                bulletClone.GetComponent<Bullet>().shotByEnemy = true;
                bulletClone.transform.Rotate(90, 0, 0);
                bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
            }
            if (this.gameObject.name.Equals("FireFoxPrefab(Clone)"))
            {
                PlayerAttributes.takeDamage(attackPower);
                CameraShaker.shakeCamera();
                StartCoroutine(die());
                EnemySpawner.enemiesDefeaten++;
                this.gameObject.GetComponent<Seeker>().StopAllCoroutines();
                this.gameObject.GetComponent<Seeker>().destroyed = true;
                MiniMapScript.enemies.Remove(this);
                health = 0;
            }
            else {
                PlayerAttributes.takeDamage(attackPower);
                CameraShaker.shakeCamera();
            }
            if (enemy.getType().getType() == 2)
            {
                anim.SetBool("attack", true);
                yield return new WaitForSeconds(1);
                anim.SetBool("attack", false);
            }
        }
	}

    public IEnumerator attackBase()
    {
        if (!dead)
        {
            if (this.gameObject.name.Equals("HammerHeadPrefab(Clone)"))
            {
                GameObject.Find("Gate").GetComponent<BaseController>().baseHealth -= attackPower * 2;
            }
            if(this.gameObject.name.Equals("FireFoxPrefab(Clone)"))
            {
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
            if (enemy.getType().getType() == 2)
            {
                anim.SetBool("attack", true);
                yield return new WaitForSeconds(1);
                anim.SetBool("attack", false);
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
        spawner.placeUnit(this.gameObject.transform.position);
        healthBarClone.transform.position = new Vector3(-1000, -1000, 0);
        descriptionClone.transform.position = new Vector3(-1000, -1000, 0);
        destroyed = true;
        Destroy (this.gameObject);
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("BASE"))
        {
            baseWithinRange = true;
        }
        if(this.gameObject.name.Equals("DesertEaglePrefab(Clone)") && col.gameObject.name.Equals("player")){
            isWithinRange = true;
            shotByPlayer = true;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("BASE"))
        {
            baseWithinRange = false;
        }
    }
}
