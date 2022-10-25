using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { private set; get; }

    public Room prfRoomA;
    public Room prfRoomB;
    public Room prfRoomC;
    public Room prfRoomD;
    public Room prfRoomE;



    [Space(10f)]

    [Header("Room Count")]
    [Range(10, 100)] public int minCount;
    [Range(10, 100)] public int maxCount;
    int roomCount;

    [Space(10f)]

    [Header("Room Weight")]
    [Range(0f, 50f)] public int weight_roomA;
    [Range(0f, 50f)] public int weight_roomB;
    [Range(0f, 50f)] public int weight_roomC;
    [Range(0f, 50f)] public int weight_roomD;
    [Range(0f, 50f)] public int weight_roomE;


    WeightedRandomPick<Room> wrp    = new WeightedRandomPick<Room>();
    Queue<Room> qRooms              = new Queue<Room>();

    private void Awake() 
    {
        Instance = this;

        ErrorCheck();
        //Init();
    }


    // void Init()
    // {
    //     wrp.Add(prfRoomA, weight_roomA);
    //     wrp.Add(prfRoomB, weight_roomB);
    //     wrp.Add(prfRoomC, weight_roomC);
    //     wrp.Add(prfRoomD, weight_roomD);
    //     wrp.Add(prfRoomE, weight_roomE);
        

    //     int countA = 0;
    //     int countB = 0;
    //     int countC = 0;
    //     int countD = 0;
    //     int countE = 0;

    //     for (int i = 0; i < 300; i++)
    //     {
    //         switch (wrp.GetItem().roomName)
    //         {
    //             case "A": GameObject.Instantiate(prfRoomA, GetRandomPosition(1f), Quaternion.identity); countA++; break;
    //             case "B": GameObject.Instantiate(prfRoomB, GetRandomPosition(1f), Quaternion.identity); countB++; break;
    //             case "C": GameObject.Instantiate(prfRoomC, GetRandomPosition(1f), Quaternion.identity); countC++; break;
    //             case "D": GameObject.Instantiate(prfRoomD, GetRandomPosition(1f), Quaternion.identity); countD++; break;
    //             case "E": GameObject.Instantiate(prfRoomE, GetRandomPosition(1f), Quaternion.identity); countE++; break;
    //         }
    //     }

    //     Debug.Log("A : B : C : D : E == " + countA + " : " + countB + " : " + countC + " : " + countD + " : " + countE);
    // }

    // Vector3 GetRandomPosition(float range)
    // {
    //     return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
    // }


    void ErrorCheck()
    {
        if (minCount > maxCount)
        {
            minCount = maxCount;
            Debug.Log("방 최대/최소 개수 설정 오류 -> 최대 개수 만큼 생성");
        }
    }


    public void CreateRandomMap()
    {
        // Room startRoom = GameObject.Instantiate(prfRoomA, Vector3.zero, Quaternion.identity);

        // qRooms.Enqueue(startRoom);

        // roomCount = Random.Range(minCount, maxCount + 1);

    }
} 
