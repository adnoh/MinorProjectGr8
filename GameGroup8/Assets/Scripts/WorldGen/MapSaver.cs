using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Class for saving the map of the game
/// </summary>
public class MapSaver {

    [XmlArray("treelist"), XmlArrayItem("treeposition")]
    public float[][] TreeList;

    [XmlArray("HouseRotationlist"), XmlArrayItem("HouseRotation")]
    public float[][] HouseRotation;

    [XmlArray("HousePositionlist"), XmlArrayItem("HousePosition")]
    public float[][] HousePosition;

    [XmlArray("WallsPositionlist"), XmlArrayItem("WallsPosition")]
    public float[][] WallsPosition;

    [XmlArray("Tilemaplist"), XmlArrayItem("Tilemap")]
    public int[][] Tilemap;

    public int nr_cHouses;

    /// <summary>
    /// Used when loading from xml-files
    /// </summary>
    public MapSaver()
    {
        //Because of things
    }

    /// <summary>
    /// Stores all the important information in this class so that it can be serialized with xml
    /// </summary>
    /// <param name="WorldBuilder"></param>
    public MapSaver(WorldBuilderII WorldBuilder)
    {
        List<Vector3> TreePos = WorldBuilder.getTrees();
        List<Vector3[]> HouseInfo = WorldBuilder.getHouses();
        List<Vector3> WallPos = WorldBuilder.getWalls();

        TreeList = new float[TreePos.Count][];
        for(int i = 0; i < TreePos.Count; i++)
        { 
            Vector3 Pos = TreePos[i];
            float pos_x = Pos.x;
            float pos_y = Pos.y;
            float pos_z = Pos.z;

            TreeList[i] = new float[3] { pos_x, pos_y, pos_z };
        }

        HousePosition = new float[HouseInfo.Count][];
        HouseRotation = new float[HouseInfo.Count][];
        for(int i = 0; i < HouseInfo.Count; i++)
        {
            Vector3 Pos = HouseInfo[i][0];
            Vector3 Rot = HouseInfo[i][1];

            float pos_x = Pos.x;
            float pos_y = Pos.y;
            float pos_z = Pos.z;

            float rot_x = Rot.x;
            float rot_y = Rot.y;
            float rot_z = Rot.z;

            HousePosition[i] = new float[3] { pos_x, pos_y, pos_z };
            HouseRotation[i] = new float[3] { rot_x, rot_y, rot_z };
        }
        nr_cHouses = WorldBuilder.get_nrHotels();

        WallsPosition = new float[WallPos.Count][];
        for (int i = 0; i < WallPos.Count; i++)
        {
            Vector3 Pos = WallPos[i];

            float pos_x = Pos.x;
            float pos_y = Pos.y;
            float pos_z = Pos.z;

            WallsPosition[i] = new float[3] { pos_x, pos_y, pos_z };
        }

        Tilemap = WorldBuilder.getMap();
    }

    /// <summary>
    /// Sets all the saved information from xml-files to the data in the WorldBuilder class
    /// </summary>
    /// <param name="WorldBuilder"></param>
    public void MapLoader(WorldBuilderII WorldBuilder)
    {
        List<Vector3> TreePositions = new List<Vector3>();
        List<Vector3[]> HouseInformation = new List<Vector3[]>();
        List<Vector3> WallsPositions = new List<Vector3>();

        foreach (float[] pos in TreeList)
        {
            Vector3 TreePosition = new Vector3(pos[0], pos[1], pos[2]);
            TreePositions.Add(TreePosition);
        }

        foreach (float[] pos in WallsPosition)
        {
            Vector3 WallPosition = new Vector3(pos[0], pos[1], pos[2]);
            WallsPositions.Add(WallPosition);
        }

        for (int i = 0; i < HousePosition.GetLength(0); i++)
        {
            float[] pos = HousePosition[i];
            float[] rot = HouseRotation[i];

            Vector3 Position = new Vector3(pos[0], pos[1], pos[2]);
            Vector3 Rotation = new Vector3(rot[0], rot[1], rot[2]);

            HouseInformation.Add(new Vector3[2] { Position, Rotation });
        }

        WorldBuilder.setHouses(HouseInformation);
        WorldBuilder.setTrees(TreePositions);
        WorldBuilder.setMap(Tilemap);
        WorldBuilder.set_nrHotels(nr_cHouses);
        WorldBuilder.setWalls(WallsPositions);
    }
}
