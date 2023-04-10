using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructibleTime = 1f;

    [Range(0f, 1f)] //xac dinh xac suat nam trong khoang (0, 1)
    public float itemSpawnChance = 0.2f; //dung de xac dinh xac suat xuat hien cua 1 item khi 1 destructible bi pha huy
    public GameObject[] spawnableItems;

    private void Start() //sau khi duoc khoi tao khi bi no, thi game object nay se tu bi huy trong 1 thoi gian la destructibleTime;
    {

        Destroy(gameObject, destructibleTime);
        
    }

    private void OnDestroy() //ham nay se tu dong duoc goi khi thuc hien ham Destroy
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance) //neu so luong item > 0 va gia tri random nho hon xac suat da quy dinh thì thuc hien
        {
            int randomIndex = Random.Range(0, spawnableItems.Length); //tao mot gia tri index ngau nhien la chi so cua item trong mang item
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);//khoi tao item do tai vi tri bi pha huy
           
        }
    }
}
