using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class Position
{
    public static Vector2[] offsets = new Vector2[]{ new Vector2(-0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 0.5f), new Vector2(0f, -0.5f)};
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public Player[] prfPlayers;

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


    public void CreatePlayers()
    {
        for (int i = 0; i < prfPlayers.Length; i++)
        {
            GameObject.Instantiate(prfPlayers[i], Vector2.zero + Position.offsets[i], Quaternion.identity);
        }
    }


    public void ClickWithAlt(Player player)
    {
        if (!RemovePlayer(player))
        {
            AddPlayer(player);
        }
    }

    public void ClickWithoutAlt(Player player)
    {
        ClearCurrentGroup();
        AddPlayer(player);
    }


    void AddPlayer(Player player)
    {
        curGroup.AddLast(player);
        player.OnSelect();
    }

    bool RemovePlayer(Player player)
    {
        if (curGroup.Remove(player))
        {
            player.OnDeselect();
            return true;
        }

        return false;
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
