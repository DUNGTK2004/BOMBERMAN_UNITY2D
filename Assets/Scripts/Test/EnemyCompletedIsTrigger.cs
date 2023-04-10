using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCompletedIsTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }
    private void DeathSequence(){
        enabled = false;
        Invoke(nameof(OnDeathSequenceEnded), 2f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            GetComponent<Collider2D>().isTrigger = true;
            MovementController player = other.gameObject.GetComponent<MovementController>();
            if(player != null)
            {
                player.DeathSequence();
            }
        }
    }
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update

    // Update is called once per frame
}
