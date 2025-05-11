using UnityEngine;

public class PaverScript : MonoBehaviour
{
    private int paverXStartPoint = -5;
    public float heightOffset = 2.5f;
    public float paverSpeed = 3f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(paverXStartPoint, Random.Range(0 - heightOffset, 0 + heightOffset), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * paverSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
