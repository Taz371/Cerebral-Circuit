using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float playerSpeed;

    public GameManagerScript gameManagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        gameManagerScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManagerScript.questionOnScreen)
        {
            Move();
        }
        else
        {
            myRigidBody.linearVelocity = Vector3.zero;
        }
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            myRigidBody.linearVelocity = Vector2.up * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            myRigidBody.linearVelocity = Vector2.down * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            myRigidBody.linearVelocity = Vector2.left * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.D) == true)
        {
            myRigidBody.linearVelocity = Vector2.right * playerSpeed * Time.deltaTime;
        }
    }
}
