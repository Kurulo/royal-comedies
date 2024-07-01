using UnityEngine;

public class CaricatureEvents : MonoBehaviour
{
    [Header("LoadSceneEvent")]
    [SerializeField] private int _sceneID;

    public void LoadSceneEvent()
    {
        GameManager.Instance.ResumeGame();
        GameManager.SceneLoader.LoadSceneWithID(_sceneID);
    }
}
