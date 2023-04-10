using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float moveSpeed = 5f;

  private Rigidbody2D rb;
  public Animator animator; //lop duoc tu dong tao khi tao animation, giup quyet dinh thu tu thuc hien animation

  public Vector3 moveInput;

  private void Start()
  {
    animator = GetComponent<Animator>(); //doi tuong dang chua component duoc gan cho bien animator de su dung trong cac phuong thuc khac cua component(animator)
  }

  private void Update()
  {
    moveInput.x = Input.GetAxis("Horizontal"); //tu dong cai dat cac phim di chuyen cho nhan vat , vao edit -> project setting -> input manager de xem chi tiet
    moveInput.y = Input.GetAxis("Vertical");
    transform.position += moveInput * moveSpeed * Time.deltaTime;

    animator.SetFloat("Speed", moveInput.sqrMagnitude); //sqrMagnitude la do lon cua vec to di chuyen, day la ham de dat gia tri cua speed la moveInput.sqrMagnitude
    
    if(moveInput.x != 0) //neu dang nhat nut di chuyen
    {
      if(moveInput.x < 0) //neu nut do la sang phai thi quay phai con nguoc lai quay trai
      {
        transform.localScale = new Vector3(1, 1, 0);
      }else
      {
        transform.localScale = new Vector3(-1, 1, 0);
      }
    }
  }


}
