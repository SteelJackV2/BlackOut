using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    public Animator move;
    public float forward;
    public float sideways;
    public Transform location;
    float moveVertical, moveHorizontal, speed;
    bool fire, aim, run, jump, heavyWeapon;
    Rigidbody body;

    // Start is called before the first frame update
    void Start() { 
        Cursor.lockState = CursorLockMode.Locked;
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        fire = Input.GetButton("Fire1");
        aim = Input.GetButton("Aim");
        run = Input.GetButton("Run");
        jump = Input.GetButton("Jump");


        

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Crate"))
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("Swaped");
                heavyWeapon = true;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Swaped");
                heavyWeapon = false;
            }
        }
    }
    private void FixedUpdate(){
        move.SetInteger("walk", (int)moveVertical);
        move.SetInteger("Side", (int)moveHorizontal);
        move.SetBool("aim", aim);
        move.SetBool("run", run);
        move.SetBool("fire", fire);
        move.SetTrigger("Trigger");
        
        if (jump)
        {
            body.AddForce(Vector3.up * 5f);
        }

        if (!fire){
            if (moveVertical >= 1f || moveVertical <= 1f){
                speed = forward;
                if (run && moveVertical > 0){
                    speed = 4f*forward;
                }
                transform.position += transform.forward * Time.deltaTime * (speed * moveVertical);
            }

            if (moveHorizontal >= 1f || moveHorizontal <= -1)
                transform.position += transform.right * Time.deltaTime * (sideways * moveHorizontal);
        }
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
}
