using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelectPos : MonoBehaviour
{
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
    }

    void ResetPos(){
        transform.position = oldPos;
    }
}
