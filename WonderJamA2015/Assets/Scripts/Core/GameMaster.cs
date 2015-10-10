using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*<summary>
 * Game's central entity. Is a singleton.
</summary>*/

public class GameMaster : MonoBehaviour
{
    public List<Transform> Obstacles;

    private static GameMaster _instance;

    public static GameMaster instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameMaster>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        GenerateTerrain();
        
    }

    void GenerateTerrain()
    {
        //First part of the level

        int platformSelect;

        for (int i = 0; i < 5; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count-3);
            Vector3 position = new Vector3(Random.Range(-16.0F+(i*6), -11.0F+(i*6)), 0.6F, 0);
            Transform Obstacle = (Transform)Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Second Part of the level

        for (int i = 0; i < 5; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count-4);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 4.0F, 0);
            Transform Obstacle = (Transform)Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Third Part of the level

        for (int i = 0; i < 4; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 9.0F, 0);
            Transform Obstacle = (Transform)Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Fourth Part of the level

        for (int i = 0; i < 4; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 14.0F, 0);
            Transform Obstacle = (Transform)Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        } 
    }

    public void OnGUI()
    {

    }
}

