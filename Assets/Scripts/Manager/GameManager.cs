using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public List<Player> curPlayerGroup = new List<Player>();

    public List<Player> playerGroup_1 = new List<Player>();
    public List<Player> playerGroup_2 = new List<Player>();
    public List<Player> playerGroup_3 = new List<Player>();



    private void Awake() {
        Instance = this;
    }
}
