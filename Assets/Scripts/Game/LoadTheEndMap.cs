using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTheEndMap : MonoBehaviour
{
    [SerializeField] private AudioSource levelUp;
    public int nextLevelIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            levelUp.Play();
            Invoke(nameof(NextLevel), 2f);
        }
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex, LoadSceneMode.Single);
    }
}
