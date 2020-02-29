using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    public void Lose(){
        //Time.timeScale = 0.1f;
        //Time.fixedDeltaTime /= 10; 
        Invoke("RelodeScene", 1);        
    }

    public void Win(){
        Debug.Log("I won!!!!!!!!!!!!!!!!");
    }

    void RelodeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
        //Time.fixedDeltaTime *= 10; 
    }
}
