using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifeTime;
    private int direction = 1; // 1 = ph?i, -1 = trái

    public void SetDirection(int dir)
    {
        direction = dir;

        // L?t m?i tên n?u c?n (ch? c?n n?u sprite có h??ng)
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * direction;
        transform.localScale = localScale;
    }

    public void ActiveProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;

        if (lifeTime >= resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OntriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
