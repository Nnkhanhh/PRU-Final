using UnityEngine;

public class ArrowCollision : EnemyDamage
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float resetTime = 5f;

    private float lifeTime;
    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Rotate the arrow to face the movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ActiveProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        lifeTime += Time.deltaTime;

        if (lifeTime >= resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OntriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
