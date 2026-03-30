using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    [Header("Music Playlists")]
    [SerializeField] private AudioClip[] menuMusic;
    [SerializeField] private AudioClip[] gameMusic;

    private AudioClip[] currentPlaylist;
    private int currentIndex = 0;


    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();

        musicSource.loop = false;

        //Subscribe to scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        //Start music for the first loaded scene
        SetPlaylistForScene(SceneManager.GetActiveScene().name);
        PlayNextSong();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Switch playlist immediately when a new scene loads
        SetPlaylistForScene(scene.name);
    }

    private void SetPlaylistForScene(string sceneName)
    {
        if (sceneName == "Start" || sceneName == "Settings")
        {
            if (currentPlaylist != menuMusic)
            {
                currentPlaylist = menuMusic;
                currentIndex = 0;

                PlayNextSong();
            }
        }
        else
        {
            if (currentPlaylist != gameMusic)
            {
                currentPlaylist = gameMusic;
                currentIndex = 0;

                PlayNextSong();
            }
        }
    }

    private void PlayNextSong()
    {
        if (currentPlaylist.Length == 0)
        {
            return;
        }

        if (musicSource != null)
        {
            musicSource.clip = currentPlaylist[currentIndex];
            musicSource.Play();
        }

        currentIndex++;
        if (currentIndex >= currentPlaylist.Length)
        {
            //When out of songs, go back to top of list
            currentIndex = 0;
        }
    }
}
