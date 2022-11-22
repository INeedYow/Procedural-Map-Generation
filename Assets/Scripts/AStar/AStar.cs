 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ASInfo
{
    static readonly Color[] pathColors = new Color[3]{
      Color.white, new Color(1f, 0.25f, 0.25f, 1f), Color.red 
    };

    public int  costF = AStar.inf;
    public int  costG = 0;
    public int  costH = AStar.inf;

    public Room parent = null;
    public int pathCount = 0;
    public Color PathColor {
        get {
            //pathCount = Mathf.Clamp(pathCount, 0, pathColors.Length);
            return pathColors[pathCount];
        }
    }

    public void ResetCost()
    {
        costG = 0;
        costH = AStar.inf;
        costF = AStar.inf;
    }

    public void SetInfo(int g, int h, Room parent)
    {
        costG = g;
        costH = h;
        costF = g + h;
        this.parent = parent;
    }
}



public class AStar
{
    public const int inf = 100000;

    public LinkedList<Room> GetPath(Room start, Room destination)
    {
        LinkedList<Room> paths        = new LinkedList<Room>();

        LinkedList<Room> openRooms    = new LinkedList<Room>();
        LinkedList<Room> closeRooms   = new LinkedList<Room>();
        Room minCostRoom;


        start.asInfo.SetInfo(
            0, 
            CalculateCostH(start, destination), 
            null);

        openRooms.AddLast(start);


        while (true)
        {   //Debug.Log(string.Format("openRooms.Count : {0}", openRooms.Count));
            if (openRooms.Count == 0)
            {   
                Debug.Log("길을 찾을 수 없음");
                break;
            }


            minCostRoom = GetMinCostRoom(openRooms);   
            //Debug.Log(string.Format("minCostRoom : {0}, pos : ({1},{2}), f : {3}, g : {4}, h : {5}", 
            //    minCostRoom, minCostRoom.PositionX, minCostRoom.PositionY, minCostRoom.asInfo.costF, minCostRoom.asInfo.costG, minCostRoom.asInfo.costH));



            closeRooms.AddLast(minCostRoom);
            openRooms.Remove(minCostRoom);


            if (minCostRoom == destination)
            {
                Room pathRoom = destination;

                while (pathRoom != null)
                {
                    paths.AddFirst(pathRoom);

                    pathRoom = pathRoom.asInfo.parent;
                }

                break;
            }


            int newG, newH, newF;
            Room currentRoom;
            bool isHorizontal;


            for (int i = 0; i < minCostRoom.nextRooms.Length; i++)
            {
                currentRoom = minCostRoom.nextRooms[i];

                if (currentRoom == null || closeRooms.Contains(currentRoom))
                {   // 방이 없는 경우 or 이미 확인한 방인 경우
                    continue;
                }

                currentRoom.asInfo.ResetCost();


                // EDirection .Up .Down .Left .Right    
                isHorizontal = i >= 2;

                newG = CalculateCostG(minCostRoom, currentRoom, isHorizontal);
                newH = CalculateCostH(currentRoom, destination);
                newF = newG + newH;

                //Debug.Log(string.Format("코스트 갱신 시도 - {0}, pos : ({1},{2}), f {3} g {4} h {5}", Direction.eDirections[i], currentRoom.PositionX, currentRoom.PositionY, newF, newG, newH));

                
                if (currentRoom.asInfo.costF > newF)
                {   //Debug.Log(string.Format("코스트 갱신 됨"));
                    currentRoom.asInfo.SetInfo(newG, newH, minCostRoom);
                }


                if (!openRooms.Contains(currentRoom))
                {   
                    openRooms.AddLast(currentRoom);
                }
                
            }
        }

        return paths;
    }


    Room GetMinCostRoom(LinkedList<Room> rooms)
    {
        int minF = inf;

        Room minCostRoom = null;

        foreach (Room room in rooms)
        {
            if (room.asInfo.costF < minF)
            {
                minF = room.asInfo.costF;
                minCostRoom = room;
            }
        }

        return minCostRoom;
    }



    int CalculateCostG(Room a, Room b, bool isHorizontal)
    {
        if (isHorizontal)
        {   // 정수값으로 사용하기 위해 * 10, 길이의 절반 -> * 5
            return a.asInfo.costG + (int)((a.width + b.width) * 5) + (int)(Mathf.RoundToInt(MapGenerator.Instance.wallSize * 10));
        }
        else{
            return a.asInfo.costG + (int)((a.height + b.height) * 5) + (int)(Mathf.RoundToInt(MapGenerator.Instance.wallSize * 10));
        }
        
    }


    int CalculateCostH(Room start, Room destination)
    {
        int x = Mathf.Abs((int)((start.PositionX - destination.PositionX) * 10m));
        int y = Mathf.Abs((int)((start.PositionY - destination.PositionY) * 10m));
        
        return x + y;
    }
}
