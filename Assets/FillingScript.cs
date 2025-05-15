using UnityEngine;

public class FillingScript : MonoBehaviour
{
    public SpriteRenderer spriteR;
    private GameObject playerObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteR.color.Equals(Color.green))
        {
            Debug.Log("YOU WON!!!");
            Destroy(playerObj);
        }
    }
}
