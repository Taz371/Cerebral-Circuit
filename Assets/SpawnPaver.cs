using UnityEngine;

public class SpawnPaver : MonoBehaviour
{
    public GameObject paver;
    private Vector3 paverPosition;
    private int paverXStartPoint = -5;

    private float ycord = 1.2f;
    public int amountOfPavers = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amountOfPavers; i++)
        {
            ycord = -2.5f + Random.Range(0, 6);
            paverPosition = new Vector3(paverXStartPoint, ycord, 0);
            Instantiate(paver, paverPosition, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
