using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    Grid grid;
    EnemySpawner enemyspawner;
    WorldBuilderII worldBuilder;


    public GameObject player;
    public static bool newgame;
    // public bool newgame2;


    public static void setNewgame(bool _newgame)
    {
        newgame = _newgame;
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

		Debug.Log("Newgame: " + newgame);
        // setNewgame(newgame2);
        GenerateMap();
        LoadBase();        
        LoadPlayer();
        initializePathfindingGrid();
        LoadEnemies();
		LoadUnits ();
   }  



    void GenerateMap()
    {
		Debug.Log("Generating Map");

        if (newgame)
        {
            worldBuilder.FirstLoad();

        }
        else
        {
            // Map loading
            MonsterCollection.mapLoad(Application.dataPath + "/saves/world.xml");

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
            LoadBase_mMap();
        }
        else
        {   // load base  + turrets
            MonsterCollection.BaseLoad(Application.dataPath + "/saves/base.xml");
            GameObject.Find("Gate").GetComponent<BaseController>().buildFromSave();
            MonsterCollection.outsideLoad(Application.dataPath + "/saves/outside.xml");
            MonsterCollection.turretLoad(Application.dataPath + "/saves/turrets.xml");
            MonsterCollection.SunLoad(Application.dataPath + "/saves/sun.xml");
            MonsterCollection.MoonLoad(Application.dataPath + "/saves/moon.xml");

            LoadBase_mMap();
        }
    }

    // Place base in the minimap
    void LoadBase_mMap()
    {
        MiniMapScript mMap = Camera.main.GetComponent<MiniMapScript>();
        mMap.ShowBase_Mmap();
    }

    void LoadPlayer()
    {
        if (newgame)
        {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().FirstLoad();
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttacker>().FirstLoad();
        }
        else
        {
            // Load player position
            MonsterCollection.playerLoad(Application.dataPath + "/saves/Player.xml");
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
            MonsterCollection.MonsterLoad(Application.dataPath + "/saves/monsters.xml");
            Camera.main.GetComponent<EnemySpawner>().savewave(Application.dataPath + "/saves/monsters.xml");
        }
    }

	void LoadUnits(){
		if (newgame) {
			Camera.main.GetComponent<PSpawner> ().FirstLoad ();
		}
	}
}