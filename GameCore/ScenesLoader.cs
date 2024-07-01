using UnityEngine.SceneManagement;

public class ScenesLoader
{
    public void LoadSceneWithID(int id)
    {
        SceneManager.LoadScene(id);
    } 

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
