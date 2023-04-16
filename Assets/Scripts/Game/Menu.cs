using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Campaign()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene(3);
    }

    public void Help()
    {
        SceneManager.LoadScene(4);
    }

    public void Menu1()
    {
        SceneManager.LoadScene(0);
    }

}
