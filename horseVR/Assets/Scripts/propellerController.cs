using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propellerController : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
    }


    // Update is called once per frame
    void Update()
    {
        if(gm.move){
            gameObject.transform.position += new Vector3(0f, 0f, -1f * Time.deltaTime);
            transform.Rotate (new Vector3 (0, 0, 30) * Time.deltaTime);
        }
    }
}
