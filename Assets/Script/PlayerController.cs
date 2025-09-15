using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    [SerializeField]  private ScoreManager scoreMng;
    [SerializeField]  private GameObject goalObj;

    [SerializeField] GameObject resultObj;
    [SerializeField] GameObject clearParticle;
    [SerializeField] AudioManager audioMgr;

    public float speed = 5f;   // 横に移動する速度
    public float jumpP = 300f; // ジャンプ力
    public bool IsRun = false;//今走れるか

    private Animator animator;

    bool isJump = false;
    bool isGoal = false;
    bool isGameOver = false;

    Rigidbody2D rbody; // リジッドボディを使うための宣言

    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isGoal) return;
        if(isGameOver) return;

        // // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            && isJump == false)
        {
            rbody.AddForce(transform.up * jumpP);
            isJump = true;
            animator.SetBool("IsJump", true);
        }
    }

    private void FixedUpdate()
    {
        if(!IsRun) return;
        if(isGoal) return;
        if(isGameOver) rbody.velocity = new Vector2(speed*-1, rbody.velocity.y);
        else
        {
            //リジッドボディに一定の速度を入れる（横移動の速度, リジッドボディのyの速度）
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //床にあたってたら飛べる
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //audioMgr.JumpSound();
            if(isGameOver || isGoal)
            {
                IsRun = false;
                rbody.velocity = new Vector2(0, 0);

                resultObj.SetActive(true);
            }
            else
            {
                rbody.velocity = new Vector2(rbody.velocity.x, 0);
            }
            isJump = false;
            animator.SetBool("IsJump", false);
        }
    }

    //当たった時の処理
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Sailor") || collider.gameObject.CompareTag("GoldSailor"))
        {
            //ポイント加算して当たったオブジェクトを消す
            if(collider.gameObject.CompareTag("GoldSailor"))audioMgr.GoldGetSound();
            else audioMgr.GetSound();
            scoreMng.ScoreAdd(collider.GetComponent<Sailor>().GetPointNum());
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            IsRun = false;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                audioMgr.ExplosionSound();
                isGameOver = true;
                animator.SetBool("IsGameOver", true);
                IsRun = true;
                rbody.AddForce(transform.up * jumpP);
            });
          
        }
        else if (collider.gameObject.CompareTag("Goal"))
        {
            isGoal = true;
            animator.SetBool("IsJump", true);

            IsRun = false;
            rbody.velocity = new Vector2(0, 0);//カメラがこがこしないように

            clearParticle.SetActive(true);
            goalObj.SetActive(true);
            DOVirtual.DelayedCall(10, () =>
            {
                resultObj.SetActive(true);
            });
            transform.DOMove(new Vector3(10,0,0), 7).SetRelative(true).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                transform.DOMove(new Vector3(0f, 3f, 0f), 1f).SetRelative(true).SetLoops(-1,LoopType.Yoyo);
            });
        }
    }

    public void SetRunAnimator(){animator.SetBool("IsRun", true);}

    //memo;rbodyの書き換えはやらない、動かすならAddforceにする
}
