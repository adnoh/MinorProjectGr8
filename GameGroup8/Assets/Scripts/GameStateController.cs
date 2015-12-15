using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    Grid grid;
    EnemySpawner enemyspawner;
    WorldBuilderII worldBuilder;


    public GameObject player;
    public bool newgame;


    void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        worldBuilder = GameObject.FindGameObjectWithTag("Ground").GetComponent<WorldBuilderII>();
        enemyspawner = GetComponent<EnemySpawner>();
    }


    /*  1. Generate map
    2. Load base
    3. Generate pathfinding grid (only after map is finished)        
    4. Load playerposition
    5. Load enemies
    6. Load minimap
 */


    void Start()
    {
        Debug.Log(worldBuilder);
        GenerateMap();
        initializePathfindingGrid();
        // LoadBase();
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
            worldBuilder.FirstLoad();
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
    }

    void LoadPlayer()
    {
        if (newgame)
        {
            
        }
        else
        {
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
            MonsterCollection.MonsterLoad("assets/saves/monsters.xml");
                      
        }

    }

    void LoadMiniMap()
    {

    }

}
