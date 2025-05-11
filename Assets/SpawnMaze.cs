using UnityEngine;

public class SpawnMaze : MonoBehaviour
{
    public GameObject verticalWall;
    public GameObject horizontalWall;

    private float horizontalWallInitalXPosition = -3.5f;
    private float verticalWallInitalXPosition = -4.0f;

    private int numberOfSquares = 48;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnGrid()
    {
        /*float[,] midpoint = { {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},
                      {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},
                      {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},
                      {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},
                      {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},
                      {0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0}};*/

        for (int xValue = 0; xValue <= 7; xValue++)
        {
            for (int yValue = -3; yValue <= 3; yValue++)
            {
                Instantiate(horizontalWall, new Vector3(horizontalWallInitalXPosition + xValue, yValue, 0), Quaternion.Euler(0, 0, 90));
            }
        }


        for (int xValue = 0; xValue <= 8; xValue++)
        {
            for (float yValue = -2.5f; yValue <= 2.5; yValue++)
            {
                Instantiate(verticalWall, new Vector3(verticalWallInitalXPosition + xValue, yValue, 0), transform.rotation);
                Debug.Log((verticalWallInitalXPosition + xValue) / 2 + "," + yValue / 2);
            }
        }
    }
}

