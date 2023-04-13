using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyNoAIRow : MonoBehaviour
{
   [SerializeField] float moveSpeed = 1f;

   new Rigidbody2D rigidbody;
   BoxCollider2D myBoxCollider;

   private void Start() 
   {
        rigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
   }

   private void Update()
   {
        if (IsFacingRight()){
            rigidbody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            rigidbody.velocity = new Vector2(-moveSpeed, 0f);     
        }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.velocity.x)), transform.localScale.y);
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion")){
          DeathSequence();
        }
   }

   private void DeathSequence(){
        enabled = false;
        Invoke(nameof(OnDeathSequenceEnded), 1f);
   }

   private void OnDeathSequenceEnded(){
        gameObject.SetActive(false);
   }

   private bool IsFacingRight()
   {
        return transform.localScale.x > Mathf.Epsilon;
   }

}
