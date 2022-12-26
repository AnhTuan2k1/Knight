using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public string[] gun;
    public string[] sword;
    private Player player;

    public PlayerData(Player player)
    {
        this.player = player;
    }
}
