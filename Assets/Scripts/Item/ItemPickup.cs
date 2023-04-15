using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType //dung de dinh nghia cac loai item trong game;
    {
        ExtraBomb, 
        BlastRadius, 
        SpeedIncrease,
    }

    public ItemType type; //giup user lua chon loai ItemType;

    private void OnItemPickup(GameObject player) //vat pham ma player nhat duoc
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().AddExplosionRadius();
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().AddSpeed();
                break;
        }

        Destroy(gameObject); //neu bi nhat thi se bien mat
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) { //so sanh neu vat the va cham cung tag (the) voi player thi kich hoat cau lenh nay
            OnItemPickup(other.gameObject);
        }
    }
}
