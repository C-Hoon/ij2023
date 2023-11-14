using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float distance;
    public float speed;
    public float atkDistance;

    public LayerMask isLayer;
    public GameObject bullet;
    public Transform pos;
    void Start()
    {

    }
    public float cooltime;
    public float curtime = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject bulletcopy = null;
        bool flipX = false;
        RaycastHit2D raycastL = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        RaycastHit2D raycastR = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);


        if (raycastL.collider != null)
        {
            if (Vector2.Distance(transform.position, raycastL.collider.transform.position) < atkDistance)
            {
                flipX = GetComponent<SpriteRenderer>().flipX = false;
                if (curtime <= 0)
                {
                    bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
                    curtime = cooltime;
                    Debug.Log(bulletcopy);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, raycastL.collider.transform.position, Time.deltaTime * speed);
            }
            curtime -= Time.deltaTime;
        }

        if (raycastR.collider != null)
        {
            if (Vector2.Distance(transform.position, raycastR.collider.transform.position) < atkDistance)
            {
                flipX = GetComponent<SpriteRenderer>().flipX = true;
                if (curtime <= 0)
                {
                    bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
                    curtime = cooltime;
                    Debug.Log(bulletcopy);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, raycastR.collider.transform.position, Time.deltaTime * speed);
            }
            curtime -= Time.deltaTime;
        }

        if (bulletcopy != null)
        {
            bulletcopy.GetComponent<EnemyBullet>().sr = flipX;
        }
    }
    
}