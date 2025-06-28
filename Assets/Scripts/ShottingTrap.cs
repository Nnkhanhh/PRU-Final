using UnityEngine;

public class ShottingTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private Vector2 shootDirection = Vector2.right; // Default (1,0)

    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        int arrowIndex = FindArrows();
        GameObject arrow = arrows[arrowIndex];
        arrow.transform.position = firePoint.position;

        ArrowCollision projectile = arrow.GetComponent<ArrowCollision>();
        projectile.SetDirection(shootDirection);
        projectile.ActiveProjectile();
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
