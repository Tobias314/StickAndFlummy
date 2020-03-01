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


    private float transitionTime = 0.02f;

    private Vector3 flummyJumpSpeed = new Vector3(1,0,0) * 5;

    private bool stickyIsHeld;
    private bool flummyIsHeld;

    // for color transition
    private Color prevColor;
    private Color currentColor;
    private float startOfTransition;
    
    public void Start(){
        //EnterFlummyMode();
        player.rb.AddForce(flummyJumpSpeed, ForceMode.Impulse);
        EnterNormalMode();

        prevColor = normalColor;
        currentColor = normalColor;
    }
    void Update(){
        // delay is to make it easier to switch from sticky to flummy without accidentally passing by normal or ghost
        if(Input.GetKeyDown(KeyCode.S)){
            stickyIsHeld = true;
            CancelInvoke();
            Invoke("UpdateMode", transitionTime);
            StartColorTransition();
        }
         if(Input.GetKeyDown(KeyCode.F)){
            flummyIsHeld = true;
            CancelInvoke();
            Invoke("UpdateMode", transitionTime);
            StartColorTransition();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            stickyIsHeld = false;
            CancelInvoke();
            Invoke("UpdateMode", transitionTime);
            StartColorTransition();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            flummyIsHeld = false;
            CancelInvoke();
            Invoke("UpdateMode", transitionTime);
            StartColorTransition();
        }

        LerpColor();
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
        player.gameObject.layer = 0;
        Mode prevMode = player.currentMode;
        if (prevMode == Mode.ghost)
        {//are we intersecting with a collider?
            Collider[] cols = Physics.OverlapSphere(player.transform.position, transform.localScale.x);
            foreach (Collider col in cols)
            {
                if (!col.CompareTag("Player"))
                {
                    GetComponent<GameProgress>().Lose();
                }
            }
        }
    }
    
    void EnterNormalMode()
    {
        player.GetComponent<SphereCollider>().material.bounciness = 0; //this belong is LeaveCurrentMode()
        player.currentMode = Mode.normal;
        SetColor(normalColor);
        player.rb.useGravity = true;
    }

    void EnterStickyMode(){
        SetColor(stickyColor);
        player.currentMode = Mode.sticky;
        player.GetComponent<SphereCollider>().material.bounciness = 0;
    }
    void EnterFlummyMode(){
        player.rb.useGravity = false;
        SetColor(flummyColor);
        player.currentMode = Mode.flummy;
        player.GetComponent<SphereCollider>().material.bounciness = 1;
        if(player.collisionCount > 0){
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
        SetColor(ghostColor);
        Debug.Log("Entering GHOOOOOOOOST MOOOODE!");
        player.rb.useGravity = true;
        player.gameObject.layer = 8;
    }
    

    void SetColor(Color color)
    {
        RenderSettings.ambientLight = color * 0.3f;
        sunlight.color = color;
    }

    private void StartColorTransition()
    {
        startOfTransition = Time.time;
        prevColor = sunlight.color;
        //COPY PASTA
        if (!stickyIsHeld && !flummyIsHeld)
        {
            currentColor = normalColor;
        }
        if (stickyIsHeld && !flummyIsHeld)
        {
            currentColor = stickyColor;
        }
        if (!stickyIsHeld && flummyIsHeld)
        {
            currentColor = flummyColor;
        }
        if (stickyIsHeld && flummyIsHeld)
        {
            currentColor = ghostColor;
        }
    }

    private void LerpColor()
    {
        Color newColor = Color.Lerp(prevColor, currentColor, (Time.time - startOfTransition) / (transitionTime * 2));
        RenderSettings.ambientLight = newColor * 0.3f;
        sunlight.color = newColor;
    }
}
