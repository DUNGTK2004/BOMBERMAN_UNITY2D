using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public GameObject[] players;

   public void CheckWinState()
   {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if(player.activeSelf) //neu nguoi choi con hoat dong thi tang len 1
            {
                aliveCount++;
            }
        }
        
        if(aliveCount <= 1) {
            Invoke(nameof(NewRound), 3f); //neu so nguoi choi con hoat dong be hon hoac bang 1 thi sau 3 s se load lai man moi
        }
   }

   private void NewRound()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //tai lai scene (load lai game moi)
   }
}
