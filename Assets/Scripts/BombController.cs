using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; //Truy cap den tilemap cua unity

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f; // thoi gian qua bom phat no
    public int bombAmount = 1; //so bom toi da ma nguoi choi co the su dung cung mot luc
    private int bombsRemaining = 0; //so bom con lai

    [Header("Explosion")] //Tao tieu de de de nhin hon trong giao dien cua unity
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask; //su dung de chi dinh cac layer ma vu no co the tac dong den
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles; //khoi tao mot doi tuong tren ban do de co the pha huy no khi no bi no
    public Destructible destructiblePrefab;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    
    public bool check = false;
    public int count = 0;
    public int max = 23;

    private void Update()
    {
        //check neu pha huy tile thi se kich hoat ham

        // if(check == true){
        //     count++;
        //     StartCoroutine(UpdateGragh());
        //     if(count >= max){
        //         check = false;
        //         count = 0;
        //     }
        // }
        CheckWallOnCollision();
        if(bombsRemaining > 0 && Input.GetKeyDown(inputKey)){
            StartCoroutine(PlaceBomb()); //dung de khoi dong Coroutine "PlaceBomb()" , giup coroutine se chay dong thoi voi cac hoat dong khac trong chuong trinh
        }
                    
       
    }

    private void CheckWallOnCollision(){
        
            if(check){
                count++;
                StartCoroutine(UpdateGragh());
                if(count >= max){
                   check = false;
                   count = 0;
                }
            
        }
    }
    private IEnumerator UpdateGragh(){
        AstarPath.active.Scan();
        yield return null;
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position; //lay vi tri hien tai cua GameObject , o day la player
        position.x = Mathf.Round(position.x); //lam tron de trung voi o luoi cua game
        position.y = Mathf.Round(position.y);

        //Tai vi tri nay thi tao 1 qua bom 
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);  // tao mot GameObject moi tu bombPrefab duoc thiet lap truoc do, doi so cuoi dai dien cho huong quay cua bom , trong truong hop nay la mac dinh va khong quay
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime); //trong thoi gian bom no coroutine se tam dung va chay lai khi thoi gian cho ket thuc

        position = bomb.transform.position; //tra ve vi tri cua qua bom vi luc nay qua bom co the da bi day ra khoi vi tri ban dau
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);


        Destroy(bomb);
        bombsRemaining++;

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        //neu sau khi dat bomb ma player va cham voi bomb thi thuoc tinh isTrigger se thanh false tuc la se co tac dong vat ly giua player va bomb
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
        
    }
    
    //
    

    private void Explode(Vector2 position, Vector2 direction, int length) //dung de tao cac vu no xung quanh vi tri dat bom
    {
        if(length <= 0) { //neu het chieu dai thi se khong no nua
            return;
        }

        position += direction;

        //Ham trong if tra ve 1 Collider2D, la collider dau tien duoc tim thay trong hinh chu nhat phu len hinh anh duoc  chi dinh
        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)){
            
            ClearDestructible(position);         
            return; //tuc la neu va cham phai mot doi tuong ma khong the xuyen qua thi vu no se khong hien thi o huong nay
        } //kiem tra xem co doi tuong nao nam trong hinh hop vuong (box) tai vi tri "position" va co kich thuoc la "Vector2.one / 2f" hay khong, chi dinh hop va cham chi phan ung voi cac doi tuong nao
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); //neu chieu dai > 1 thi explosion se la mid con nguoc lai la end
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration); //Thay cho ham duoi cho tien su dung
        //Destroy(explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1); //goi de quy cho vu no den khi do dai = 0
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position); //tra ve vi tri bomb(x, y, z) 
        TileBase tile = destructibleTiles.GetTile(cell); //lay thong tin ve tile tai vi tri do, co the dung thong tin nay de xem tile do co phai la  o co the bi pha huy hay khong

        if(tile != null)
        {
            
            Instantiate(destructiblePrefab, position, Quaternion.identity); //neu co thanh phan co the bi pha huy thi khoi tao hinh anh tile vi pha vo
            destructibleTiles.SetTile(cell, null); //dat gia tri cho tile bi pha huy la null, bien mat khoi map
            check = true;
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
