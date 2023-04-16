using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Campaign()
    {
        SceneManager.LoadScene(7);
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

    public void Story(){
        SceneManager.LoadScene(6);
    }

    public void Continue1(){
        SceneManager.LoadScene(1);
    }

    public void Continue2(){
        SceneManager.LoadScene(2);
    }

    public void Continue3(){
        SceneManager.LoadScene(5);
    }

    public void End(){
        SceneManager.LoadScene(0);
    }
}
