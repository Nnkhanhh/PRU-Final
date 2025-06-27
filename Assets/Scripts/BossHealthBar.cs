using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour 
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
     private Transform boss;

    private void Start()
    {
        boss = transform.parent; // assume this script is on Canvas, which is a child of Boss
    }
    public void SetHealth(float health,float maxHealth)
    {
        Slider.gameObject.SetActive(true);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }


    private void LateUpdate()
    {
        if (boss != null)
        {
            transform.position = boss.position + Offset;
        }
    }


}
