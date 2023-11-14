using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testttt : MonoBehaviour
{

    public float distance;
    public float speed;
    public LayerMask isLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.right * -1, new Color(0, 1, 0));
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
        }
    }
}
