using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttacker : MonoBehaviour {

	public GameObject enemyDescription;
	public bool showEnemyDescription;

	public Text enemyDescriptionText;
	public Text enemyHealthBar;
	public Text enemyWeaponDamageText;

	public GameObject bullet;
	public float bulletSpeed = 100f;
		
	void Start () {
		showEnemyDescription = false;
		enemyDescription.SetActive (false);

	}

	void Update () {
		enemyDescription.SetActive (showEnemyDescription);
        bool Base = Controls.getPause();

        if (!Base)
        {
            if (Input.GetMouseButtonDown(0))
            {

                GameObject shot = GameObject.Instantiate(bullet, transform.position + (transform.forward), transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            }


            if (Input.GetMouseButtonDown(2) && false)
            {

                RaycastHit hit;
                Ray bulletray = new Ray(transform.position, Vector3.forward);
                if (Physics.Raycast(bulletray, out hit))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        EnemyController enemyController = hit.collider.gameObject.GetComponent<EnemyController>();
                        enemyController.setHealth(enemyController.getHealth());


                        if (enemyController.getHealth() <= 0)
                        {
                            EnemySpawner.enemiesDefeaten++;
                            Destroy(hit.collider.gameObject);
                            showEnemyDescription = false;
                        }
                    }
                }
            }
        }
		
	}

	public void setEnemyDescription(EnemyController enemyController){
		enemyDescriptionText.text = "Level = " + enemyController.getLevel ();
		enemyHealthBar.text = "Health = " + enemyController.getHealth ();
		enemyWeaponDamageText.text = "Weapon Damage = " + enemyController.getAttackPower ();
		showEnemyDescription = true;
	}

	public void setEnemyDescriptionOff(){
		showEnemyDescription = false;
	}

	public void setEnemyDescriptionOn(){
		showEnemyDescription = true;
	}


}
