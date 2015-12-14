using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{


    WorldBuilder worldbuilder;
    Grid grid;


    void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        worldbuilder = GetComponentInChildren<WorldBuilder>();
    }

    void Start()
    {
        GenerateMap();
        initializePathfindingGrid();
    }


    //  

    /*  1. Generate map
        2. Load base
        3. Generate pathfinding grid (only after map is finished)        
        4. Load player
        5. Load enemies
        6. Load minimap
     */


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

    }

    void LoadPlayer()
    {

    }

    void Enemies()
    {

    }

}
