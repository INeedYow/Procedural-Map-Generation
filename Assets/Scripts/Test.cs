using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Test : MonoBehaviour
{
    public Room start;
    public Room destination;


    LinkedList<Room> list = new LinkedList<Room>();

    bool isDone = false;



    private void Update() 
    {
        // if (Input.anyKeyDown)
        // {
        //     Debug.Log(string.Format("KeyDown : {0}", Input.inputString));
        // }
        


        //Debug.Log(string.Format("Input.inputString : {0}", Input.inputString));


        // if (start != null && destination != null)
        // {
        //     if (!isDone)
        //     {
        //         isDone = true;
        //         AStar astar = new AStar();
        //         list = astar.GetPath(start, destination);

        //         foreach(Room r in list)
        //         {
        //             //Debug.Log(string.Format("Path f : {0}, g : {1}, h : {2}", r.asInfo.costF, r.asInfo.costG, r.asInfo.costH));
        //             r.SetColor(Color.red);
        //         }
        //     }
        // }    
        // else if (isDone)
        // {
        //     isDone = false;

        //     foreach(Room r in list)
        //     {
        //         r.SetColor(Color.white);
        //     }
        //     list.Clear();
        // }
    }

    private void OnGUI() 
    {
        // if ( GUI.Button (new Rect (10, 10, 150, 100), "나는 버튼입니다"))
        //     print("버튼을 클릭하셨습니다!");

        
    }
}
