using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barController : MonoBehaviour
{

    private bool right;
    private Vector3 startPosition;
    private float t;
    private float temp;
    GameManager gm;
    private float startX, startY;

    // Start is called before the first frame update
    void Start()
    {
        right = true;
        startPosition = gameObject.transform.position;
        t = 0;
        gm = GameManager.GetInstance();
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(gm.move){
            gameObject.transform.position += new Vector3(0f, 0f, -1f * Time.deltaTime);

            if(right){
                gameObject.transform.position += gameObject.transform.right * Time.deltaTime;
                if(temp > 1f && t > 0.2f){
                    right = false;
                    t = 0;
                }
            }
            else if(!right){
                gameObject.transform.position -= gameObject.transform.right * Time.deltaTime;
                if(temp > 1f && t > 0.2f){
                    right = true;
                    t = 0;
                }
            }
            temp = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x-startX, 2) + Mathf.Pow(gameObject.transform.position.y-startY, 2));
            t+=Time.deltaTime;
        }
    }
}
