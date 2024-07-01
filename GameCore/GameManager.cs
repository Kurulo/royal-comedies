using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static ScenesLoader SceneLoader;

    private bool _isGamePaused = false;
    public bool IsGamePaused {  get { return _isGamePaused; } }

    public delegate void GamePused();
    public static event GamePused GamePausedEvent;
    public static event GamePused GameResumeEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }   

        if (SceneLoader == null)
        {
            SceneLoader = new ScenesLoader();
        }
    }

    public void PauseGame()
    {
        if (!_isGamePaused)
        {
            _isGamePaused = true;
            GamePausedEvent();
        }
    }

    public void ResumeGame()
    {
        if (_isGamePaused)
        {
            _isGamePaused = false;
            GameResumeEvent();
        }
    }

    public void QuiteAplication()
    {
#if UNITY_WEBGL
        QuitWebGLGame();
#else
        Application.Quit();
#endif
        Debug.Log("Aplication is quite");

    }

    private void QuitWebGLGame()
    {
        // Call a JavaScript function to close the browser tab or window
        Application.ExternalEval("window.close();");
    }
}
