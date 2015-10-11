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
        int[,] map = null;
        bool possible = false;
        while (!possible)
        {
            map = getNewMap(Lines, Columns);
            for (int i = 0; i < Lines; i++)
            {
                if (i == 52)
                    ObstaclesPerLine = ObstaclesPerLine-2;

                bool[] occupied = new bool[Columns];
                for (int k = 0; k < occupied.Length; k++)
                {
                    occupied[k] = false;
                }
                for (int j = 0; j < ObstaclesPerLine; j++)
                {
                    int platformSelect;
                    if (i == 0)
                        platformSelect = 0;
                    else
                    {
                        if (i <=9)
                        {
                            platformSelect = RNG(0, 6);
                        }
                        else
                        {
                            if ( i <=32)
                            {
                                platformSelect = RNG(7, 11);
                            }
                            else
                            {
                                if( i <=42)
                                {
                                    platformSelect = RNG(12, 20);
                                }
                                else
                                {
                                    platformSelect = RNG(12, Obstacles.Count);
                                }
                            }
                        }
                    }
                    bool available = false;
                    int column = 0;
                    while (!available)
                    {
                        if (i <= 42)
                        {
                            column = RNG(0, Columns);
                        }
                        else
                        {
                            if (i <= 52)
                            {
                                column = RNG(1, Columns - 1);
                            }
                            else
                            {
                                column = RNG(2, Columns - 2);
                            }
                            
                        }
                        if (!occupied[column])
                            available = true;
                    }
                    occupied[column] = true;
                    map[i, column] = platformSelect;
                }
                possible = isMapPossible(map);
            }
        }
        RenderObstacles(map);
    }

    void RenderObstacles(int[,] map)
    {
        float limitXStart = 0;
        float limitXEnd = 0;
        float limitYStart = 0;
        float limitYEnd = 0;
        float width = 0;
        float height = 0;
        GameObject[] limits = GameObject.FindGameObjectsWithTag("Limit");
        foreach (GameObject limit in limits)
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
        width = limitXEnd - limitXStart;
        height = limitYEnd - limitYStart;
        float oneLine = (height / Lines);
        float oneColumn = (width / Columns);

        for (int i = 0; i < Lines; i++)
        {
            float y = i * oneLine + limitYStart + (oneLine / 2);
            for (int j = 0; j < Columns; j++)
            {
                if(map[i,j] != -1)
                {
                    float x = j * oneColumn + limitXStart + (oneColumn / 2);
                    Vector3 position = new Vector3(x, y);

                    //Si l'obstacle est un oiseau
                    if (Obstacles[map[i, j]].CompareTag("leftDirectionBird"))
                    {
                        position.x += Random.Range(30.0F, 75.0F);
                    }
                    Instantiate(Obstacles[map[i,j]], position, Quaternion.identity);
                }
            }
        }
    }

    bool isMapPossible(int[,] map)
    {
        int[,] visited = getNewMap(Lines, Columns);
        for (int i=0;i< Columns;i++)
        {
            if (isMapPossible(map, 0, i, visited))
                return true;
        }
        return false;
    }

    bool isMapPossible(int[,] map, int line, int column, int[,] visited)
    {
        bool possible = false;
        Point actual = new Point(line, column);
        if (isFinalPoint(actual))
            return true;
        if (isRealPoint(actual) && map[line,column] != -1 && visited[line,column] != 1)
        {
            visited[line, column] = 1;
            if (isMapPossible(map, line, column - 1, visited))
                possible = true;
            else if (isMapPossible(map, line, column + 1, visited))
                possible = true;
            else if (isMapPossible(map, line + 1, column - 1, visited))
                possible = true;
            else if (isMapPossible(map, line + 1, column, visited))
                possible = true;
            else if (isMapPossible(map, line + 1, column + 1, visited))
                possible = true;
        }
        return possible;
    }

    bool isRealPoint(Point actual)
    {
        if (actual.x >= 0 && actual.x < Lines && actual.y >= 0 && actual.y < Columns)
            return true;
        else
            return false;
    }

    bool isFinalPoint(Point actual)
    {
        if (actual.x == Lines-1)
            return true;
        else
            return false;
    }

    int[,] getNewMap(int lines, int columns)
    {
        int[,] map = new int[lines, columns];
        for(int i=0;i< Lines;i++)
        {
            for(int j=0;j<Columns;j++)
            {
                map[i, j] = -1;
            }
        }
        return map;
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

    class Point
    {
        public int x;
        public int y;
        public Point(int x, int y) { this.x = x; this.y = y; }
    }
}
