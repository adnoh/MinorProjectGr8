﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class MapSaver {

    [XmlArray("list"), XmlArrayItem("list")]
    public float[][] TreeList;

    [XmlArray("list"), XmlArrayItem("list")]
    public float[][] HouseRotation;

    [XmlArray("list"), XmlArrayItem("list")]
    public float[][] HousePosition;

    [XmlArray("list"), XmlArrayItem("list")]
    public int[][] Tilemap;

    public MapSaver(WorldBuilderII WorldBuilder)
    {
        List<Vector3> TreePos = WorldBuilder.getTrees();
        List<Vector3[]> HouseInfo = WorldBuilder.getHouses();

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

        Tilemap = WorldBuilder.getMap();
    }

    public void MapLoader(WorldBuilderII WorldBuilder)
    {
        List<Vector3> TreePositions = new List<Vector3>();
        List<Vector3[]> HouseInformation = new List<Vector3[]>();

        foreach(float[] pos in TreeList)
        {
            Vector3 TreePosition = new Vector3(pos[0], pos[1], pos[2]);
            TreePositions.Add(TreePosition);
        }

        for(int i = 0; i < HousePosition.GetLength(0); i++)
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
    }
}