using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelectPos : MonoBehaviour
{
    Vector3 oldPos;
    Quaternion oldRotate;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        oldRotate = transform.rotation;
    }

    public void ResetPos(){
        transform.position = oldPos;
        transform.rotation = oldRotate;
    }
}
