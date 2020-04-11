using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class playerManager : NetworkBehaviour {
    public float respawntime = 3f;
    public int KillCount = 0;
    private int health = 500;
    public Text score;
    public Slider healthbar;
    public Animator animations;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private Collider[] collideOnDeath;
    private bool[] collwasEnabled;

    public void setup(){
        score.text = "My Kills: " + KillCount;
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++){
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        collwasEnabled = new bool[collideOnDeath.Length];
        for (int i = 0; i < collwasEnabled.Length; i++){
            collwasEnabled[i] = collideOnDeath[i].enabled;
        }
        setDefaults();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            RpcTakeDamage();
        }
    }

    [ClientRpc]
    public void RpcTakeDamage()
    {
        health -= 10;
        healthbar.value = health;

        if (health <= 0)
        {
            RpcDie();
            health = 500;
        }
    }


    [ClientRpc]
    public void RpcAddKill(){
        KillCount++;
        score.text = "My Kills: " + KillCount;
    }
    public int getKill(){
        return KillCount;
    }

    [ClientRpc]
    private void RpcDie(){
        animations.SetTrigger("Die");
        new WaitForSeconds(1f);
        for (int i = 0; i < disableOnDeath.Length; i++){
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < collideOnDeath.Length; i++){
           // collideOnDeath[i].enabled = false;
        }

        StartCoroutine(reSpawn());
    }


    private IEnumerator reSpawn(){
        yield return new WaitForSeconds(respawntime);
        setDefaults();

        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;

    }

    public void setDefaults() {
        for (int i = 0; i < disableOnDeath.Length; i++){
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        for (int i = 0; i < collideOnDeath.Length; i++){
           // collideOnDeath[i].enabled = collwasEnabled[i];
        }
        health = 500;
        healthbar.maxValue = 500;
        healthbar.value = health;
        animations.SetTrigger("respawn");
    }

    public void OnTriggerEnter(Collider other){
        string t = other.tag;
        t = t.Substring(0, 4);
        if (t == "Play"){
            string n = other.name;
            if (n.Substring(0, 7).Equals("missile"))
            {
                RpcDie();
                gameManager.getPlayer(other.tag).RpcAddKill();

            }
            else RpcTakeDamage();

            if (health <= 0)
            {
                Debug.Log(t + " is Dead");
                RpcDie();
                health = 500;
                gameManager.getPlayer(other.tag).RpcAddKill();
            }
        }
    }


}
