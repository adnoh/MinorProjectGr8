using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    Grid grid;
    EnemySpawner enemyspawner;
    WorldBuilderII worldBuilder;


    public GameObject player;
    public bool newgame;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Save player position
            MonsterCollection.playerSave("Assets/saves/Player.xml");

            // Save enemies
            MonsterCollection.MonsterSave("Assets/saves/monsters.xml");

            // Save outside variables + base
            MonsterCollection.outsideSave("Assets/saves/outside.xml");
            MonsterCollection.turretSave("Assets/saves/turrets.xml");
            MonsterCollection.BaseSave("Assets/saves/base.xml");

            // Save World (if you want to save the world, go aghead, be a hero)
            MonsterCollection.MapSave("Assets/saves/world.xml");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Load player position
            MonsterCollection.playerLoad();

            // Load enemies including enemies health
            EnemySpawner enemySpawner = Camera.main.GetComponent<EnemySpawner>();
            enemySpawner.savewave();


            // Base loading
            MonsterCollection.BaseLoad("Assets/saves/base.xml");
            GameObject.Find("Gate").GetComponent<BaseController>().buildFromSave();
            MonsterCollection.outsideLoad("Assets/saves/outside.xml");
            MonsterCollection.turretLoad("Assets/saves/turrets.xml");           
            
            // Map loading
            MonsterCollection.mapLoad("Assets/saves/world.xml");
            worldBuilder.ReplaceAssets();
            worldBuilder.BuildTexture();


        }

    }



    void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        worldBuilder = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
        enemyspawner = Camera.main.GetComponent<EnemySpawner>();
    }






    /*  1. Generate map
    2. Load base
    3. Generate pathfinding grid (only after map is finished)        
    4. Load playerposition
    5. Load enemies    
 */


    void Start()
    {
        Debug.Log(worldBuilder);
        GenerateMap();
        initializePathfindingGrid();
        LoadBase();        
        LoadPlayer();
        LoadEnemies();
   }  



    void GenerateMap()
    {

        if (newgame)
        {
            worldBuilder.FirstLoad();
        }
        else
        {
            // Map loading
            MonsterCollection.mapLoad("Assets/saves/world.xml");

            worldBuilder.SecondLoad();
        }



    }

    void initializePathfindingGrid()
    {
        grid.CreateGrid();
    }

    void LoadBase()
    {
        if (newgame)
        {
            
        }
        else
        {   // load base  + turrets
            MonsterCollection.BaseLoad("Assets/saves/base.xml");
            GameObject.Find("Gate").GetComponent<BaseController>().buildFromSave();
            MonsterCollection.outsideLoad("Assets/saves/outside.xml");
            MonsterCollection.turretLoad("Assets/saves/turrets.xml");
            
        }
    }

    void LoadPlayer()
    {
        if (newgame)
        {
            
        }
        else
        {
            // Load player position
            MonsterCollection.playerLoad();
        }

    }

    void LoadEnemies()
    { 
        if (newgame)
        {
            enemyspawner.FirstLoad();
        }


        else
        {
            // Load enemies
            MonsterCollection.MonsterLoad("assets/saves/monsters.xml");
            enemyspawner.savewave();
               
        }

    }
}
