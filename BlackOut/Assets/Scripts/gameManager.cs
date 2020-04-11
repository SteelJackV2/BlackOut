using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour{
    private const string PLAYERIDPREFIX = "Player ";
    private static Dictionary<string, playerManager> players = new Dictionary<string, playerManager>();

    public static void registerPlayer(string netId, playerManager player){
        string id = PLAYERIDPREFIX + netId;
        players.Add(id, player);
        player.transform.name = id;
    }


    public static playerManager getPlayer(string playerId){
        return players[playerId];
    }

    public static void unRegisterPlayer(string playerId){
        players.Remove(playerId);
    }

    void OnGUI(){
        GUILayout.BeginArea(new Rect(10, 130, 200, 500));
        GUILayout.BeginVertical();

        foreach (string playerID in players.Keys){
            GUILayout.Label(playerID + "  -  Kills: " + players[playerID].getKill());
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
