using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class handRaycaster : MonoBehaviour
{

    GameManager gm;
    private SteamVR_LaserPointer steamVrLaserPointer;
    private float t;
    private bool selecting = true;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void Start(){
        gm = GameManager.GetInstance();
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if(t>1f)
        {
            Debug.Log("objeto clicado com o laser " + e.target.name);
            t = 0f;
            if(e.target.gameObject.tag == "target"){
                Destroy(e.target.gameObject);
            }
        }

        if(selecting){
            if(e.target.name == "Block"){
                Debug.Log("Block");
            }
            if(e.target.name == "Target"){
                Debug.Log("Target");
            }
            if(e.target.name == "Ceiling"){
                Debug.Log("Ceiling");
            }
            if(e.target.name == "HalfWall"){
                Debug.Log("HalfWall");
            }
            if(e.target.name == "Propeller"){
                Debug.Log("Propeller");
            }
            if(e.target.name == "Cylinder"){
                Debug.Log("Cylinder");
            }
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
    }
}