using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public GameProgress progress;
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Spikes")){
            Debug.Log("I died :(");
            progress.Lose();
        }
        if(col.CompareTag("Finish")){
            progress.Win();
        }
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            progress.Lose();
        }
    }
}
