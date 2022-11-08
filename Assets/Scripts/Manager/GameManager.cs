using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    LinkedList<Player> curGroup = new LinkedList<Player>();


    LinkedList<Player>[] savedGroups = new LinkedList<Player>[4];


    bool isPaused;
    public bool IsPaused {
        set { 
            isPaused = value; 
            if (isPaused) {
                Time.timeScale = 0f;
            }
            else {
                Time.timeScale = 1f;
            }
        }
        get { return isPaused; }
    }




    private void Awake() 
    {
        Instance = this;

        for (int i = 0; i < savedGroups.Length; i++)
        {
            savedGroups[i] = new LinkedList<Player>();
        }
    }


    public void AddPlayer(Player player)
    {
        if (!curGroup.Contains(player))
        {
            curGroup.AddLast(player);
            player.OnSelect();
        }
    }

    public void RemovePlayer(Player player)
    {
        if (curGroup.Remove(player))
        {
            player.OnDeselect();
        }
    }

    public void ClearCurrentGroup()
    {
        foreach(Player player in curGroup)
        {
            player.OnDeselect();
        }

        curGroup.Clear();
    }

    public void SaveGroup(int groupNum)
    {
        CopyGroup(savedGroups[groupNum], curGroup);
    }

    public void LoadGroup(int groupNum)
    {
        if (savedGroups[groupNum].Count == 0)
        {
            return;
        }

        foreach (Player player in curGroup)
        {
            player.OnDeselect();
        }

        CopyGroup(curGroup, savedGroups[groupNum]);

        foreach (Player player in curGroup)
        {
            player.OnSelect();
        }
    }

    void CopyGroup(LinkedList<Player> to, LinkedList<Player> from)
    {
        to.Clear();

        foreach(Player player in from)
        {
            to.AddLast(player);
        }
    }


    public void MoveCommand(Room destination)
    {
        foreach (Player player in curGroup)
        {
            player.OnMoveCommand(destination);
        }
    }

}
