using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool sr;
    public float speed;
    public float distance;
    public int bulletdamage;
    public bool isCritical = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = sr;
        if (sr == false)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            MonsterController monsterController = collision.transform.gameObject.GetComponent<MonsterController>();

            monsterController.OnDamaged(bulletdamage, isCritical);
            DestroyBullet();
        }
        else if(collision.tag == "Platform")
        {
            DestroyBullet();
        }
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
