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
                bool[] occupied = new bool[Columns];
                for (int k = 0; k < occupied.Length; k++)
                {
                    occupied[k] = false;
                }
                for (int j = 0; j < ObstaclesPerLine; j++)
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
                    Instantiate(Obstacles[map[i,j]], position, Quaternion.identity);
                }
            }
        }
    }

    bool isMapPossible(int[,] map)
    {
        List<Point> visited = new List<Point>();
        for (int i=0;i< Columns;i++)
        {
            if (isMapPossible(map, 0, i, visited))
                return true;
        }
        return false;
    }

    bool isMapPossible(int[,] map, int line, int column, List<Point> visited)
    {
        bool possible = false;
        Point actual = new Point(line, column);
        if (isFinalPoint(actual))
            return true;
        if (isRealPoint(actual) && map[line,column] != -1 && !isVisited(actual,visited))
        {
            visited.Add(actual);
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
            visited.Remove(actual);
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

    bool isVisited(Point actual, List<Point> visited)
    {
        foreach (Point point in visited)
        {
            if (actual.x == point.x && actual.y == point.y)
                return true;
        }
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
