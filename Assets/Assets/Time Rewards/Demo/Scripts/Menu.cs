using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void LoadDaily()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadActive()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTime()
    {
        SceneManager.LoadScene(3);
    }
}
