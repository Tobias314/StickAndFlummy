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

    private Vector3 flummyJumpSpeed = new Vector3(1,0,0) * 5;

    private bool stickyIsHeld;
    private bool flummyIsHeld;

    public void Start(){
        //EnterFlummyMode();
        player.rb.AddForce(flummyJumpSpeed, ForceMode.Impulse);
        EnterNormalMode();
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
        //LeaveCurrentMode();
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
    /*
    void LeaveCurrentMode(){
        Mode prevMode = player.currentMode;
        if (prevMode == Mode.normal)
        {

        }
        if (prevMode == Mode.flummy)
        {

        }
    }
    */
    void EnterNormalMode()
    {
        player.gameObject.layer = 0;
        player.GetComponent<SphereCollider>().material.bounciness = 0; //this belong is LeaveCurrentMode()
        player.currentMode = Mode.normal;
        sunlight.color = normalColor;
        player.rb.useGravity = true;
    }

    void EnterStickyMode(){
        player.gameObject.layer = 0;
        sunlight.color = stickyColor;
        player.currentMode = Mode.sticky;
        player.GetComponent<SphereCollider>().material.bounciness = 0;
    }
    void EnterFlummyMode(){
        player.gameObject.layer = 0;
        player.rb.useGravity = false;
        sunlight.color = flummyColor;
        player.currentMode = Mode.flummy;
        player.GetComponent<SphereCollider>().material.bounciness = 1;
        if(player.isColliding){
            //player.rb.AddForce((Mathf.Abs(player.collisionDirection.x) * player.collisionDirection) + ((1.0f - Mathf.Abs(player.collisionDirection.x)) * Vector3.up) * 10, ForceMode.Impulse);
            //player.rb.velocity = ;
            player.rb.AddForce( - player.collisionDirection * 10 + Vector3.up * 7, ForceMode.Impulse);        
        }
            player.rb.useGravity = player.useGravitationInBounce;
    }


    void EnterGhostMode()
    {
        player.currentMode = Mode.ghost;
        player.GetComponent<SphereCollider>().material.bounciness = 0;
        sunlight.color = ghostColor;
        Debug.Log("Entering GHOOOOOOOOST MOOOODE!");
        player.rb.useGravity = true;
        player.gameObject.layer = 8;
    }
}
