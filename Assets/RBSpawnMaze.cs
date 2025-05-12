using UnityEngine;

public class RBSpawnMaze : MonoBehaviour
{
    public GameObject square;
    //12 x 8

    public GameObject hLine;
    public GameObject vLine;

    private string startingPoint;
    private GameObject startingBlock;

    private SpriteRenderer spriteR;

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

        startingPoint = (Random.Range(0, 12) + "," + Random.Range(0, 8)).ToString();

        ChangeColor(startingPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColor(string startingPoint)
    {
        startingBlock = GameObject.Find(startingPoint);

        GameObject childObj = startingBlock.transform.Find("Filling").gameObject;

        spriteR = childObj.GetComponent<SpriteRenderer>();
        spriteR.color = Color.red;
    }
}
