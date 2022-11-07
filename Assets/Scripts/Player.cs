﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    LinkedList<Room> paths = new LinkedList<Room>();
    SpriteRenderer sr;
    BoxCollider2D coll;



    //temp 

    public Room dest;
    public float speed = 4f;
    public bool isMove = false;


    private void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dest != null)
                OnMoveCommand(dest);
        }
    }


    public void Selected()
    {
        GameManager.Instance.curPlayerGroup.Add(this);
    }

    public void DeSelected()
    {
        GameManager.Instance.curPlayerGroup.Remove(this);
    }


    public void SetColor(Color color)
    {
        sr.color = color;
    }

    public void OnMoveCommand(Room destination)
    {   //Debug.Log(string.Format("Move ({0}) -> ({1})", transform.position, destination.transform.position));
        AStar aStar = new AStar();

        if (isMove)
        {
            StopCoroutine(Move());
        }

        paths = aStar.GetPath(GetStartRoom(), destination);


        StartCoroutine(Move());

    }


    Room GetStartRoom()
    {
        Collider2D coll = Physics2D.OverlapBox(transform.position, Vector2.one, 0f, LayerMask.GetMask("Room"));        
        //Debug.Log(string.Format("coll : {0}", coll));

        Room room = coll.transform.GetComponent<Room>();

        if (room == null) Debug.Log("player.GetRoom null");

        return room;
        
    }


    IEnumerator Move()
    {
        isMove = true;

        LinkedListNode<Room> node = paths.First;
        Room room;

        while (node != null)
        {
            room = node.Value;

            if ((transform.position - room.transform.position).sqrMagnitude < 0.01f )
            {
                node = node.Next;
                continue;
            }
            else
            {
                transform.Translate((room.transform.position - transform.position).normalized * speed * Time.deltaTime);

                yield return null;
            }
        }

        isMove = false;
        yield return null;
        //Debug.Log("arrive");
    }

    
}