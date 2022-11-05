using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation
{
    List<Room> rooms        = new List<Room>();
    UnDuplicatedRandomPick<Room> udrpRoom = new UnDuplicatedRandomPick<Room>();

 
    public void AddRoom(Room newRoom)
    {
        rooms.Add(newRoom);
    }

    public void AddRoom(List<Room> newRooms)
    {
        rooms.AddRange(newRooms);
    }


    public List<Room> CreateNewRooms()
    {
        List<Room> newRooms = new List<Room>();
        List<BuildInfo> infos = new List<BuildInfo>();
        UnDuplicatedRandomPick<EDirection> udrpEDir = new UnDuplicatedRandomPick<EDirection>();
        WeightedRandomPick<BuildInfo> wrpInfo = new WeightedRandomPick<BuildInfo>();

        Room randomRoom;
        EDirection eDirection;

        udrpRoom.SetItem(rooms);

        while (!udrpRoom.IsEmpty())
        {    
            randomRoom = udrpRoom.GetItem();
            
            udrpEDir.SetItem(GetEmptyDirectionList(randomRoom));

            
            while(!udrpEDir.IsEmpty())
            {
                eDirection = udrpEDir.GetItem();

                infos.Clear();
                infos.AddRange(RoomChecker.GetCanBuildInfoList(randomRoom, eDirection));

                if (infos.Count == 0)
                    continue;
                

                wrpInfo.Clear();
                foreach(BuildInfo info in infos)
                {
                    wrpInfo.Add(info, info.prfRoom.weight);
                }

                BuildInfo randomInfo = wrpInfo.GetItem();
                
                Room newRoom = GameObject.Instantiate(randomInfo.prfRoom, randomInfo.position, Quaternion.identity);
                Room.totalRoomCount++; //Debug.Log(string.Format("roomCount = {0}", Room.totalRoomCount));

                randomRoom.nextRooms[(int)eDirection] = newRoom;
                newRoom.nextRooms[(int)Direction.GetReverseDirection(eDirection)] = randomRoom;

                newRooms.Add(newRoom);
                
            }
            
        }
        

        return newRooms;
    }


    List<EDirection> GetEmptyDirectionList(Room room)
    {
        List<EDirection> list = new List<EDirection>();

        for (int i = 0; i < Direction.eDirections.Length; i++)
        {
            if (room.nextRooms[(int)Direction.eDirections[i]] == null)
            {
                list.Add(Direction.eDirections[i]);
            }
        }

        return list;
    }

}
