using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManagerMultiplayer : NetworkBehaviour
{
    [Header("Positions & Objectss")]
    float randNum = 0.0f;
    float offScreen = 16.0f;
    Vector2 offScreenPos;
    public playerScript multiPlayer;
    public GameObject Platform;
    public GameObject Wall;
    public GameObject Enemy;

    public int platformSpawnNum = 0;
    public int wallSpawnNum = 0;
    public int enemySpawnNum = 0;

    [Header("Multiplayer Vars")]
    [SyncVar]
    public Vector3 syncPosition;
 

    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform();
        offScreenPos = new Vector2(offScreen, 3.5f);

        multiPlayer = FindObjectOfType<playerScript>();
    }

    void Update()
    {
        /*if (multiPlayer.isPlayerAlive == false)
        {
            Debug.Log("Multiplayer player Dead");
            restartGame();
        }*/

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platformSpawnNum++;
        //wallSpawnNum++;
       // enemySpawnNum++;

        if (platformSpawnNum >= 200)
        {
            platformSpawnNum = 0;
            spawnPlatform();
        }

        //if (wallSpawnNum >= 90)
        //{
        //    wallSpawnNum = 0;
        //    spawnWall();
        //}

        //if (enemySpawnNum >= 125)
        //{
        //    enemySpawnNum = 0;
        //    spawnEnemy();
        //}
    }

    
    void spawnPlatform()
    {
        // -3 to 5 is the amount of leeway for Y value
        randNum = Random.Range(-3, 5);
        Vector3 pos = new Vector3(offScreen, randNum, 0);

        Instantiate(Platform, pos, Quaternion.identity);

        NetworkServer.Spawn(Platform);
    }

    //void spawnWall()
    //{
    //    Instantiate(Wall, offScreenPos, Quaternion.identity);
    //    NetworkServer.Spawn(Wall);
    //}

    //[Command]
    //void spawnEnemy()
    //{
    //    Instantiate(Enemy, offScreenPos, Quaternion.identity);
    //}

    void restartGame()
    {
        Debug.Log("Game Restarted");

        multiPlayer.isPlayerAlive = true;

        platformSpawnNum = 0;
        wallSpawnNum = 0;
        enemySpawnNum = 0;

        var platforms = GameObject.FindGameObjectsWithTag("Platform"); // change this to ground for a differnt game lmao // keep as platform for normal game.
        var walls = GameObject.FindGameObjectsWithTag("wall");
        var enemies = GameObject.FindGameObjectsWithTag("AI");


        foreach (var platform in platforms)
        {
            Destroy(platform);
        }

        foreach (var wall in walls)
        {
            Destroy(wall);
        }


        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
