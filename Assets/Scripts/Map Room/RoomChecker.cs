using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection { UP, DOWN, LEFT, RIGHT, }

public static class Direction
{
    public static EDirection[] eDirections = { EDirection.UP, EDirection.DOWN, EDirection.LEFT, EDirection.RIGHT };
    public static int[] xDir = { 0, 0, -1, 1 };
    public static int[] yDir = { 1, -1, 0, 0 };

    public static EDirection GetReverseDirection(EDirection eDirection)
    {
        switch (eDirection)
        {
            case EDirection.UP      : return EDirection.DOWN;
            case EDirection.DOWN    : return EDirection.UP;
            case EDirection.LEFT    : return EDirection.RIGHT;
            default                 : return EDirection.LEFT;
        }
    }
}



public struct BuildInfo
{
    public Room prfRoom;
    public Vector2 position;


    public BuildInfo(Room prfRoom, Room parent, EDirection eDir)
    {
        this.prfRoom = prfRoom;
        position = Vector2.zero;
        
        position = GetPosition(parent, eDir);
    }
    
    Vector2 GetPosition(Room parent, EDirection eDir)
    {
        //Debug.Log(string.Format("parent pos : {0}, parent width : {1}, parent height : {2}, my width : {3}, my height : {4}, Dir : {5}", new Vector2(parent.transform.position.x, parent.transform.position.y), parent.width, parent.height, prfRoom.width, prfRoom.height, eDir));
        return new Vector2(parent.transform.position.x + ((parent.width + prfRoom.width) * 0.5f + MapGenerator.Instance.wallSize) * Direction.xDir[(int)eDir], 
                        parent.transform.position.y + ((parent.height + prfRoom.height) * 0.5f + MapGenerator.Instance.wallSize) * Direction.yDir[(int)eDir]); 
    }
}




public class RoomChecker
{

    public static bool IsNextPosition(Room a, Room b)
    {   // 
        decimal a_x = (decimal)(Mathf.Round(a.transform.position.x * 10f)) * 0.1m;
        decimal a_y = (decimal)(Mathf.Round(a.transform.position.y * 10f)) * 0.1m;
        decimal b_x = (decimal)(Mathf.Round(b.transform.position.x * 10f)) * 0.1m;
        decimal b_y = (decimal)(Mathf.Round(b.transform.position.y * 10f)) * 0.1m;


        if (a_x == b_x)
        {
            decimal dist = a_y > b_y ? a_y - b_y : b_y - a_y;
            decimal value = (decimal)(a.height + b.height) * 0.5m + (decimal)MapGenerator.Instance.wallSize;
            
            return dist == value;
        }
        else if (a_y == b_y)
        {
            decimal dist = a_x > b_x ? a_x - b_x : b_x - a_x;
            decimal value = (decimal)(a.width + b.width) * 0.5m + (decimal)MapGenerator.Instance.wallSize;

            return dist == value;
        }

        return false;
    }


    public static bool CheckRoomCollision(Room room, Vector2 position)
    {
        Collider2D[] colls = new Collider2D[2];
        int collisionCount = Physics2D.OverlapBoxNonAlloc(position, new Vector2(room.width + MapGenerator.Instance.wallSize * 2f, room.height + MapGenerator.Instance.wallSize * 2f), 0f, colls, LayerMask.GetMask("Room"));

        return collisionCount == 1;
    }
    


    public static List<BuildInfo> GetCanBuildInfoList(Room parentRoom, EDirection eDirection)
    {
        List<BuildInfo> list = new List<BuildInfo>(); 
        Vector2 position;

        foreach(Room room in MapGenerator.Instance.prfRooms)
        {
            if (room.weight <= 0f) 
                continue;

            switch (eDirection)
            {
                case EDirection.LEFT:
                case EDirection.RIGHT:
                {
                    position = new Vector2(parentRoom.transform.position.x + ((parentRoom.width + room.width) * 0.5f + MapGenerator.Instance.wallSize) * Direction.xDir[(int)eDirection], parentRoom.transform.position.y);
                    break;
                }
                default:
                {
                    position = new Vector2(parentRoom.transform.position.x, parentRoom.transform.position.y + ((parentRoom.height + room.height) * 0.5f + MapGenerator.Instance.wallSize ) * Direction.yDir[(int)eDirection]);
                    break;
                }
            }
            
            if (CheckRoomCollision(room, position))
            {
                list.Add(new BuildInfo(room, parentRoom, eDirection));
            }
        }

        return list;
    }



    public static void CheckAndLinkRoom(Room room)
    {
        Vector2 pos;

        foreach (EDirection eDir in Direction.eDirections)
        {
            if (room.nextRooms[(int)eDir] != null)
                continue;
            

            if (eDir == EDirection.UP || eDir == EDirection.DOWN)
            {
                pos = (Vector2)room.transform.position + new Vector2(0, Direction.yDir[(int)eDir] * (room.height * 0.5f + MapGenerator.Instance.wallSize));
            }
            else{
                pos = (Vector2)room.transform.position + new Vector2(Direction.xDir[(int)eDir] * (room.width * 0.5f + MapGenerator.Instance.wallSize), 0);
            }
            

            RaycastHit2D hit = Physics2D.Raycast(pos, new Vector2(Direction.xDir[(int)eDir], Direction.yDir[(int)eDir]), 2f);

            if (!hit) 
            {
                continue;
            }


            Room tempRoom = hit.transform.GetComponent<Room>();

            if (tempRoom == null) 
                continue;

            //Debug.Log(string.Format("room : {0}/{1} , tempRoom : {2}/{3}", room, room.transform.position, tempRoom, tempRoom.transform.position));


            if (IsNextPosition(room, tempRoom))
            {   //Debug.Log(string.Format("Link {0} with {1} // {2}", room.transform.position, tempRoom.transform.position, eDir));
                room.nextRooms[(int)eDir] = tempRoom;
                tempRoom.nextRooms[(int)Direction.GetReverseDirection(eDir)] = room;

                LinkRoom(room, tempRoom, eDir);
            }
        }
    }

    public static void LinkRoom(Room a, Room b, EDirection directionA2B)
    {
        a.nextRooms[(int)directionA2B] = b;
        b.nextRooms[(int)Direction.GetReverseDirection(directionA2B)] = a;

        // TODO 길목 생성
        MapGenerator.Instance.CreateDoor(a, directionA2B);
    }
}
