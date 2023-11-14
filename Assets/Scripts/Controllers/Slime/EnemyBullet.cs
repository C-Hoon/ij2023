using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public bool sr;
    public float speed;
    public float distance;
    public int bulletdamage;
    public LayerMask isLayer;
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

        if (sr == true)
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
        if (collision.tag == "Player")
        {
            PlayerMove player = collision.transform.gameObject.GetComponent<PlayerMove>();

            player.ChangeHealth(PlayerMove.ChangeHealthType.DOWN);
        }
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
