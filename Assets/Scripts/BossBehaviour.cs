
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float HitPoints;
    public float HitPointslMax = 5;
    public BossHealthBar healthBar;

    private void Start()
    {
        HitPoints = HitPointslMax;
        healthBar.SetHealth(HitPoints, HitPointslMax);
    }

    public void TakeHit(float damage)
    {
        HitPoints -= damage;
        healthBar.SetHealth(HitPoints, HitPointslMax);
    }

}
