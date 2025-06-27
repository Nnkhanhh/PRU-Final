using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isRunning)
            elapsedTime += Time.deltaTime;
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public float StopTimer()
    {
        isRunning = false;
        return elapsedTime;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
