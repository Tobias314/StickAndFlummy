using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isJumping;


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
        
    }

    void FixedUpdate()
    {
        Debug.Log(isJumping);
        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
        if(isJumping){
            rb.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
            isJumping=false;
        }
    }
}