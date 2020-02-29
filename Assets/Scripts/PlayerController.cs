using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isJumping;
    public bool isColliding;

    private Vector3[] directions = new Vector3[]{ new Vector3(1,0,0),
                        new Vector3(-1,0,0),
                        new Vector3(0,-1,0),
                        new Vector3(0,1,0),
                        new Vector3(0,0,1),
                        new Vector3(0,0,-1)};


    void Start()
    { 
        rb = GetComponent<Rigidbody>();

        // Moves the GameObject using it's transform.
        //rb.isKinematic = true;
    }

    void Update(){
        if(!isJumping){
            isJumping = Input.GetButtonDown("Jump");
            
        }
        //Physics.Raycast(transform.position,)
    }

    void FixedUpdate()
    {
        //Debug.Log(isJumping);
        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
        if(isJumping){
            isJumping=false;
            if(isColliding){
                rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        //Debug.Log("entered");
        isColliding = true;
        Debug.Log("number of contact points" + collision.contactCount);
        for(int i=0; i<collision.contactCount; i++){
            Debug.Log(collision.GetContact(i).point);
        }
    }

    private void OnCollisionExit(Collision other){
        //Debug.Log("exited");
        isColliding = false;
    }

}