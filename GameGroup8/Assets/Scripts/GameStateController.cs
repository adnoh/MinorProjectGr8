using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{


    WorldBuilder worldbuilder;
    Grid grid;
    EnemySpawner enemyspawner;


    public GameObject player;
    public bool newgame;


    void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        worldbuilder = GetComponentInChildren<WorldBuilder>();
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
        GenerateMap();
        initializePathfindingGrid();
        LoadBase();
        LoadPlayer();
        LoadEnemies();
   }  



    void GenerateMap()
    {
        worldbuilder.StartWorldBuilder();
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
            MonsterCollection.outsideLoad();            
        }

    }

    void LoadMiniMap()
    {

    }

}
