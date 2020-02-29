using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isJumping;
    public bool isColliding;
    public Vector3 collisionDirection;

    private Vector3[] directions = new Vector3[]{ new Vector3(1,0,0),
                        new Vector3(-1,0,0),
                        new Vector3(0,-1,0),
                        new Vector3(0,1,0),
                        new Vector3(0,0,1),
                        new Vector3(0,0,-1)};


    public void ChangeMode(ModeController.Mode newMode){
        if(newMode == ModeController.Mode.flummy){
            
        }
    }

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
        Vector3 collisionDirection = new Vector3(0,0,0);
        for(int i=0; i<collision.contactCount; i++){
            collisionDirection += collision.GetContact(i).point;
        }
        this.collisionDirection = collisionDirection / collision.contactCount - transform.position;
        this.collisionDirection = Vector3.Normalize(this.collisionDirection);
        Debug.Log("average contact direction" + this.collisionDirection);
    }

    private void OnCollisionExit(Collision other){
        //Debug.Log("exited");
        isColliding = false;
    }

}