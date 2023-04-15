using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //thu vien cua unity

public class LevelMove : MonoBehaviour
{
   [SerializeField] private AudioSource levelUp;
   public int NextLevelIndex;//chi so cua scene se chuyen den

   private void OnTriggerEnter2D(Collider2D other)
   {
        print("Enter Trigger");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            levelUp.Play();
            print("Switching Scene to " + NextLevelIndex);
            Invoke(nameof(NextLevel), 1f); //tham so thu 2 giup tai new scene mot cach doc lap va khong no tiep voi scene khac. scene cu bi xoa va giai phong tai nguyen
        }
   }

   private void NextLevel()
   {
        SceneManager.LoadScene(NextLevelIndex, LoadSceneMode.Single);
   }
}
