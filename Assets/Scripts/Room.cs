using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static int totalRoomCount = 0; 
    
    public string roomName;
    public float width;
    public float height;
    
    [Header("Up, Down, Left, Right")]
    public Room[] nextRooms = new Room[4];
    [Range(0f, 50f)] public float weight;


    public void SetScale()
    {
        width = transform.localScale.x;
        height = transform.localScale.y;
    }


    // private void OnMouseDown() {
    //     Debug.Log("Link Test");
    //     RoomChecker.CheckAndLinkRoom(this, true);
    // }


    private void OnDrawGizmos() {
        foreach(Room room in nextRooms)
        {
            if (room == null) continue;
            Gizmos.DrawLine(transform.position, room.transform.position);
        }
    }


    public void SetColor(Color color)
    {
        SpriteRenderer sp = transform.GetComponent<SpriteRenderer>();

        if (sp == null) return;

        sp.color = color;
    }

}
