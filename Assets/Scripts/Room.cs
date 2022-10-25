using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection {
    NONE, UP, DOWN, LEFT, RIGHT, 
}

public class Room : MonoBehaviour
{
    public static int roomCount = 0;
    public string roomName;


    // public BoxCollider2D coll;


    // private void Update() 
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         SetEnabled();
    //     }
    // }

    // public void SetEnabled()
    // {
    //     coll.enabled = true;
    // }

    public void CreateNeighborRoom(int count)
    {
        // 상하좌우 결정
        // 해당 위치에 방 모양 결정
            // 결정된 방이 설치될 수 있는지 판단
                // 불가능할 경우 설치를 포기할 것인지 다른 종류의 설치 가능한 방이 있다면 설치할 것인지 (의도결정)
        // 재귀(탈출조건으로 방 개수)
            // 방 개수가 부족한데 모든 방에서 새로운 방을 생성하지 않는 경우 판단 및 해결

        if (roomCount >= count)
        {
            return;
        }
    }
}
