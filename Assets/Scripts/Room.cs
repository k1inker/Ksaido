using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Room : MonoBehaviour
{
    public bool isRoomClear = false;
    public CompositeCollider2D door;
    public TilemapRenderer doorVisible;
    // дверь закрывается если в нее стрелять
    public void OnTriggerExit2D(Collider2D collision)
    { 
        if (GameObject.Find("Player"))
        {
            if (!isRoomClear)
            {
                door.isTrigger = false;
                doorVisible.enabled = true;
            }
        }
    }
}
