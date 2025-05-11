using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float playerSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) == true)
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
