using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public Text text;

    public void Lose(){
        //Time.timeScale = 0.1f;
        //Time.fixedDeltaTime /= 10;
        GetComponent<ModeController>().player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        text.text = "I lose :(((((((((((";
        Invoke("RelodeScene", 1);   
            
    }

    public void Win(){
        Debug.Log("I won!!!!!!!!!!!!!!!!");
        Time.timeScale = 0;
        text.text = "I won!!!!!!!!!!!!!!!!";
    }

    void RelodeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        text.text = ""; 
        //Time.timeScale = 1;
        //Time.fixedDeltaTime *= 10; 
    }
}
