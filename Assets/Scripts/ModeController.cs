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
    }

    void LeaveCurrentMode(){
        return;
    }
    void EnterStickyMode(){
        sunlight.color = stickyColor;
        player.ChangeMode(Mode.sticky);
    }
    void EnterFlummyMode(){
        sunlight.color = flummyColor;
        player.ChangeMode(Mode.flummy);
    }

}
