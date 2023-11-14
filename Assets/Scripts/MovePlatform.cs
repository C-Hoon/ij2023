using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private Tilemap tilemap;
    public Transform startPos;   // Start
    public Transform endPos;    // End
    public Transform desPos;
    public float speed=0.8f;



     void Start()
    {
        tilemap = GetComponent<Tilemap>();  
        transform.position = startPos.position;
        desPos = endPos;
        
    }

    // Update is called once per frame
   void FixedUpdate()
    {
      tilemap.transform.position=  Vector2.MoveTowards(transform.position,desPos.position,Time.deltaTime*speed);// Moveoards 함수는 어떠한 타겟까지 갔을 경우
                                                                                                        // 멈추는 역할을 하는 것이다.

        if (Vector2.Distance(transform.position, desPos.position)<=0.05f)
        {
            if(desPos==endPos)
            {
                desPos = startPos;
            }
            else
            {
                desPos = endPos;
            }

        }

    }
}
