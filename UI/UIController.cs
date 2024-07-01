using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _volumePanel;
    [SerializeField] private GameObject _tutorialPanel;

    [Header("Buttons")]
    [SerializeField] private Button _quiteButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Conected Components")]
    [SerializeField] private CurtainController _curtainController;

    [Header("Start Settings")]
    [SerializeField] private bool _closeMenuInStart = false;
    [SerializeField] private bool _isGameplayMenu = false;

    private void Start()
    {
        CloseVolumeMenu();

        if (_tutorialPanel != null )
            CloseTutorialPanel();

        if (_closeMenuInStart)
        {
            CloseMainMenu();
        }

        if (_quiteButton != null)
            _quiteButton.onClick.AddListener(()=>GameManager.Instance.QuiteAplication());
        if (_resumeButton != null)
            _resumeButton.onClick.AddListener(()=>ResumeGame());
        if (_mainMenuButton != null)
            _mainMenuButton.onClick.AddListener(()=>GoToMainMenu());
    }

    private void Update()
    {
        if (_isGameplayMenu)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
                OpenMainMenu();
                CloseVolumeMenu();
            }
        }
    }

    public void OpenTutorialPanel()
    {
        CloseMainMenu();
        _tutorialPanel.SetActive(true);
    }

    public void CloseTutorialPanel()
    {
        OpenMainMenu();
        _tutorialPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        CloseMainMenu();
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.ResumeGame();
        GameManager.SceneLoader.LoadSceneWithID(0);
    }

    public void OpenVolumeMenu()
    {
        CloseMainMenu();
        _volumePanel.SetActive(true);
    }

    public void CloseVolumeMenu()
    {
        OpenMainMenu();
        _volumePanel.SetActive(false);
    }

    public void CloseMainMenu()
    {
        _menuPanel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        _menuPanel.SetActive(true);
    }

    private IEnumerator ResumeGameWithDelay()
    {
        yield return new WaitForSeconds(1f);
    }
}
