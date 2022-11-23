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


    public ASInfo asInfo = new ASInfo();
    SpriteRenderer sp;

    public decimal PositionX {
        get { return (decimal)(Mathf.RoundToInt(transform.position.x * 10f)) * 0.1m; }
    }
    public decimal PositionY {
        get { return (decimal)(Mathf.RoundToInt(transform.position.y * 10f)) * 0.1m; }
    }


    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
    }

    public void SetScale()
    {
        width = transform.localScale.x;
        height = transform.localScale.y;
    }


    private void OnDrawGizmos() {
        foreach(Room room in nextRooms)
        {
            if (room == null) continue;
            Gizmos.DrawLine(transform.position, room.transform.position);
        }
    }



    public void OnPathRoom(bool isOnPath)
    {
        if (isOnPath)
        {
            asInfo.pathCount++;
        }
        else{
            asInfo.pathCount--;
        }
        
        SetColor(asInfo.PathColor);
    }

    public void SetColor(Color color)
    {
        sp.color = color;
    }
}
