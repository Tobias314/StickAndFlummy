using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    public enum Mode
    {
        normal, sticky, flummy, ghost
    }
    public PlayerController player;

    public Light sunlight;

    public Color normalColor;
    public Color stickyColor;
    public Color flummyColor;
    public Color ghostColor;

    private Vector3 flummyJumpSpeed = new Vector3(1,2,0) * 5;

    private bool stickyIsHeld;
    private bool flummyIsHeld;

    public void Start(){
        //EnterFlummyMode();
        player.rb.velocity = flummyJumpSpeed;    
        sunlight.color = flummyColor;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            stickyIsHeld = true;
            UpdateMode();
        }
         if(Input.GetKeyDown(KeyCode.F)){
            flummyIsHeld = true;
            UpdateMode();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            stickyIsHeld = false;
            UpdateMode();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            flummyIsHeld = false;
            UpdateMode();
        }
    }

    void UpdateMode(){
        LeaveCurrentMode();
        if (!stickyIsHeld && !flummyIsHeld)
        {
            EnterNormalMode();
        }
        if (stickyIsHeld && !flummyIsHeld)
        {
            EnterStickyMode();
        }
        if (!stickyIsHeld && flummyIsHeld)
        {
            EnterFlummyMode();
        }
        if (stickyIsHeld && flummyIsHeld)
        {
            EnterGhostMode();
        }
    }

    void LeaveCurrentMode(){
        return;
    }

    void EnterNormalMode()
    {
        player.GetComponent<SphereCollider>().material.bounciness = 0; //this belong is LeaveCurrentMode()
        player.currentMode = Mode.normal;
        sunlight.color = normalColor;
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
            player.rb.velocity = flummyJumpSpeed;        
        }
            player.rb.useGravity = player.useGravitationInBounce;
    }


    void EnterGhostMode()
    {
        player.currentMode = Mode.ghost;
        sunlight.color = ghostColor;
        Debug.Log("Entering GHOOOOOOOOST MOOOODE!");
    }
}
