using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;
    
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction) //quay doi tuong explosion den huong moi
    {
        //quay dua vao huong cu the vd : down thi se quay xuong
        float angle = Mathf.Atan2(direction.y, direction.x); //tinh toan goc cua vector huong moi so voi truc x(ben phai). ket qua tra ve la mọt goc theo don vi radian.
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); //tao ra mot quay xoay theo truc z(theo chieu kim dong ho) voi goc tinh duoc o buoc tren. ket qua la xuay dou tuong den huong moi, o day phai chuyen sang do bang angle * Mathf.Rad2Deg
    }


    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
