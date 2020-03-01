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

    public int gravityDirectionIndex = 0;
    const float gravityStrength = 9.81f;
    public Vector3[] GravityDirections = {gravityStrength * Vector3.down, gravityStrength * Vector3.left, gravityStrength * Vector3.up, gravityStrength * Vector3.right};

    private Vector3[] directions = new Vector3[]{ new Vector3(1,0,0),
                        new Vector3(-1,0,0),
                        new Vector3(0,-1,0),
                        new Vector3(0,1,0),
                        new Vector3(0,0,1),
                        new Vector3(0,0,-1)};

    void Start()
    { 
        //ChangeMode(ModeController.Mode.flummy)
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = useGravitationInBounce;
        //var collider = GetComponent<SphereCollider>();
        //collider.material.bounciness = 1;
        //rb.AddForce((transform.right + transform.up * 5) * Time.fixedDeltaTime * 100, ForceMode.Impulse);
        // Moves the GameObject using it's transform.
        //rb.isKinematic = true;
    }

    void Update(){
        if(Input.GetButtonDown("Jump")){
            //isJumping = Input.GetButtonDown("Jump");
            gravityDirectionIndex += 1;
            gravityDirectionIndex = gravityDirectionIndex % GravityDirections.Length;
            Debug.Log(gravityDirectionIndex);
            //Physics.gravity = GravityDirections[gravityDirectionIndex];
            Debug.Log(GravityDirections[gravityDirectionIndex]);          
        }
        //Physics.Raycast(transform.position,)
    }

    void FixedUpdate()
    {
        if (currentMode == ModeController.Mode.normal)
        {
            //rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
            if (rb.velocity.x < 1)
            {
                rb.velocity += new Vector3(.1f, 0, 0);
            }
        }
        //Debug.Log(isColliding);
        //rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
        //if(isJumping){
        //isJumping=false;
        //if(isColliding){
        //    rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
        //}
        //} 
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
        //Debug.Log("average contact direction" + this.collisionDirection);

        if(currentMode == ModeController.Mode.sticky){
            Debug.Log("gravity of");
            GetComponent<Rigidbody>().useGravity = false;
            var newVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity = newVelocity;
            //rb.AddForce(collisionDirection, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision other){
        //Debug.Log("exited");
        isColliding = false;
        GetComponent<Rigidbody>().useGravity = useGravitationInBounce;
    }

}