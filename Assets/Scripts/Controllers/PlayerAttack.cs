using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rig;
    public GameObject bullet;
    public Transform rspos;
    public Transform lspos;
    public Transform rapos;
    public Transform lapos;
    public Vector2 boxSize;
    public float distance;
    public LayerMask isLayer;
    private PlayerMove playerMove;


    public int attackdamage { get { return playerMove.playerStat.attack; } }
    public float acooltime; //다음 Attack까지 걸리는 시간
    private float acurtime;

    //public int bulletdamage { get { return playerMove.playerStat.attack; } }
    public float scooltime = 0.5f; //다음 Shoot까지 걸리는 시간
    private float scurtime;

    bool isCritical = false;


    public int attackDamageWithCrit
    {
        get
        {
            int attack = playerMove.playerStat.attack;
            float dice = Random.Range(1,101);
            int rate = playerMove.playerStat.critRate;
            Debug.Log($"dice : {dice}, rate : {rate}");
            if (dice <= rate)
            {
                isCritical = true;
                float critDamage = playerMove.playerStat.critDamage;
                float damage = attack * ((critDamage + 100f) / 100f);
                Debug.Log($"critical!!!!!!!!!! : {damage}, {attack}");
                return (int)damage;
            }
            isCritical = false;
            return attack;
        }
    }

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        scurtime = 0;
        acooltime = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        //Attack
        if (acurtime <= 0)
        {
            if (GameCore.Managers.Input.GetKey(Define.keyMaps.Attack))
            {
                bool sr = GetComponent<SpriteRenderer>().flipX;
                if (sr == false)
                {
                    Collider2D[] rcollider2Ds = Physics2D.OverlapBoxAll(rapos.position, boxSize, 0);
                    foreach (Collider2D collider in rcollider2Ds)
                    {
                        Debug.Log(collider.tag);
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<MonsterController>().OnDamaged(attackDamageWithCrit, isCritical);
                            EventBus.Publish(new BladeAttackEvent() { value = attackDamageWithCrit });
                        }
                    }
                }
                else
                {
                    Collider2D[] lcollider2Ds = Physics2D.OverlapBoxAll(lapos.position, boxSize, 0);
                    foreach (Collider2D collider in lcollider2Ds)
                    {
                        Debug.Log(collider.tag);
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<MonsterController>().OnDamaged(attackDamageWithCrit, isCritical);
                            EventBus.Publish(new BladeAttackEvent() { value = attackDamageWithCrit });
                        }
                    }
                }
                animator.SetTrigger("doAttack");
                acurtime = acooltime;
            }
        }
        else
        {
            acurtime -= Time.deltaTime;
        }

        //Shoot
        if (scurtime <= 0)
        {
            if (GameCore.Managers.Input.GetKey(Define.keyMaps.Shoot))
            {
                Debug.Log("Shoot");
                GameObject go;
                bool dir = this.GetComponent<SpriteRenderer>().flipX; //좌우반전
                if (dir == true) //왼쪽일 때
                {
                    Debug.Log("Left");
                    go = Instantiate(bullet, lspos.position, transform.rotation);
                }
                else //오른쪽일 때
                {
                    Debug.Log("Right");
                    go = Instantiate(bullet, rspos.position, transform.rotation);
                }

                Bullet bt = go.GetComponent<Bullet>();
                bt.sr = dir;
                bt.bulletdamage = attackDamageWithCrit;
                bt.isCritical = isCritical;

                animator.SetTrigger("doDragonfire");
                scurtime = scooltime;
            }
        }
        scurtime -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(rapos.position, boxSize);
        Gizmos.DrawWireCube(lapos.position, boxSize);
    }
}
class BladeAttackEvent : SubscribeEvent
{
}
class GunAttackEvent : SubscribeEvent
{
}