using UnityEngine;

public class SpawnMaze : MonoBehaviour
{
    public GameObject verticalWall;
    public GameObject horizontalWall;

    private float horizontalWallInitalXPosition = -3.5f;
    private float verticalWallInitalXPosition = -4.0f;

    private int horizontalchance;
    private int verticalchance;

    public int difficulty = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnGrid();

        Debug.Log(Random.Range(0,10));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void spawnGrid()
    {
        for (int xValue = 0; xValue <= 7; xValue++)
        {
            for (int yValue = -3; yValue <= 3; yValue++)
            {
                horizontalchance = Random.Range(0, difficulty);

                if (horizontalchance % 1 == 0)
                {
                    Instantiate(horizontalWall, new Vector3(horizontalWallInitalXPosition + xValue, yValue, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }


        for (int xValue = 0; xValue <= 8; xValue++)
        {
            for (float yValue = -2.5f; yValue <= 2.5; yValue++)
            {
                verticalchance = Random.Range(0, difficulty);
                if (verticalchance % 1 == 0)
                {
                    Instantiate(verticalWall, new Vector3(verticalWallInitalXPosition + xValue, yValue, 0), transform.rotation);
                }
            }
        }
    }
}

