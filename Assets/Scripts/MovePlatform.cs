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
      tilemap.transform.position=  Vector2.MoveTowards(transform.position,desPos.position,Time.deltaTime*speed);// Moveoards �Լ��� ��� Ÿ�ٱ��� ���� ���
                                                                                                        // ���ߴ� ������ �ϴ� ���̴�.

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
