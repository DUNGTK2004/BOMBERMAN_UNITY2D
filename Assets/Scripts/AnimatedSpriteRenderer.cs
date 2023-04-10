using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour //la lop co so ma moi tap lenh bat nguon tu do
{
    private SpriteRenderer SpriteRenderer; // la 1 thanh phan (component) của đoi tuong gameObject duoc su dung de hien thi doi tuong duoi dang sprite 2D len man hinh

    public Sprite idleSprite; //nhan vat nhan roi
    public Sprite[] animationSprites; //mang chua cac animation cua nhan vat

    public float animationTime = 0.25f; 
    private int animationFrame;

    public bool loop = true; //dung de kiem soat viec lap lai cua hanh dong
    public bool idle = true; //dung de kiem soat trang thai nghi cua hanh dong

    private void Awake() //ham khoi tao cac gia tri mac dinh cua doi tuong de chuan bi cho viec chay cua doi tuong
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() //la ham trong MonoBehaviour duoc goi khi doi tuong duoc bat hoac tat trong canh (scene), dung de quan ly tai nguyen va dam bao rang cac thanh phan (component) hoat dong cua doi tuong duoc kich hoat hoac vo hieu hoa dung cach
    {
        SpriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        SpriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime); //dung de lap lai 1 ham sau 1 khoang thoi gian nhat dinh
    }

    private void NextFrame() //cap nhat hinh anh dang duoc hien thi 
    {
        animationFrame++;

        if(loop && animationFrame >= animationSprites.Length) {
            animationFrame = 0;
        }

        if(idle) {
            SpriteRenderer.sprite = idleSprite; //neu dang nhan roi thi gan hinh anh cho nhan vat nhan roi
        }else if(animationFrame >= 0 && animationFrame < animationSprites.Length){
            SpriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
