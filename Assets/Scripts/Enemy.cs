using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Speed of the enemy movement
    [SerializeField] private float distance = 5f; // Distance to move before reversing direction
    private Vector3 startPos; // Starting position of the enemy
    private bool movingRight = true; // Direction of movement
	void Start()
    {
        startPos = transform.position; // Store the starting position
	}

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance; // Calculate left boundary
        float rightBound = startPos.x + distance; // Calculate right boundary
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound) // Check if the enemy has reached the right boundary
            {
                movingRight = false; // Reverse direction
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound) // Check if the enemy has reached the left boundary
            {
                movingRight = true; // Reverse direction
                Flip();
            }
        }
    }

    void Flip()
    {
        // This method can be used to flip the enemy sprite if needed
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the x-axis
        transform.localScale = theScale;    
	}
}
