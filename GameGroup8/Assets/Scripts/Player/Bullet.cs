using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Type type;
	public int dmg;
	public bool stun;
	public bool poisonous;
    public bool shotByPlayer;
    public bool shotByEnemy;

    Score score_;

    void Start(){

        if(this.gameObject.name.Equals("CatPrefab(Clone)") && this.gameObject.name.Equals("SnailPrefab(Clone)"))
        {
            shotByPlayer = false;
        }

        score_ = Camera.main.GetComponent<Score>();

        if (this.gameObject.CompareTag ("No type")) {
			type = new Type (0);
		} else if (this.gameObject.CompareTag ("Wind")) {
			type = new Type (1);
		} else if (this.gameObject.CompareTag ("Earth")) {
			type = new Type (2);
		} else {
			type = new Type (3);
		}
	}

    /// <summary>
    /// If the bullet hits an enemy (col) it does damage to it and destroys the bullet
    /// </summary>
    /// <param name="col"></param>
	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag ("Enemy") && !shotByEnemy && (this.gameObject.name.Equals("newBullet(Clone)") || this.gameObject.name.Equals ("CatPrefab(Clone)") || this.gameObject.name.Equals("SnailPrefab(Clone)"))){
			EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
            enemyController.shotByPlayer = true;
			int damage = (int)(Random.Range (dmg, dmg + 10) * type.damageMultiplierToType(enemyController.getType()) * PlayerAttributes.getAttackMultiplier());
			enemyController.setHealth(enemyController.getHealth () - damage);
			if(poisonous){
				enemyController.setPoisoned(true);
			}
			if(stun){
				enemyController.stun (true);
			}
			PlayerAttacker.lastAttackedEnemy = enemyController;
			if(enemyController.getHealth () <= 0){
				EnemySpawner.enemiesDefeaten++;
                if (!enemyController.getName().Equals("MeepMeep"))
                {
                    col.gameObject.GetComponent<Seeker>().StopAllCoroutines();
                    col.gameObject.GetComponent<Seeker>().destroyed = true;
                }
                score_.addScoreEnemy(enemyController.getLevel());
                enemyController.destroyed = true;
                if (!enemyController.dead){
                    PlayerAttributes.getExperience(enemyController.getLevel());
                }
                enemyController.StartCoroutine (enemyController.die());                
                PlayerAttacker.lastAttackedEnemy = null;
				MiniMapScript.enemies.Remove(enemyController);
                Analytics.setPlaceKill(col.gameObject.transform.position);
                if (col.gameObject.name == "FireFoxPrefab(Clone)")
                    Analytics.setHitByEnemy(0);
                if (col.gameObject.name == "HammerHeadPrefab(Clone)")
                    Analytics.setHitByEnemy(1);
                if (col.gameObject.name == "DesertEaglePrefab(Clone)")
                    Analytics.setHitByEnemy(2);
            }

            if (shotByPlayer)
            {
                Analytics.setHitCount(true);
            }

			GameObject.Destroy (gameObject);
		}
        if (col.gameObject.name.Equals("player") && shotByEnemy)
        {
            PlayerAttributes.takeDamage(dmg);
            GameObject.Destroy(this.gameObject);
            CameraShaker.shakeCamera();
        }

	}
 
}
