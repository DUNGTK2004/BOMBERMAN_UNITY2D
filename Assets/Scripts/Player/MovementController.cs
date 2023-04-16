using UnityEngine;

public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down; //Vector2 : bieu dien cac vector va diem 2D , down -> vector2(0, -1)
    public float speed = 5f;
    
    [Header("Keycode")] 
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    [Header("Move")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    [Header("Sounds")]
    [SerializeField] private AudioSource itemSpeed;
    [SerializeField] private AudioSource death;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); //lay tham chieu den thanh phan rigidbody (thanh phan de them tinh chat vat ly vao tro choi)
        activeSpriteRenderer = spriteRendererDown;
        AstarPath.active.Scan();
    }

    private void Update()
    {
        if (Input.GetKey(inputUp)) {
            SetDirection(Vector2.up, spriteRendererUp);
        } else if (Input.GetKey(inputDown)) {
            SetDirection(Vector2.down, spriteRendererDown);
        } else if (Input.GetKey(inputLeft)) {
            SetDirection(Vector2.left, spriteRendererLeft);
        } else if (Input.GetKey(inputRight)) {
            SetDirection(Vector2.right, spriteRendererRight);
        } else {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void FixedUpdate() //ham dung de di chuyen doi tuong trong tro choi bang cach su dung rigidbody va ham nay dam bao di chuyen on dinh va chinh xã trong moi khung hinh tro choi
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        
        rigidbody.MovePosition(position + translation); //mot ham trong lop Rigidbody trong unity , dung de di chuyen nhan vat den vi tri moi
    }
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer) {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp; //Kiem tra xem spriteRenderer co dang cung tro toi cung mot doi tuong voi spriteRenderUp hay khong. Neu dung thi spriteRenderUp se duong kich hoat (enable), nguoc lai se bi tat (disable)
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown; // (spriteRender == spriteRenderDown) la phan so sanh con spriteRenderDown = la gan gia tri la true hoach false
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
        

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero; //kiem tra neu huong la zero thi gan idle = true va nguoc lai tuc la neu khong nhan phim nao thi nhan vat se dung yen
    }

    private void OnTriggerEnter2D(Collider2D other) //neu va cham voi lop explosion
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion") || other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            death.Play();
            DeathSequence();
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                GetComponent<Collider2D>().isTrigger = true;
                DeathSequence();
            }
    }
    public void DeathSequence(){ //tat moi chuc nang 
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f); //dung de thuc hien 1 ham sau 1 khoang thoi gian nhat dinh
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState(); //Tim kiem doi tuong cua GameManager , va check xem no con hoat dong hay khong
    }

    public void AddSpeed()
    {
        itemSpeed.Play();
        speed++;
    }
}
