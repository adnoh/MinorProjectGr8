using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class to store all the necessery information for the game.
/// This class is used for the analytics of the game, and for the AI of the game.
/// It only contains getters and setters and is not continuesly running when the game is, it only gets updated with an event.
/// </summary>
public class Analytics {

    private static int score = 0;
    private static int playerLevel = 0;//
    private static int timesDied = 0;//

    private static float timePlayed = 0;
    private static float timeOutside = 0;//
    private static float timeCloseToBase = 0;//                  
    private static float timeBase = 0;//

    private static int[] weapon = new int[8];//                  // index = weaponNr
    private static int[] shots_hit = new int[2];//               // [0] = hit, [1] = miss
    private static int shots_fired;
    private static int[] hit_enemyType = new int[3];//           // [0] = firefox, [1] = hamerhead, [2] = deserteagle
    private static int[] playerUpgrades = new int[4];//          // [0] = attack, [1] = speed, [2] = health, [3] = energy
    private static int[] building = new int[4];//                // [0] = Rock, [1] = C-a-P, [2] = Snail, [3] = Harp

    private static List<float[]> placeRIP = new List<float[]>();//   // list of places where the player died
    private static List<float[]> placeKill = new List<float[]>();//  // list of places whete enemies died
    	
	public static void setScore(int scores)
    {
        score = scores;
    }

    public static void setPlayerLevel()
    {
        playerLevel += 1;
    }

    public static void set_timesDied()
    {
        timesDied += 1;
    }

    public static void set_timePlayed()
    {
        timePlayed += Time.unscaledDeltaTime;
    }

    public static void set_timeOutside()
    {
        timeOutside += Time.unscaledDeltaTime;
    }

    public static void set_timeCTBase()
    {
        timeCloseToBase += Time.unscaledDeltaTime;
    }

    public static void set_timeBase()
    {
        timeBase += Time.unscaledDeltaTime;
    }

    public static void setWeapons(int weaponNr)
    {
        weapon[weaponNr] += 1;
    }

    public static void setHitCount(bool hit)
    {
        if (hit)
            shots_hit[0] += 1;
        else if (!hit)
            shots_hit[1] += 1;
    }

    public static void fireShot()
    {
        shots_fired++;
    }

    public static void setHitByEnemy(int Enemy)
    {
        switch (Enemy)
        {
            case 1:
                hit_enemyType[0] += 1;
                break;
            case 2:
                hit_enemyType[1] += 1;
                break;
            case 3:
                hit_enemyType[2] += 1;
                break;
        }
    }

    public static void setPlayerUpgrades(int upgrade)
    {
        switch (upgrade)
        {
            case 1:
                playerUpgrades[0] += 1;
                break;
            case 2:
                playerUpgrades[1] += 1;
                break;
            case 3:
                playerUpgrades[2] += 1;
                break;
            case 4:
                playerUpgrades[3] += 1;
                break;
        }
    }

    public static void setBuildings(int buildingNr)
    {
        switch (buildingNr)
        {
            case 1:
                building[0] += 1;
                break;
            case 2:
                building[1] += 1;
                break;
            case 3:
                building[2] += 1;
                break;
            case 4:
                building[3] += 1;
                break;
        }
    }

    public static void setPlaceDied(Vector3 pos)
    {
        float pos_x = pos.x;
        float pos_y = pos.y;
        float pos_z = pos.z;

        placeRIP.Add(new float[] { pos_x, pos_y, pos_z });
    }

    public static void setPlaceKill(Vector3 pos)
    {
        float pos_x = pos.x;
        float pos_y = pos.y;
        float pos_z = pos.z;

        placeKill.Add(new float[] { pos_x, pos_y, pos_z });
    }

    public static int getScore()
    {
        return score;
    }

    public static int getPlayerLevel()
    {
        return playerLevel;
    }

    public static int get_timesDied()
    {
        return timesDied;
    }

    public static float get_timePlayed()
    {
        return timePlayed;
    }

    public static float get_timeOutside()
    {
        return timeOutside;
    }

    public static float get_timeCTBase()
    {
        return timeCloseToBase;
    }

    public static float get_timeBase()
    {
        return timeBase;
    }

    public static int[] getWeapons()
    {
        return weapon;
    }

    public static int[] getHitCount()
    {
        return shots_hit;
    }

    public static int getShotsFired()
    {
        return shots_fired;
    }

    public static int[] getHitByEnemy()
    {
        return hit_enemyType;
    }

    public static int[] getPlayerUpgrades()
    {
        return playerUpgrades;
    }

    public static int[] getBuildings()
    {
        return building;
    }

    public static float[][] getPlaceDied()
    {
        float[][] Places = new float[placeRIP.Count][];
        for (int i = 0; i < placeRIP.Count; i++)
        {
            Places[i] = placeRIP[i];
        }
        return Places;
    }

    public static float[][] getPlaceKill()
    {
        float[][] Places = new float[placeKill.Count][];
        for (int i = 0; i < placeKill.Count; i++)
        {
            Places[i] = placeKill[i];
        }
        return Places;
    }
}
