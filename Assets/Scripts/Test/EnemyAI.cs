using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float nextWPDistance; //khoang cach toi thieu giua doi tuong di chuyen va diem tiep theo trong duong di da tinh toan
    public Seeker seeker; //dung de tim duong di
    public SpriteRenderer characterSR;
    Transform target;
    Path path;
    //Coroutine se dinh kem voi gameObject de chay
    Coroutine moveCoroutine; //coroutine la mot co che trong unity cho phep tam dung va tiep tuc thuc thi 1 phan cua chuong trinh theo mot thoi gian khac voi nhung phan con lai, hay goi la chay tren 1 luong (thread) doc lap voi chuong trinh chinh
    
    private void Start()
    {
        target = FindObjectOfType<MovementController>().gameObject.transform; //dung de tim vi tri dau tien cua player
        InvokeRepeating("CalculatePath", 0f, 0.5f);
    }
    void CalculatePath()
    {
        //seeker la mot component co san trong thu vien A* pathfinding project tra ve duong di ngan nhat giua 2 diem
        if(seeker.IsDone())  //day la ham kiem tra xem viec tim duong co dang duoc thuc hien hay khong , dung de tranh truong hop goi ham startpath khi mot tim kiem duong di khac dang duoc thuc hien
        {
            seeker.StartPath(transform.position, target.position, OnPathCallback); //tim duong di giua enemy va player voi ban do duoc truyen vao, tra ve mot duong dan thong qua ham OnPathCallback
        }
    }

    void OnPathCallback(Path p)
    {
        if(p.error) return;

        path = p;
        //Move to target;
        MoveToTarget(); //di chuyen enemy den nguoi choi

    }

    void MoveToTarget()
    {   //xem xem co coroutine nao dang chay khong , neu co thi tat coroutine do va chay coroutine moi de di chuyen enemy den muc tieu
        if(moveCoroutine != null) StopCoroutine(moveCoroutine); 
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while(currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized; //day la vecto don vi cua vecto noi sap di toi - vi tri hien tai
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);

            if(distance < nextWPDistance)
            {
                currentWP++;
            }

            if(force.x != 0)
            {
                if(force.x < 0){
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                }else characterSR.transform.localScale = new Vector3(-1, 1, 0);
            }

            yield return null;
        }
    }
}
