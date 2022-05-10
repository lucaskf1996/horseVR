using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "obstacle"){
            Debug.Log("bateu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
