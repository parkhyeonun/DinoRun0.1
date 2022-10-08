using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Dino : MonoBehaviour
{
    public enum State { Stand , Run , Jump ,Hit }
    public float startJumpPower;
    public float JumpPower;
    public bool isGround;
    public bool isJumpKey;
    public UnityEvent onHit;
    Rigidbody2D rigid;
    Animator anim;
    Sounder sound;
    private void Start()
    {
        sound.PlaySound(Sounder.Sfx.Reset);
    }

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<Sounder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isLive)
            return;
        //1. 점프(점프파워)
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && isGround)
        {
            rigid.AddForce(Vector2.up * startJumpPower , ForceMode2D.Impulse);   
        }

        isJumpKey = Input.GetButton("Jump") || Input.GetMouseButton(0);


    }

    //1. 롱 점프 
    private void FixedUpdate()
    {
        if (!GameManager.isLive)
            return;

        if (isJumpKey && !isGround)
        {
            JumpPower = Mathf.Lerp(JumpPower, 0, 0.1f);
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }

    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state);

    }
    //2. 착지 (물리 충돌 이벤트)
    void OnCollisionStay2D(Collision2D collision)
    {
        //4. 애니메이션
        if (! isGround)
        {
            ChangeAnim(State.Run);
            //사운드
            sound.PlaySound(Sounder.Sfx.Land);
            JumpPower = 1;
        }

       
        isGround = true;
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.Jump);
        //사운드
        sound.PlaySound(Sounder.Sfx.Jump);
        isGround = false;
        
    }

    //3. 장애물 터치 (트리거 충돌 이벤트)
    void OnTriggerEnter2D(Collider2D collision)
    {
        rigid.simulated = false;
        //사운드 
        ChangeAnim(State.Hit);
        sound.PlaySound(Sounder.Sfx.Hit);
        GameManager.GameOver();

    }
    //5. 사운드
}
