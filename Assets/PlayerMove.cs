using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float playerSpeed;

    public GameManagerScript gameManagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 10);

        gameManagerScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManagerScript.questionOnScreen && !gameManagerScript.winScreenActive)
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
        if (Input.GetKey(KeyCode.W) == true)
        {
            myRigidBody.linearVelocity = Vector2.up * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            myRigidBody.linearVelocity = Vector2.down * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            myRigidBody.linearVelocity = Vector2.left * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            myRigidBody.linearVelocity = Vector2.right * playerSpeed;
        }
        else
        {
            myRigidBody.linearVelocity = Vector3.zero;
        }
    }
}
