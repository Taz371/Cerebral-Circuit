using UnityEngine;
using UnityEngine.SceneManagement;

public class PaverScript : MonoBehaviour
{
    public float paverSpeed = 1f;

    private int direction = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 5)
        {
            Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        MovePacer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3)
        {
            Destroy(collision.gameObject);
        }

        Centralise();

        Turn();
    }

    void Turn()
    {
        if (direction == 0)
        {
            direction = Random.Range(0, 4);
        }
        else if (direction == 1)
        {
            direction = Random.Range(0, 4);
        }
        else if (direction == 2)
        {
            direction = Random.Range(0, 4);
        }
        else if (direction == 3)
        {
            direction = Random.Range(0, 4);
        }
    }

    void MovePacer()
    {
        if (direction == 1 && transform.position.y < 2)
        {
            transform.position += new Vector3(0, 1, 0) * paverSpeed * Time.deltaTime;
        }
        else if (direction == 2 && transform.position.y > -2)
        {
            transform.position += new Vector3(0, -1, 0) * paverSpeed * Time.deltaTime;
        }
        else if (direction == 3 && transform.position.x > -3)
        {
            transform.position += new Vector3(-1, 0, 0) * paverSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(1, 0, 0) * paverSpeed * Time.deltaTime;
            direction = 0;
        }
    }

    void Centralise()
    {
        if (direction == 0) // Right
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }
        else if (direction == 1) // Up
        {
            transform.position += new Vector3(0, 0.5f, 0);
        }
        else if (direction == 2) // Down
        {
            transform.position += new Vector3(0, -0.5f, 0);
        }
        else if (direction == 3) // Left
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }
    }
}
