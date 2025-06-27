using UnityEngine;

public class AxeSwingTrap : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2f;      // How fast it swings
    [SerializeField] private float maxSwingAngle = 60f;  // Maximum swing angle

    private float currentAngle = 0f;
    [SerializeField] private float direction = 1f; // 1 = clockwise, -1 = counterclockwise

    void Update()
    {
        currentAngle += direction * swingSpeed * Time.deltaTime;

        if (Mathf.Abs(currentAngle) >= maxSwingAngle)
        {
            direction *= -1; // Change direction at limits
            currentAngle = Mathf.Clamp(currentAngle, -maxSwingAngle, maxSwingAngle);
        }

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
