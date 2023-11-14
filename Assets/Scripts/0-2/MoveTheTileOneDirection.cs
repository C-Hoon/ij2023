using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveTheTileOneDirection : MonoBehaviour
{
    // Start is called before the first frame update

    private Tilemap tilemap;
    public Collider2D pop_up;
    public float speed;


    void Start()
    {
        tilemap= GetComponent<Tilemap>();   
    
    }

    // Update is called once per frame
    void Update()
    {
        tilemap.transform.position = new Vector2(tilemap.transform.position.x, tilemap.transform.position.y+(speed*Time.deltaTime));
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            tilemap.transform.position = new Vector2(pop_up.transform.position.x,pop_up.transform.position.y);
        }
    }


}
