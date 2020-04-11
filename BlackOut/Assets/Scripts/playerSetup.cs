using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(playerManager))]
public class playerSetup : NetworkBehaviour{

    [SerializeField]
    Behaviour[] componentsToDisable;

    void Start(){
        disableComponents();
    }


    void disableComponents(){
        if (!isLocalPlayer){
            for (int x = 0; x < componentsToDisable.Length; x++) {
                componentsToDisable[x].enabled = false;
            }
        }
        GetComponent<playerManager>().setup();
    }

    public override void OnStartClient(){
        base.OnStartClient();
        string id = GetComponent<NetworkIdentity>().netId.ToString();

        gameManager.registerPlayer(id, GetComponent<playerManager>());
    }

    private void OnDisable(){
        gameManager.unRegisterPlayer(transform.name);

    }

}
