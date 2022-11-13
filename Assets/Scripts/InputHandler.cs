using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public const float rayDistance = 30f;
    public const float camSpeed = 5f;



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
        if (GameManager.Instance.IsPaused)
            return;



        if (Input.GetMouseButtonDown(1))
        {   
            RaycastHit2D hit = MousePositionRaycast(LayerMask.GetMask("Room"));
        
            if (hit)
            {
                Room room = hit.transform.GetComponent<Room>();

                GameManager.Instance.MoveCommand(room);
            }
            
        }

        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = MousePositionRaycast(LayerMask.GetMask("Player"));

            if (hit)
            {
                Player player = hit.transform.GetComponent<Player>();

                if (Input.GetKey(KeyCode.LeftAlt))
                {   
                    GameManager.Instance.ClickWithAlt(player);
                }
                else{
                    GameManager.Instance.ClickWithoutAlt(player);
                }
            }
        }



        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ClearCurrentGroup();
        }


        
        else if (Input.GetKey(KeySetting.keys[KeyAction.HERO_GROUP_1]))
        {
            SaveOrLoadGroup(0);
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.HERO_GROUP_2]))
        {
            SaveOrLoadGroup(1);
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.HERO_GROUP_3]))
        {
            SaveOrLoadGroup(2);
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.HERO_GROUP_4]))
        {
            SaveOrLoadGroup(3);
        }

        
        if (Input.GetKey(KeySetting.keys[KeyAction.MOVE_CAM_UP]))
        {
            cam.transform.Translate(Vector2.up * camSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.MOVE_CAM_DOWN]))
        {
            cam.transform.Translate(Vector2.down * camSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeySetting.keys[KeyAction.MOVE_CAM_LEFT]))
        {
            cam.transform.Translate(Vector2.left * camSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeySetting.keys[KeyAction.MOVE_CAM_RIGHT]))
        {
            cam.transform.Translate(Vector2.right * camSpeed * Time.deltaTime);
        }

        
    }

    public void SaveOrLoadGroup(int groupNum)
    {   
        if (Input.GetKey(KeyCode.LeftControl)) 
        {   
            GameManager.Instance.SaveGroup(groupNum);
        }
        else{
            GameManager.Instance.LoadGroup(groupNum);
        }
    }
}