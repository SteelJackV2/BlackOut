using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shooting : NetworkBehaviour{
    public GameObject Bullet;
    public GameObject Missile;
    public Transform spawnLocation;
    public Text dialog;
    public Text weapon;
    private bool heavyWeapon;
    string id;
    float startTime;
    bool starter = true;
    bool fire;

    void Start(){
        id = GetComponent<NetworkIdentity>().netId.ToString();
    }

    void Update(){
        fire = Input.GetButton("Fire1");
        if (fire && !heavyWeapon){
            CmdFire();
        }
        else if(heavyWeapon && Input.GetButtonDown("Fire1"))
        {
            CmdLaunch();
            Debug.Log(heavyWeapon);

        }
        if (heavyWeapon) weapon.text = "Missile";
        else weapon.text = "Assult Rifle";
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Crate")){
            if (heavyWeapon) dialog.text = "Press E for Assult Rifle";
            else dialog.text = "Press F for Missile";
            if (Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("Swaped");
                heavyWeapon = true;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                heavyWeapon = false;
            }
        }
        else dialog.text = "";

    }

    [Command]
    public void CmdFire(){
        if (heavyWeapon){
            id = GetComponent<NetworkIdentity>().netId.ToString();
            GameObject misile = Instantiate(Missile, spawnLocation.position, spawnLocation.rotation) as GameObject;
            misile.tag = "Player " + id;
            NetworkServer.Spawn(misile);
        }
        if(!heavyWeapon)
        {
            id = GetComponent<NetworkIdentity>().netId.ToString();
            GameObject body = Instantiate(Bullet, spawnLocation.position, spawnLocation.rotation) as GameObject;
            body.tag = "Player " + id;
            NetworkServer.Spawn(body);
        }
    }
    [Command]
    public void CmdLaunch()
    {
        id = GetComponent<NetworkIdentity>().netId.ToString();
        GameObject misile = Instantiate(Missile, spawnLocation.position, spawnLocation.rotation) as GameObject;
        misile.tag = "Player " + id;
        NetworkServer.Spawn(misile);
    }
}
