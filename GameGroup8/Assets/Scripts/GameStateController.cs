using UnityEngine;
using System.Collections;



/// <summary>
/// This class manages the GameState
/// /// </summary>
public class GameStateController : MonoBehaviour
{
    Grid grid;
    EnemySpawner enemyspawner;
    WorldBuilderII worldBuilder;


    public GameObject player;
    public static bool newgame;


    /// <summary>
    /// Sets wether it's a new game or loaded game
    /// </summary>
    /// <param name="_newgame"></param>
    public static void setNewgame(bool _newgame)
    {
        newgame = _newgame;
    }  
    
    /// <summary>
    ///  Make needed references
    /// </summary>
    void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        worldBuilder = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
        enemyspawner = Camera.main.GetComponent<EnemySpawner>();
    }

    // Manages the order of loading 
    void Start()
    {        
		GenerateMap();
        LoadBase();        
        LoadPlayer();
        initializePathfindingGrid();
        LoadEnemies();
		LoadUnits ();
    }  


    // Generate or load Map
    void GenerateMap()
    {
		
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

    // initializes pathfinding grid
    void initializePathfindingGrid()
    {
        grid.CreateGrid();
    }


    // Loads the base position, turrets and even the position of the sun and moon
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


    // Load the enemies
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
            Camera.main.GetComponent<EnemySpawner>().savewave();
               
        }

    }



	void LoadUnits()
    {
		if (newgame)
        {
			Camera.main.GetComponent<PSpawner>().FirstLoad();
		}
	}
}