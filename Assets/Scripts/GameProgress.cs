using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public Text text;
    private bool hasWon;

    private void Start()
    {
        hasWon = false;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }
    public void Lose(){
        //Time.timeScale = 0.1f;
        //Time.fixedDeltaTime /= 10;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        text.text = "I lose :(((((((((((";
        Invoke("RelodeScene", 1);   
    }

    public void Win(){
        Debug.Log("I won!!!!!!!!!!!!!!!!");
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
    }

    void RelodeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        text.text = ""; 
        //Time.timeScale = 1;
        //Time.fixedDeltaTime *= 10; 
    }

}
