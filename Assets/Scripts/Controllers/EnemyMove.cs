using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int nextMove;
    public int maxhealth;
    public int enemyhealth;
    public Transform target;
    public GameObject bullet;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        

        //Invoke("Think", 3);
    }

    void Start()
    {

        maxhealth = 500;
        enemyhealth = 300;

        delay = 0.1f;
        repeat = 2;
        //transform;
        GameObject go = Managers.Resource.Instantiate("UI/WorldSpace/UI_MonsterStatBar", transform);
        go.transform.position += new Vector3(0, 1.0f);
        statBar = go.GetComponent<MonsterStatBar>();

        statBar.SetUI(enemyhealth, maxhealth);

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);



        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    private void Update()
    {
        FollowTarget();
    }

    //재귀함수
    /*void Think()
   {
       //Set Next Active
       nextMove = Random.Range(-1, 2);

       //Sprite Animation
       animator.SetInteger("WalkSpeed", nextMove);

       //Flip Sprite
       if(nextMove != 0)
           spriteRenderer.flipX = nextMove == 1;

       //Recursive
       float nextThinkTime = Random.Range(2f, 5f);
       Invoke("Think", nextThinkTime);
   }*/

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        //Invoke("Think", 3);
    }

    public void OnDamaged(int damage, bool isCritical = false)
    {
        //Sprite Alpha
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
            Invoke("DeActive", 1);
        }
    }

    public void DeActive()
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

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        follow = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        follow = false;
    }
}
public class EnemyDieEvent : SubscribeEvent
{

}
