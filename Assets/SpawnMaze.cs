using UnityEngine;

public class SpawnMaze : MonoBehaviour
{
    public GameObject verticalWall;
    public GameObject horizontalWall;

    public GameObject paver;

    private float horizontalWallInitalXPosition = -3.5f;
    private float verticalWallInitalXPosition = -4.0f;

    private int horizontalchance;
    private int verticalchance;

    public int difficulty = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnGrid();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    
    void SpawnGrid()
    {
        for (int xValue = 0; xValue <= 7; xValue++)
        {
            for (int yValue = -3; yValue <= 3; yValue++)
            {
                horizontalchance = Random.Range(0, 100);

                if (horizontalchance % difficulty == 0)
                {
                    Instantiate(horizontalWall, new Vector3(horizontalWallInitalXPosition + xValue, yValue, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }

        for (int xValue = 0; xValue <= 8; xValue++)
        {
            for (float yValue = -2.5f; yValue <= 2.5; yValue++)
            {
                verticalchance = Random.Range(0, 100);

                if (verticalchance % difficulty == 0)
                {
                    Instantiate(verticalWall, new Vector3(verticalWallInitalXPosition + xValue, yValue, 0), transform.rotation);
                }
            }
        }
    }
}

