using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy2 : MonoBehaviour
{
    private AIPath aiPath;
    public Vector3 vector;
    public Animator animator;
    public float xx;
    public float xx1;
    public float changePosition;
    //public AnimatedSpriteRenderer spriteRendererDeath;
    public new Rigidbody2D rigidbody {get; private set;}
    public float hp = 10;
    public float speed;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        vector = transform.position;
        animator = GetComponent<Animator>();
        xx = vector.x * 100;

        xx1 = vector.x * 100;

        changePosition = xx1 - xx;
        animator.SetFloat("Hp", hp);
    }

    // Update is called once per frame
    void Update()
    {
        xx1 = vector.x * 100;
        vector = transform.position;
        changePosition = xx1 - xx;

     
        animator.SetFloat("Speed", changePosition);
        // if(changePosition != 0)
        // {
        //     if(changePosition < 0)
        //     {
        //         transform.localScale = new Vector3(1, 1, 0);
        //     }else transform.localScale = new Vector3(-1, 1, 0);
        // }
        xx = xx1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion")){
            hp = 0;
            animator.SetFloat("Hp", hp);
            DeathSequence();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
                GetComponent<Collider2D>().isTrigger = true;
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
