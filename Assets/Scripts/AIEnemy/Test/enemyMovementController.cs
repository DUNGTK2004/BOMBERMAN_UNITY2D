using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementController : MonoBehaviour
{
    public float enemySpeed;
    Rigidbody2D enemyRB;
    Animator enemyAnim;

    //Khai bao cac bien de enemy co the lat duoc
    public GameObject enemyGraphic; //de dua con cua no la enemy vao
    bool faceingRight = true; //bien giup xem thu enemy quay mat sang trai hay sang phai
    float facingTime = 5f; //5s se lat 1 lan
    float nextFlip = 0f; //ngay sau khi bat dau game se lat luon
    bool canFlip = true; //khi nhan vat chua cham vao box collider thi con enemy van co the lat

    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponentInChildren<Animator>(); //su dung animator tu con cua no la enemy1;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFlip){ //Time.time tra ve thoi gian hien tai cua game , neu thoi gian hien tai cua game lon hon lan lat tiep theo  
            nextFlip = Time.time + facingTime; //tang bien nextFlip len de tiep tuc update
            flip();
        }
    }
    //chuc nang lat
    void flip()
    {
        if(!canFlip) return;
        faceingRight = !faceingRight;
        Vector3 theScale = enemyGraphic.transform.localScale; //truy cap den Scale cua enemy con
        theScale.x *= -1; // dung de lat
        enemyGraphic.transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(faceingRight && other.transform.position.x < transform.position.x) //neu enemy dang quay sang ben phai ma va cham voi player thi neu toa do x cua nhan vat < cua enemy thi se lat sang trai
            {
                flip();
            }else if(!faceingRight && other.transform.position.x > transform.position.x)
            {
                flip();
            }
            canFlip = false; //khi gap nhan vat thi se khong the quay tiep ma chi quay ve huong nhan vat
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if(other.tag == "Player"){ //khi gap player thi se co 1 luc keo nhan vat di chuyen
    //         if(!faceingRight){
    //             enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
    //         }else enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
    //         enemyAnim.SetBool("Run", true);
    //     }
    // }

    void  OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            canFlip = true; //khi nguoi choi ko con va cham thi tiep tuc chuc nang lat sau 5s
            enemyRB.velocity = new Vector2(0, 0); //dat toc do cua rigidbody cua doi tuong enemy thanh(0, 0) nghia la no se khong di chuyen nua
            enemyAnim.SetBool("Run", false); //cho animation tro ve nhu ban dau
            enemyAnim.SetBool("Run1", false);
        }
    }

//
    // public float vectorX;
    // public float vectorY;
    // public float sizeVector;
    // public float directionMoveX;
    // public float directionMoveY;

    // void OnTriggerStay2D(Collider2D other)
    // {
    //      vectorX = other.transform.position.x - transform.position.x;
    //         vectorY = other.transform.position.y - transform.position.y;
    //         sizeVector = Mathf.Sqrt(vectorX * vectorX + vectorY * vectorY);
    //         directionMoveX = vectorX / sizeVector;
    //     if (other.tag == "Player"){ //khi gap player thi se co 1 luc keo nhan vat di chuyen
    //         // if(!faceingRight){
    //         //     enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
    //         // }else enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
           
    //         directionMoveY = vectorY / sizeVector;
    //         enemyRB.AddForce(new Vector2(directionMoveX, directionMoveY) * enemySpeed);
    //         enemyAnim.SetBool("Run", true);
    //     }
    // }

    void OnTriggerStay2D(Collider2D other)
{
    if (other.tag == "Player")
    {
       
        Vector2 direction = (other.transform.position - transform.Find("Enemy1").position).normalized;
        enemyRB.AddForce(direction * enemySpeed);
        // if(enemyRB.velocity.x == 0){
        //             enemyAnim.SetBool("Run", true);
             enemyAnim.SetBool("Run", true);

    }
}

//Mot thuat toan khac 

//     void MoveTowardsTarget(Vector3 target)
// {
//     transform.Find("Enemy1").position = Vector3.MoveTowards(transform.Find("Enemy1").position, target, enemySpeed * Time.deltaTime);
// }

// void OnTriggerStay2D(Collider2D other)
// {
//     if (other.tag == "Player")
//     {
//         MoveTowardsTarget(other.transform.position);
//         enemyAnim.SetBool("Run", true);
//     }
// }


}
