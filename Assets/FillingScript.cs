using UnityEngine;
using UnityEngine.SceneManagement;

public class FillingScript : MonoBehaviour
{
    public SpriteRenderer spriteR;
    private GameObject playerObj;

    public GameManagerScript gameManagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.Find("Player").gameObject;
        gameManagerScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteR.color.Equals(Color.green))
        {
            SIBSpawnMazeScript.level += 1;
            gameManagerScript.win();
        }
    }
}
