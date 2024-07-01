using UnityEngine;
using UnityEngine.UI;

public class SkipCaricatureButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SkipCaricature());
    }

    public void SkipCaricature()
    {
        GameManager.SceneLoader.LoadSceneWithID(2);
        GameManager.Instance.ResumeGame();
    }
}
