using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public const float rayDistance = 30f;
    GroupCommand<Player> playerGroup1 = new GroupCommand<Player>();
    GroupCommand<Player> playerGroup2 = new GroupCommand<Player>();


    Camera cam;

    private void Awake() {
        cam = Camera.main;
    }

    RaycastHit2D MousePositionRaycast(LayerMask layerMask)
    {
        return Physics2D.Raycast( cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, rayDistance, layerMask);
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {   
            RaycastHit2D hit = MousePositionRaycast(LayerMask.GetMask("Room"));
        
            if (hit)
            {
                Room room = hit.transform.GetComponent<Room>();

                foreach(Player p in GameManager.Instance.curPlayerGroup)
                {
                    p.OnMoveCommand(room);
                }
            }
            
        }

        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = MousePositionRaycast(LayerMask.GetMask("Player"));

            if (hit)
            {
                Player player = hit.transform.GetComponent<Player>();
                
                // TODO 수정 //

                if (!Input.GetKey(KeyCode.LeftAlt))
                {   
                    GameManager.Instance.curPlayerGroup.Clear();
                }
                
                player.Selected();
            }
        }


        else if (Input.GetKey(KeyCode.Alpha1)) 
        { 
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerGroup1.SetGroup(GameManager.Instance.curPlayerGroup);    //
            }
            else{
                playerGroup1.Execute();
            } 
        }


        else if (Input.GetKey(KeyCode.Alpha2)) 
        { 
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerGroup2.SetGroup(GameManager.Instance.curPlayerGroup);    //
            }
            else{
                playerGroup2.Execute();
            } 
        }


        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.curPlayerGroup.Clear();
        }
        
    }
}