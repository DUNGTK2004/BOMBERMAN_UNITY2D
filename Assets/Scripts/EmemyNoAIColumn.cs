using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyNoAIColumn : MonoBehaviour
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
        if (IsFacingUp()){
            rigidbody.velocity = new Vector2(0f, moveSpeed);
        } else {
            rigidbody.velocity = new Vector2(0f, -moveSpeed);     
        }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
        transform.localScale = new Vector2(transform.localScale.x, -(Mathf.Sign(rigidbody.velocity.y)));
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

   private bool IsFacingUp()
   {
        return transform.localScale.y > Mathf.Epsilon;
   }

}
