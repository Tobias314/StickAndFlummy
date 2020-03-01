using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public Text text;
    public Text timerText;
    private float startTime;
    public bool updateTime = true;

    void Start()
    { 
        startTime = Time.time;
        updateTime = true;
    }

    public void Update(){
        if(updateTime){
            float duration = Time.time - startTime;
            int minutes = (int)duration / 60;
            float seconds = duration % 60;
            timerText.text = minutes.ToString() + ":" + seconds.ToString("f2");
        }
    }

    public void Lose(){
        //Time.timeScale = 0.1f;
        //Time.fixedDeltaTime /= 10;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        text.text = "I lose :(((((((((((";
        Invoke("RelodeScene", 1);   
        updateTime = false;
    }

    public void Win(){
        Debug.Log("I won!!!!!!!!!!!!!!!!");
        Time.timeScale = 0;
        text.text = "I won!!!!!!!!!!!!!!!!";
        updateTime = false;
    }

    void RelodeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        text.text = ""; 
        //Time.timeScale = 1;
        //Time.fixedDeltaTime *= 10; 
    }
}
