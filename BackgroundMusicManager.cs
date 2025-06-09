using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;

    public AudioSource musicSource;

    [Header("Main Menu & Victory Music")]
    public AudioClip menuBGM;
    public AudioClip victoryBGM;

    [Header("Stage Music")]
    public AudioClip stage1BGM;
    public AudioClip stage2BGM;
    public AudioClip stage3BGM;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusicByScene();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicByScene();
    }

    public void PlayMusicByScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                PlayMusic(menuBGM);
                break;
            case "Lvl 1":
                PlayMusic(stage1BGM);
                break;
            case "Lvl 2":
                PlayMusic(stage2BGM);
                break;
            case "Lvl 3":
                PlayMusic(stage3BGM);
                break;
            default:
                PlayMusic(menuBGM);
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayVictoryMusic()
    {
        PlayMusic(victoryBGM);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
