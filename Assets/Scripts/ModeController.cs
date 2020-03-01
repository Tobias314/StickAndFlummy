using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    public enum Mode
    {
        sticky, flummy
    }
    public PlayerController player;

    public Mode currentMode;
    public Light sunlight;

    public Color stickyColor;
    public Color flummyColor;

    private Vector3 flummyJumpSpeed = new Vector3(1,2,0) * 5;

    public void Start(){
        currentMode = Mode.flummy;
        //EnterFlummyMode();
        player.rb.AddForce(flummyJumpSpeed, ForceMode.Impulse);    
        sunlight.color = flummyColor;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            LeaveCurrentMode();
            currentMode = Mode.sticky;
            EnterStickyMode();
        }
         if(Input.GetKeyDown(KeyCode.F)){
            LeaveCurrentMode();
            currentMode = Mode.flummy;
            EnterFlummyMode();
        }

        player.currentMode = currentMode;
    }

    void LeaveCurrentMode(){
        return;
    }
    void EnterStickyMode(){
        sunlight.color = stickyColor;
        player.GetComponent<SphereCollider>().material.bounciness = 0;
    }
    void EnterFlummyMode(){
        sunlight.color = flummyColor;
        player.GetComponent<SphereCollider>().material.bounciness = 1;
        if(player.isColliding){
            //player.rb.AddForce((Mathf.Abs(player.collisionDirection.x) * player.collisionDirection) + ((1.0f - Mathf.Abs(player.collisionDirection.x)) * Vector3.up) * 10, ForceMode.Impulse);
            //player.rb.velocity = ;
            //player.rb.AddForce( - player.collisionDirection * 10 + Vector3.up * 3, ForceMode.Impulse);        
        }
            player.rb.useGravity = player.useGravitationInBounce;
    }

}
