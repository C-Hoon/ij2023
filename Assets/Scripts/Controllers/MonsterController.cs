using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDir
{
    Left,
    Right,
}
public class MonsterController : MonoBehaviour
{
    public int maxhealth;
    public int enemyhealth;
    public MoveDir moveDir;
    public GameObject bulletPrefab;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;
    public CircleCollider2D circleCollider2D;
    GameObject target;


    public Vector2 MoveDirection { get { if (moveDir == MoveDir.Left) { return Vector2.left; } else { return Vector2.right; } } }

    bool follow = false;

    public float delay; //딜레이
    public int repeat; //반복 횟수
    int value = 0;

    MonsterStatBar statBar;

    [SerializeField] [Range(1f, 4f)] float moveSpeed = 3f; //추격속도

    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f; //근접거리
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Invoke("Think", 3);
    }

    void Start()
    {
        delay = 0.1f;
        repeat = 2;
        //transform;
        GameObject go = Managers.Resource.Instantiate("UI/WorldSpace/UI_MonsterStatBar", transform);
        go.transform.position += new Vector3(0, 1.0f);
        statBar = go.GetComponent<MonsterStatBar>();
        statBar.SetUI(enemyhealth, maxhealth);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(MoveDirection.x, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + MoveDirection.x * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    void Turn()
    {
        moveDir = MoveDir.Right;
        spriteRenderer.flipX = MoveDirection.x != 1;

        CancelInvoke();
        //Invoke("Think", 3);
    }

    public void OnDamaged(int damage, bool isCritical = false)
    {
        value = repeat;
        StartCoroutine(FlickerCoroution());
        enemyhealth -= damage;
        statBar.SetUI(enemyhealth, maxhealth);

        Debug.Log($"OnDamaged");
        // 데미지 수치 올라가는 ui
        GameObject go = Managers.Resource.Instantiate("UI/WorldSpace/UI_Damage");
        go.transform.position = transform.position;
        go.GetComponent<UI_Damage>().Init(damage, isCritical);

        if (enemyhealth <= 0)
        {
            //Point
            GameCore.Managers.Game.stagePoint += 100;

            //Sprtie Flip Y
            spriteRenderer.flipY = true;

            //Collider Disable
            capsuleCollider.enabled = false;

            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            EventBus.Publish(new EnemyDieEvent { value = 1 });

            //Destory
            Invoke("DeActive", 3);
        }
    }

    void DeActive()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator FlickerCoroution()
    {
        if (value > 0)
        {
            value -= 1;
        }
        else
        {
            yield break;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

        yield return new WaitForSeconds(delay);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);

        yield return new WaitForSeconds(delay);

        StartCoroutine(FlickerCoroution());
    }

    public void Fire()
    {
        GameObject bullet = Managers.Resource.Instantiate("Projectile/Tinkle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            follow = true;
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            follow = false;
            target = null;
        }
    }
}
/*public class EnemyDieEvent : SubscribeEvent
{
}*/