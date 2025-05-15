/*using UnityEngine;
using UnityEngine.SceneManagement;

public class RBSpawnMaze : MonoBehaviour
{
    public GameObject square;
    //12 x 8

    private string point;
    private GameObject block;

    private SpriteRenderer spriteR;

    private string newPoint;

    private int squaresCovered = 0;
    private int direction = 0;
    //private string newPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       for (int i = 0; i <= 11; i++)
       {
            for (int j = 0; j <= 7; j++)
            {
                var newObject = Instantiate(square, new Vector3(0.85f + i, -0.34f - j, 0), transform.rotation);
                newObject.name = i + "," + j;
            }
       }

        string startingPoint = (Random.Range(0, 12) + "," + Random.Range(0, 8)).ToString();

        getFilling(startingPoint);
        ChangeColorRed(startingPoint);

        while(direction == 0)
        {
            direction = Random.Range(-2, 3);
        }

        Debug.Log(direction);
        RemoveWall(startingPoint, direction);

        newPoint = startingPoint;
        while (squaresCovered != 48)
        {
            direction = Random.Range(-2, 3);

            string nextPoint = RemoveWall(newPoint, direction);
            if (nextPoint != "")
            {
                ChangeColorRed(nextPoint);
                newPoint = nextPoint;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ChangeColorRed(string point)
    {
        getFilling(point);
        spriteR.color = Color.red;
        squaresCovered++;
    }

    Color findColor(string point)
    {
        getFilling(point);
        return spriteR.color;
    }

    bool isColored(string point)
    {
        Color color = findColor(point);
        Color red = new Color(1, 0, 0, 1);

        if (color == red)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void getFilling(string point)
    {
        block = GameObject.Find(point);

        if (block != null)
        {
            GameObject childObj = block.transform.Find("Filling").gameObject;

            spriteR = childObj.GetComponent<SpriteRenderer>();
        }
    }

    string RemoveWall(string point, int wallNo)
    {
        block = GameObject.Find(point);

        // 1 = Left Wall
        // -1 = Right Wall
        // 2 = Top Wall
        // -2 = Bottom Wall

        string[] coords = point.Split(',');
        int x = int.Parse(coords[0]);
        int y = int.Parse(coords[1]);

        char[] pointArray = point.ToCharArray();

        if (wallNo == 1 && x > 0)
        {
            GameObject childObj = block.transform.Find("Left Wall").gameObject;
            Destroy(childObj);

            newPoint = (x - 1) + "," + y; ;

            if (isColored(newPoint) == false)
            {
                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Right Wall").gameObject;
                Destroy(childObj);
                return newPoint;
            }
            else
            {
                wallNo = wallNo * -1;
                return "";
            }
        }
        else if (wallNo == -1 && x < 11)
        {
            GameObject childObj = block.transform.Find("Right Wall").gameObject;
            Destroy(childObj);

            newPoint = (x + 1) + "," + y; ;

            if (isColored(newPoint) == false)
            {
                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Left Wall").gameObject;
                Destroy(childObj);
                return newPoint;
            }
            else
            {
                wallNo = wallNo * -1;
                return "";
            }
        }
        else  if (wallNo == 2 && y > 0)
        {
            GameObject childObj = block.transform.Find("Top Wall").gameObject;
            Destroy(childObj);

            newPoint = x + "," + (y - 1);

            if (isColored(newPoint) == false)
            {
                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Bottom Wall").gameObject;
                Destroy(childObj);
                return newPoint;
            }
            else
            {
                wallNo = wallNo * -1;
                return "";
            }
        }
        else if (wallNo == -2 && y < 7)
        {
            GameObject childObj = block.transform.Find("Bottom Wall").gameObject;
            Destroy(childObj);

            newPoint = x + "," + (y + 1);

            if (isColored(newPoint) == false)
            {
                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Top Wall").gameObject;
                Destroy(childObj);
                return newPoint;
            }
            else
            {
                wallNo = wallNo * -1;
                return "";
            }
        }
        else
        {
            wallNo = wallNo * -1;
            return "";
        }
    }
}*/