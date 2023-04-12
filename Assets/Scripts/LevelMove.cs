using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //thu vien cua unity

public class LevelMove : MonoBehaviour
{
   public int NextLevelIndex;//chi so cua scene se chuyen den

   private void OnTriggerEnter2D(Collider2D other)
   {
        print("Enter Trigger");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            print("Switching Scene to " + NextLevelIndex);
            SceneManager.LoadScene(NextLevelIndex, LoadSceneMode.Single); //tham so thu 2 giup tai new scene mot cach doc lap va khong no tiep voi scene khac. scene cu bi xoa va giai phong tai nguyen
        }
   }
}
