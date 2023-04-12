using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleCollider : MonoBehaviour
{
    
     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion")){
            // hp = 0;
            // animator.SetFloat("Hp", hp);
            DeathSequence();
        }
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

    private void DeathSequence(){
        enabled = false;
        //spriteRendererDeath.enabled = true;
        Invoke(nameof(OnDeathSequenceEnded), 2f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        //FindObjectOfType<GameManager>().CheckWinState();
    }
}
