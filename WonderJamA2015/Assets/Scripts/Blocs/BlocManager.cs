using UnityEngine;
using System.Collections.Generic;

public class BlocManager : MonoBehaviour
{
    public List<Transform> Obstacles;
    public int Columns;
    public int Lines;
    public int ObstaclesPerLine;

    // Use this for initialization
    void Start ()
    {
        GenerateObstacles();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    int RNG(int min, int max)
    {
        int rand = Random.Range(min, max);
        return rand;
    }

    void GenerateObstacles()
    {
        float limitXStart = 0;
        float limitXEnd = 0;
        float limitYStart = 0;
        float limitYEnd = 0;
        float width = 0;
        float height = 0;
        GameObject[] limits = GameObject.FindGameObjectsWithTag("Limit");
        foreach(GameObject limit in limits)
        {
            Limit l = limit.GetComponent<Limit>();
            if (l.axis == Limit.Axis.x && l.type == Limit.Type.start)
                limitXStart = l.transform.position.x;
            if (l.axis == Limit.Axis.x && l.type == Limit.Type.end)
                limitXEnd = l.transform.position.x;
            if (l.axis == Limit.Axis.y && l.type == Limit.Type.start)
                limitYStart = l.transform.position.y;
            if (l.axis == Limit.Axis.y && l.type == Limit.Type.end)
                limitYEnd = l.transform.position.y;
        }
        //Debug.Log("X: " + limitXStart + "," + limitXEnd + " / Y: " + limitYStart + "," + limitYEnd);
        width = limitXEnd - limitXStart;
        height = limitYEnd - limitYStart;
        float oneLine = (height / Lines);
        float oneColumn = (width / Columns);

        for (int i=0;i< Lines;i++)
        {
            float y = i * oneLine + limitYStart + (oneLine / 2);
            bool[] occupied = new bool[Columns];
            for(int k=0;k<occupied.Length;k++)
            {
                occupied[k] = false;
            }
            for(int j=0;j<ObstaclesPerLine;j++)
            {
                int platformSelect = RNG(0, Obstacles.Count);
                bool available = false;
                int column = 0;
                while (!available)
                {
                    column = RNG(0, Columns);
                    if (!occupied[column])
                        available = true;
                }
                occupied[column] = true;

                float x = column * oneColumn + limitXStart + (oneColumn/2);
                Vector3 position = new Vector3(x, y);
                Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
                Debug.Log(column);
            }
        }
    }

    void GenerateTerrain()
    {
        //First part of the level

        int platformSelect;

        for (int i = 0; i < 5; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count - 3);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 0.6F, 0);
            Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Second Part of the level

        for (int i = 0; i < 5; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count - 4);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 4.0F, 0);
            Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Third Part of the level

        for (int i = 0; i < 4; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 9.0F, 0);
            Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }

        //Fourth Part of the level

        for (int i = 0; i < 4; i++)
        {
            platformSelect = Random.Range(0, Obstacles.Count);
            Vector3 position = new Vector3(Random.Range(-16.0F + (i * 6), -11.0F + (i * 6)), 14.0F, 0);
            Instantiate(Obstacles[platformSelect], position, Quaternion.identity);
        }
    }
}
