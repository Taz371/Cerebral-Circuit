using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SIBSpawnMazeScript : MonoBehaviour
{
    public GameObject square;

    public float mazeGenerationSpeed;

    private string point;
    private GameObject block;

    private SpriteRenderer spriteR;

    private string newPoint;

    private int squaresCovered = 0;
    private int direction = 0;

    private string[] stack = new string[Stack.MaxSize];
    private int top = -1;

    private int paths;
    private string currentPoint;

    private int[] directions = {-2, -1, 1, 2};
    private bool moved;

    public float mazeLength;
    public float mazeHeight;

    private GameObject childObj;

    public static int level = 0;

    private Dictionary <string, List <string>> mazeGraph = new Dictionary<string, List <string>>();
    private string listToString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mazeLength += level;
        mazeHeight += level;
        AdjustCamera();
        StartCoroutine(CreateMaze());
    }

    void SetWinArea()
    {
        string winPoint = (mazeLength-1) + "," + (mazeHeight-1);
        getFilling(winPoint);
        spriteR.color = Color.green;
    }

    void AdjustCamera()
    {
        GameObject camera = GameObject.Find("Main Camera");
        Camera cameraComponent = camera.GetComponent<Camera>();

        // Formula to position the camera at the centre of the maze
        cameraComponent.orthographicSize = (mazeLength / 2) + 1;
        camera.transform.position = new Vector3((mazeLength / 2) - 0.5f, -1 * ((mazeHeight / 2) - 0.5f), -10);
    }

    IEnumerator CreateMaze()            
    {
        for (int xCord = 0; xCord <= mazeLength - 1; xCord++)
        {
            for (int yCord = 0; yCord <= mazeHeight - 1; yCord++)
            {
                var newNode = Instantiate(square, new Vector3(xCord, -yCord, 0), transform.rotation);
                newNode.name = xCord + "," + yCord;
            }
        }

        string startingPoint = Random.Range(0, (int)mazeLength) + "," + Random.Range(0, (int)mazeHeight);

        //Debug.Log("Starting Point: " + startingPoint);

        Stack.push(ref top, stack, startingPoint);
        
        //Debug.Log(startingPoint);

        ChangeColorRed(startingPoint);
        ChangeLayerToVisited(startingPoint);

        int[] validDirections = new int[] { -2, -1, 1, 2 };
        direction = validDirections[Random.Range(0, validDirections.Length)];

        //Debug.Log(direction);

        string nextPoint = RemoveWall(startingPoint, direction);

        if (nextPoint != "")
        {
            Stack.push(ref top, stack, nextPoint);
        }

        while (!Stack.isEmpty(top))
        {
            moved = false;
            paths = 4;

            nextPoint = Stack.peek(stack, top);
            string[] coords = nextPoint.Split(',');
            //Debug.Log("Parsing point: " + nextPoint);

            int x = int.Parse(coords[0]);
            int y = int.Parse(coords[1]);

            if (x + 1 < mazeLength && isVisited((x + 1) + "," + y))
            {
                paths -= 1;
            }
            if (x - 1 >= 0 && isVisited((x - 1) + "," + y))
            {
                paths -= 1;
            }
            if (y + 1 < mazeHeight && isVisited(x + "," + (y + 1)))
            {
                paths -= 1;
            }
            if (y - 1 >= 0 && isVisited(x + "," + (y - 1)))
            {
                paths -= 1;
            }

            if (paths == 0)
            {
                nextPoint = Stack.pop(ref top, stack);
                ChangeColorWhite(nextPoint);
                yield return new WaitForSeconds(mazeGenerationSpeed);
            }
            else
            {
                int[] shuffledDirections = ShuffleArray(directions);
                ChangeColorRed(nextPoint);
                ChangeLayerToVisited(nextPoint);

                for (int i = 0; i < shuffledDirections.Length; i++)
                {
                    string currentPoint = RemoveWall(nextPoint, shuffledDirections[i]);
                    if (currentPoint != "")
                    {
                        yield return new WaitForSeconds(mazeGenerationSpeed);
                        ChangeColorRed(currentPoint);
                        ChangeLayerToVisited(currentPoint);
                        Stack.push(ref top, stack, currentPoint);

                        moved = true;
                        break;
                    }
                }

                if (!moved)
                {
                    nextPoint = Stack.pop(ref top, stack);
                    ChangeColorWhite(nextPoint);
                    yield return new WaitForSeconds(mazeGenerationSpeed);
                }
            }
        }

        SetWinArea();

        Debug.Log($"Count: {mazeGraph.Count}");

        foreach (KeyValuePair<string, List<string>> item in mazeGraph)
        {
            listToString = "";
            for (int i = 0; i < item.Value.Count; i++)
            {
                listToString = listToString + " " + item.Value[i];
            }

            string message = ($"Key: {item.Key}, Value: {listToString}");
            Debug.Log(message);
        }

        //Stack.printStack(stack ,top);
    }

    int[] ShuffleArray(int[] array)
    {
        int[] shuffledArray = (int[])array.Clone();
        for (int i = 0; i < shuffledArray.Length; i++)
        {
            int rnd = Random.Range(i, shuffledArray.Length);
            int temp = shuffledArray[rnd];
            shuffledArray[rnd] = shuffledArray[i];
            shuffledArray[i] = temp;
        }
        return shuffledArray;
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

    void ChangeColorWhite(string point)
    {
        getFilling(point);
        spriteR.color = Color.white;
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

        return color.Equals(Color.red);
    }

    void ChangeLayerToVisited(string point)
    {
        block = GameObject.Find(point);
        block.layer = 3;
    }

    bool isVisited(string point)
    {
        if (point != "")
        {
            block = GameObject.Find(point);
            if (block!= null && block.layer == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        // 1 = Left Wall
        // -1 = Right Wall
        // 2 = Top Wall
        // -2 = Bottom Wall

        string[] coords = point.Split(',');
        int x = int.Parse(coords[0]);
        int y = int.Parse(coords[1]);

        //Debug.Log(x);
        //Debug.Log(y);


        if (wallNo == 1 && x > 0)
        {
            newPoint = (x - 1) + "," + y;

            if (isVisited(newPoint) == false)
            {
                block = GameObject.Find(point);
                GameObject childObj = block.transform.Find("Left Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Right Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                AddToGraph(point, newPoint);

                return newPoint;
            }
            else
            {
                return "";
            }
        }
        else if (wallNo == -1 && x < mazeLength-1)
        {
            newPoint = (x + 1) + "," + y;

            if (isVisited(newPoint) == false)
            {
                block = GameObject.Find(point);
                GameObject childObj = block.transform.Find("Right Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Left Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                AddToGraph(point, newPoint);

                return newPoint;
            }
            else
            {
                return "";
            }
        }
        else if (wallNo == 2 && y > 0)
        {
            newPoint = x + "," + (y - 1);

            if (isVisited(newPoint) == false)
            {
                block = GameObject.Find(point);
                GameObject childObj = block.transform.Find("Top Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Bottom Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                AddToGraph(point, newPoint);

                return newPoint;
            }
            else
            {
                return "";
            }
        }
        else if (wallNo == -2 && y < mazeHeight-1)
        {
            newPoint = x + "," + (y + 1);

            if (isVisited(newPoint) == false)
            {
                block = GameObject.Find(point);
                GameObject childObj = block.transform.Find("Bottom Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                GameObject adjacentBlock = GameObject.Find(newPoint);
                childObj = adjacentBlock.transform.Find("Top Wall").gameObject;
                if (childObj != null)
                {
                    Destroy(childObj);
                }

                AddToGraph(point, newPoint);

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

    void AddToGraph(string point, string newPoint)
    {
        if (!mazeGraph.ContainsKey(point))
        {
            mazeGraph.Add(point, new List<string>());
            if (!mazeGraph[point].Contains(newPoint))
            {
                mazeGraph[point].Add(newPoint);
            }
        }
        else
        {
            if (!mazeGraph[point].Contains(newPoint))
            {
                mazeGraph[point].Add(newPoint);
            }
        }

        if (!mazeGraph.ContainsKey(newPoint))
        {
            mazeGraph.Add(newPoint, new List<string>());
            if (!mazeGraph[newPoint].Contains(point))
            {
                mazeGraph[newPoint].Add(point);
            }
        }
        else
        {
            if (!mazeGraph[newPoint].Contains(point))
            {
                mazeGraph[newPoint].Add(point);
            }
        }
    }
}

internal class Stack
{
    public static int MaxSize = 10000000;

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
