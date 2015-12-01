using UnityEngine;
using System.Collections;

public class Cat_Projectile : MonoBehaviour {

    private Type type;

    void Start()
    {
        type = new Type(2);
    }

    void Update()
    {
        if (gameObject.name.Equals("Cat!(Clone)") && gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0f, 0f, 0f))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy") && gameObject.name.Equals("Cat!(Clone)"))
        {
            EnemyController enemyController = col.gameObject.GetComponent<EnemyController>();
            int damage = (int)(Random.Range(40, 60) * type.damageMultiplierToType(enemyController.getType()));
            enemyController.setHealth(enemyController.getHealth() - damage);
            PlayerAttacker.lastAttackedEnemy = enemyController;
            if (enemyController.getHealth() <= 0)
            {
                EnemySpawner.enemiesDefeaten++;
                Destroy(col.gameObject);
                PlayerAttacker.lastAttackedEnemy = null;
				MiniMapScript.enemies.Remove(enemyController);
            }
        }
        if (col.gameObject.CompareTag("Enemy") && gameObject.name.Equals("Cat!(Clone)"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (gameObject.name.Equals("Cat!(Clone)"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
