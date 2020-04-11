using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour{

    public float timer = 0f;

    public float force;
    Rigidbody rb;

    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = force * gameObject.transform.forward;
    }

    void Update(){
        timer++;

        if (timer >= 200){
            Destroy();
        }
    }

    void Destroy(){
        gameObject.SetActive(false);
        NetworkServer.Destroy(gameObject);
        timer = 0;
    }

    private void OnTriggerEnter(Collider other){
        Destroy();
    }
}
