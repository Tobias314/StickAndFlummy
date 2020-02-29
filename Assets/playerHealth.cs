using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Spikes")){
            Debug.Log("I died :(");
        }
    }
}
