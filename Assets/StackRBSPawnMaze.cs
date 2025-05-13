using UnityEngine;
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

    private string possiblePoint;

    private string[] stack = new string[Stack.MaxSize];
    private int top = -1;

    private int paths;
    private string currentPoint;

    private int[] directions = { -2, -1, 1, 2 };
    private bool moved;

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

        Stack.push(ref top, stack, startingPoint);

        getFilling(startingPoint);
        ChangeColorRed(startingPoint);

        int[] validDirections = new int[] { -2, -1, 1, 2 };
        direction = validDirections[Random.Range(0, validDirections.Length)];

        //Debug.Log(direction);
        RemoveWall(startingPoint, direction);

        newPoint = startingPoint;

        string nextPoint = RemoveWall(newPoint, direction);
        Stack.push(ref top, stack, nextPoint);

        while (!Stack.isEmpty(top))
        {
            moved = false;

            paths = 4;

            nextPoint = Stack.peek(stack, top);
            string[] coords = nextPoint.Split(',');
            int x = int.Parse(coords[0]);
            int y = int.Parse(coords[1]);

            if (x + 1 < 12 && isColored((x + 1) + "," + y))
            {
                paths -= 1;
            }
            if (x - 1 >= 0 && isColored((x - 1) + "," + y))
            {
                paths -= 1;
            }
            if (y + 1 < 8 && isColored(x + "," + (y + 1)))
            {
                paths -= 1;
            }
            if (y - 1 >= 0 && isColored(x + "," + (y - 1)))
            {
                paths -= 1;
            }

            if (paths == 0)
            {
                nextPoint = Stack.pop(ref top, stack);
            }
            else
            {
                int[] shuffledDirections = ShuffleArray(directions);

                for (int i = 0; i < directions.Length; i++)
                {
                    string currentPoint = RemoveWall(nextPoint, directions[i]);
                    if (currentPoint != "")
                    {
                        Stack.push(ref top, stack, currentPoint);
                        ChangeColorRed(currentPoint);
                        moved = true;
                    }
                }

                if (!moved)
                {
                    nextPoint = Stack.pop(ref top, stack);
                }
            }
        }





        Stack.printStack(stack ,top);
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
                return "";
            }
        }
        else if (wallNo == 2 && y > 0)
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
                return "";
            }
        }
        else
        {
            return "";
        }
    }
}

internal class Stack
{
    public const int MaxSize = 100;

    public static bool IsFull(int top)
    {
        if (top == MaxSize - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void push(ref int top, string[] stack, string value)
    {
        if (!IsFull(top))
        {
            top += 1;
            stack[top] = value;
        }
        else
        {
            Debug.Log("Stack is full, data not added");
        }
    }

    public static string pop(ref int top, string[] stack)
    {
        string poppedItem;
        if (isEmpty(top))
        {
            Debug.Log("Stack is empty nothing to pop");
            poppedItem = "";
        }
        else
        {
            poppedItem = stack[top];
            top -= 1;
        }
        return poppedItem;
    }

    public static bool isEmpty(int top)
    {
        if (top == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string peek(string[] stack, int top)
    {
        string peekedItem;
        if (isEmpty(top))
        {
            Debug.Log("Stack is empty nothing to peek");
            peekedItem = "";
        }
        else
        {
            peekedItem = stack[top];
        }
        return peekedItem;
    }

    public static void printStack(string[] stack, int top)
    {
        if (!isEmpty(top))
        {
            for (int i = 0; i <= top; i++)
            {
                Debug.Log(stack[i]);
            }
        }
        else
        {
            Debug.Log("Stack is empty");
        }
    }
}
