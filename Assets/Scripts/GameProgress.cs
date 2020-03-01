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
    public bool updateTime;
    private bool hasWon;

    private void Start()
    {
        startTime = Time.time;
        updateTime = true;
        hasWon = false;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
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
        text.text = "I won!!!!!!!!!!!!!!!!";
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        updateTime = false;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        text.text = "I won!!!!!!!!!!!!!!!!   Press space to go to the next Level";
        hasWon = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hasWon)
            {
                if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if(updateTime){
            float duration = Time.time - startTime;
            int minutes = (int)duration / 60;
            float seconds = duration % 60;
            timerText.text = minutes.ToString() + ":" + seconds.ToString("f2");
        }
    }

    void RelodeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        text.text = ""; 
        //Time.timeScale = 1;
        //Time.fixedDeltaTime *= 10; 
    }

}
