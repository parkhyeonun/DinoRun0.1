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
        //1. ����(�����Ŀ�)
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && isGround)
        {
            rigid.AddForce(Vector2.up * startJumpPower , ForceMode2D.Impulse);   
        }

        isJumpKey = Input.GetButton("Jump") || Input.GetMouseButton(0);


    }

    //1. �� ���� 
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
    //2. ���� (���� �浹 �̺�Ʈ)
    void OnCollisionStay2D(Collision2D collision)
    {
        //4. �ִϸ��̼�
        if (! isGround)
        {
            ChangeAnim(State.Run);
            //����
            sound.PlaySound(Sounder.Sfx.Land);
            JumpPower = 1;
        }

       
        isGround = true;
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.Jump);
        //����
        sound.PlaySound(Sounder.Sfx.Jump);
        isGround = false;
        
    }

    //3. ��ֹ� ��ġ (Ʈ���� �浹 �̺�Ʈ)
    void OnTriggerEnter2D(Collider2D collision)
    {
        rigid.simulated = false;
        //���� 
        ChangeAnim(State.Hit);
        sound.PlaySound(Sounder.Sfx.Hit);
        GameManager.GameOver();

    }
    //5. ����
}
