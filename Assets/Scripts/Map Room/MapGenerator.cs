using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { private set; get; }

    [Header("Rooms")]
    public Room[] prfRooms;
    public int startRoomIndex = 0;




    [Space(10f)]

    [Header("Wall")]
    public GameObject prfWall;
    [Range(0.1f, 0.5f)] public float wallSize = 0.5f;


    [Space(10f)]

    [Header("Room Count")]
    [Range(2, 100)]     public int minCount;
    [Range(2, 100)]     public int maxCount;



    List<Generation> generations    = new List<Generation>();




    private void Awake() 
    {
        Instance = this;

        //ErrorCheck();
        Init();

        CreateRandomMap();
    }



    void ErrorCheck()
    {
        if (minCount > maxCount)
        {
            maxCount = minCount;
            Debug.Log("방 최대/최소 개수 설정 오류 -> 더 큰 개수 만큼 생성");
        }
    }


    void Init()
    {
        foreach (Room room in prfRooms)
        {
            room.SetScale();
        }

        prfWall.transform.localScale = new Vector2(wallSize, wallSize);

        minCount = PlayerPrefs.GetInt("min", 10);
        maxCount = PlayerPrefs.GetInt("max", 99);
    }


    void CreateRandomMap()
    {
        int goalCount = Random.Range(minCount, maxCount + 1);   Debug.Log(string.Format("goalCount : {0}", goalCount));
        
        CreateStartRoom();

        List<Room> newRooms = new List<Room>();
        UnDuplicatedRandomPick<Room> udrpRooms = new UnDuplicatedRandomPick<Room>();

        Room removeRoom;
        int index = 0;
        int removeCount;
        bool isFull = false;
        int loop = 0;


        while (!isFull)
        {
            newRooms.Clear();
            generations.Add(new Generation());
            

            for (int i = index; i >= 0; i--)
            {   // 해당 세대에서 방을 하나도 만들 수 없는 경우 이전 세대에서 만들도록
                newRooms.AddRange(generations[i].CreateNewRooms());

                if (newRooms.Count != 0)
                    break;
            }


            if (Room.totalRoomCount >= goalCount)
            {
                removeCount = Room.totalRoomCount - goalCount;
            }
            else{   // 최소 방 1개는 남기도록
                removeCount = Random.Range(0, newRooms.Count);  
            }
            //Debug.Log(string.Format("generation : {0} , newRooms : {1} , remove : {2}", index, newRooms.Count, removeCount));



            udrpRooms.SetItem(newRooms);

            for (int i = 0; i < removeCount; i++)
            {
                removeRoom = udrpRooms.GetItem();

                for (int j = 0 ; j < removeRoom.nextRooms.Length; j++)
                {
                    if (removeRoom.nextRooms[j] != null)
                    {
                        removeRoom.nextRooms[j].nextRooms[(int)Direction.GetReverseDirection(Direction.eDirections[j])] = null;
                    }
                }

                newRooms.Remove(removeRoom);
                Destroy(removeRoom.gameObject);
                Room.totalRoomCount--; //Debug.Log(string.Format("after remove roomCount = {0}", Room.totalRoomCount));
            }


            foreach (Room room in newRooms)
            {
                RoomChecker.CheckAndLinkRoom(room);
            }

            generations[++index].AddRoom(newRooms);
            //Debug.Log(string.Format("total : {0}", Room.totalRoomCount));

            isFull = goalCount == Room.totalRoomCount;
            


            if (loop++ > 100)
            {
                throw new System.Exception("Inf loop");
            }
        }

        GameManager.Instance.CreatePlayers();
    }




    void CreateStartRoom()
    {   
        generations.Add(new Generation());

        if (startRoomIndex >= prfRooms.Length)
        {
            startRoomIndex = prfRooms.Length - 1;
        }

        Room startRoom = GameObject.Instantiate(prfRooms[startRoomIndex], Vector3.zero, Quaternion.identity);
        
        Room.totalRoomCount++;

        generations[0].AddRoom(startRoom);
    }


    public void CreatePath(Room a, Room b, EDirection directionA2B)
    {
        Vector2 pos;

        if (directionA2B == EDirection.UP || directionA2B == EDirection.DOWN)
        {
            pos = (Vector2)a.transform.position + new Vector2(0f, Direction.yDir[(int)directionA2B] * (wallSize + a.height) * 0.5f);
        }
        else{
            pos = (Vector2)a.transform.position + new Vector2(Direction.xDir[(int)directionA2B] * (wallSize + a.width) * 0.5f, 0f);
        }

        GameObject door = GameObject.Instantiate(prfWall, pos, Quaternion.identity);

        // parentRoom -> childRoom의 통로를 childRoom의 자식 오브젝트로 해서 childRoom이 지워질 때 통로도 지워지도록
        door.transform.SetParent(b.transform);  
    }

} 
