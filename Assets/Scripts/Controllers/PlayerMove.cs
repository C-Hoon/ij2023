using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using GameCore;
using GameCore.Data;

public class PlayerMove : UnitMove
{
    [SerializeField]
    public PlayerStat playerStat;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D capsuleCollider;

    [SerializeField]
    GameObject backGround;

    int health;
    int mana;
    public int Health { get { return health; } set { health = value; } }
    public int MaxHealth { get { return playerStat.hp; } }
    public int Mana { get { return mana; } set { mana = value; } }
    public int MaxMana { get { return playerStat.mp; } }


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        Managers.Game.Player = transform.gameObject;
        Init();
    }
    void Init()
    {
        playerStat = new PlayerStat();
        playerStat.Init(playerStat.level);
        //아이템 순회후 스텟 증가
        health = MaxHealth;
        mana = MaxMana;

        Vector3 vec = Camera.main.transform.position;
        vec = new Vector3(vec.x, vec.y, 1f);
        GameObject go = Instantiate(backGround);
        go.transform.position = vec;

        PlayerStat stat = GetComponent<PlayerMove>().playerStat;
        string weaponName = PlayerPrefs.GetString(typeof(Weapon).Name);
        if (string.IsNullOrEmpty(weaponName))
            return;
        Weapon weaponScriptableObject = Resources.Load<Weapon>("Equipment/Weapons/" + weaponName);
        stat.AddToWeapon(weaponScriptableObject);
    }

    #region characterStats
    //***********************************************************************************
    

    //체력감소 델리게이트
    //public delegate void HealthUIUpdateDelegate();
    //public event HealthUIUpdateDelegate HealthUIUpdateEvent;

    public int GetStat(Define.CharacterStat stat)
    {
        switch (stat)
        {
            case Define.CharacterStat.MaxHp:
                return MaxHealth;
            case Define.CharacterStat.Hp:
                return Health;
            case Define.CharacterStat.MaxMp:
                return MaxMana;
            case Define.CharacterStat.Mp:
                return Mana;
        }
        return 0;
    }
    public enum ChangeHealthType
    {
        UP,
        DOWN,
    }

    public void ChangeHealth(ChangeHealthType changeHealthType)
    {
        if (changeHealthType == ChangeHealthType.UP)
        {
            if (health > 0)
            {
                health++;
                EventBus.Publish(new UpdateStatUIEvent() { value = 1 });
                Debug.Log(GetComponent<PlayerMove>());
            }
        }
        else if (changeHealthType == ChangeHealthType.DOWN)
        {
            if (health > 1)
            {
                health--;
                EventBus.Publish(new UpdateStatUIEvent() { value = -1 });
                Debug.Log(GetComponent<PlayerMove>());
            }
            else
            {
                health--;
                EventBus.Publish(new UpdateStatUIEvent() { value = 0 });
                //Player Die Effect
                OnDie();
                EventBus.Publish(new PlayerDieEvent { value = 1 });
                //Result UI
                Debug.Log("Die!");

                //Retry UI Button
                Managers.UI.ShowPopupUI(Define.PopupUIType.DiePopup, "DiePopup");
            }
        }

    }
    //***********************************************************************************
    #endregion

    void Update()
    {
        //Debug.Log(playerStat.level);
        if (Managers.Game.isPaused == true)
            return;

        if (animator.GetBool("doDie") == true)
            return;


        //Jump
        if (Managers.Input.GetKeyDown(Define.keyMaps.Jump) && !animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * playerStat.jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }

        //Stop Speed
        if (Managers.Input.GetKeyUp(Define.keyMaps.Left) || Managers.Input.GetKeyUp(Define.keyMaps.Right))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,
                rigid.velocity.y);
        }

        //Direction Sprite
        if (Managers.Input.GetKey(Define.keyMaps.Left) || Managers.Input.GetKey(Define.keyMaps.Right))
        {
            spriteRenderer.flipX = Managers.Input.GetAxisRaw == -1;
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) == 0)
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);

    }

    /*
    void Jump()
    {
        //Jump
        if (!animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
    }
    */
    void FixedUpdate()
    {
        if (Managers.Game.isPaused == true)
            return;

        if (animator.GetBool("doDie") == true)
            return;

        //Move Speed
        float h = Managers.Input.GetAxisRaw;
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > playerStat.moveSpeed) //Right Max Speed
        {
            rigid.velocity = new Vector2(playerStat.moveSpeed, rigid.velocity.y);
            //Debug.Log("R" + rigid.velocity);
        }


        else if (rigid.velocity.x < playerStat.moveSpeed * (-1)) //Left Max Speed
        {
            rigid.velocity = new Vector2(playerStat.moveSpeed * (-1), rigid.velocity.y);
            //Debug.Log("L" + rigid.velocity);
        }

        //Landing Platform
        /*if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("isJump", false);
                }
            }
        }*/

    }

    //피격모션
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                Debug.Log("적을 공격했습니다.");
                OnAttack(collision.transform);
            }
            //Damaged
            else
            {
                Debug.Log("적에게 공격 당했습니다.");
                OnDamaged(collision.transform.position);
            }
        }

        else if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("함정에 걸렸습니다.");
            OnDamaged(collision.transform.position);
        }

        /*// 점프 후 착지 시
        if(collision.gameObject.tag == "Platform")
        {
            animator.SetBool("isJump", false);
        }*/
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            animator.SetBool("isJump", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSiliver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze) GameCore.Managers.Game.stagePoint += 50;
            else if (isSiliver) GameCore.Managers.Game.stagePoint += 100;
            else if (isGold) GameCore.Managers.Game.stagePoint += 300;

            //Deactive Item
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Finish")
        {
            //Next Stage
            GameCore.Managers.Game.NextStage();
        }
    }

    //밟기 공격
    void OnAttack(Transform enemy)
    {
        //Point
        //GameCore.Managers.Game.stagePoint += 100;

        //Reaction Force
        rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);

        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged(playerStat.attack);
    }

    void OnDamaged(Vector2 targetPos)
    {
        if (animator.GetBool("doDie") == true)
            return;

        if (gameObject.layer == 9)
        {
            // Change Layer
            gameObject.layer = 10;

            //Health Down
            ChangeHealth(ChangeHealthType.DOWN);

            if(animator.GetBool("doDie") == false)
            {
                // View Alpha
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            } 

            // Reaction Force
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

            // 무적시간
            animator.SetTrigger("doDamaged");
            Invoke("OffDamaged", 2);
        }
    }

    // 무적 해제
    void OffDamaged()
    {
        if (animator.GetBool("doDie") == true)
            return;

        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //Sprite Alpha
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprtie Flip Y
        //spriteRenderer.flipY = true;

        //Collider Disable
        //capsuleCollider.enabled = false;

        //Die Effect Jump
        //rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        animator.SetBool("doDie", true);
        gameObject.layer = 10;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
class PlayerDieEvent : SubscribeEvent
{
}
