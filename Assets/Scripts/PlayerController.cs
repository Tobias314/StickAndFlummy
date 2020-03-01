using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isJumping;
    public bool isColliding;
    public Vector3 collisionDirection;
    public bool useGravitationInBounce = true;
    public ModeController.Mode currentMode = ModeController.Mode.flummy;

    private Vector3[] directions = new Vector3[]{ new Vector3(1,0,0),
                        new Vector3(-1,0,0),
                        new Vector3(0,-1,0),
                        new Vector3(0,1,0),
                        new Vector3(0,0,1),
                        new Vector3(0,0,-1)};


    public void ChangeMode(ModeController.Mode newMode){
        Debug.Log("switched mode to " + newMode);
        var collider = GetComponent<SphereCollider>();
        if(newMode == ModeController.Mode.flummy){
            collider.material.bounciness = 1;
            if(isColliding){
                rb.AddForce((collisionDirection * 10) * Time.fixedDeltaTime * 50, ForceMode.Impulse);
            }
            rb.useGravity = useGravitationInBounce;
        }else if(newMode == ModeController.Mode.sticky){
            collider.material.bounciness = 0;
            rb.useGravity = true;
        }
        currentMode = newMode;
    }

    void Start()
    { 
        //ChangeMode(ModeController.Mode.flummy)
        rb = GetComponent<Rigidbody>();
        rb.useGravity = useGravitationInBounce;
        var collider = GetComponent<SphereCollider>();
        collider.material.bounciness = 1;
        rb.AddForce((transform.right + transform.up * 5) * Time.fixedDeltaTime * 100, ForceMode.Impulse);
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
        if(currentMode == ModeController.Mode.sticky){
            if(isColliding){
                rb.AddForce((-collisionDirection) * Time.fixedDeltaTime * 100, ForceMode.Impulse);
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