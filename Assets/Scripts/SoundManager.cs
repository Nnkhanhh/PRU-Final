using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    public AudioClip menuBGM;
    public AudioClip map1BGM;
    public AudioClip map2BGM;
    public AudioClip map3BGM;
    public AudioClip map4BGM;
    public AudioClip map5BGM;
    public AudioClip winBGM;

    private void Awake()
    {
        // Nếu đã có một Instance, thì huỷ GameObject mới (tránh trùng)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Đặt bản thân làm Instance
        Instance = this;

        // Không bị huỷ khi load scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Menu": PlayBGM(menuBGM); break;
            case "Map1": PlayBGM(map1BGM); break;
            case "Map2": PlayBGM(map2BGM); break;
            case "Map3": PlayBGM(map3BGM); break;
            case "Map4": PlayBGM(map4BGM); break;
            case "Map5": PlayBGM(map5BGM); break;
            case "WinUI": PlayBGM(winBGM); break;
            default: audioSource.Stop(); break;
        }
    }

    void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying)
            return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
