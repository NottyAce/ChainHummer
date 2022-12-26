using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterManager))]
#endif

//CharacterManagerを継承
public class Hero : CharacterManager
{
    //攻撃の種類による敵の吹き飛び倍率
    [SerializeField]
    float[] hitbackrate;
    //攻撃の種類による威力の倍率
    [SerializeField]
    float[] atkrate;
    //攻撃による硬直時間（秒）
    [SerializeField]
    float[] atk_motion_length;
    //接地判定
    bool isGround;
    //ジャンプ中かを判定
    bool isJump;
    //攻撃中かを判定
    bool isAttack;
    [SerializeField]
    float jumpPos;
    //縦方向の移動速度
    float speed_y;
    //回転処理を行うのでローカル軸とグローバル軸の違いを区別
    public bool direction2;
    //ハンマーのスクリプトを格納
    [SerializeField]
    GameObject hummer;
    Hummer hummerscript;
    //重力を保存
    float temp2;

    private void Awake()
    {
        isJump = false;
        isAttack = false;
        jumpPos = 0.0f;
        speed_y = 0;
        direction = 0;
        direction2 = false;
        hummerscript = hummer.GetComponent<Hummer>();
        temp2 = gravity;
    }

    void FixedUpdate()
    {
        if (isAttack == false && isDown == false)
        {
            //弱攻撃
            if (Input.GetKey(KeyCode.J))
            {
                StartCoroutine(AttackCoolTime(0));
            }
            //強攻撃
            else if (Input.GetKey(KeyCode.I))
            {
                StartCoroutine(AttackCoolTime(1));
            }
            //投擲攻撃
            else if (Input.GetKey(KeyCode.L))
            {
                StartCoroutine(AttackCoolTime(2));
            }

            //左へ移動
            if (Input.GetKey(KeyCode.A))
            {
                if (direction2 == true)
                {
                    //向きを変えた場合スプライトの向きを変える
                    transform.Rotate(new Vector3(0, -180, 0));
                    direction2 = false;
                }
                //進行方向を決定
                direction = -1;
            }
            //右へ移動
            else if (Input.GetKey(KeyCode.D))
            {
                if (direction2 == false)
                {
                    //向きを変えた場合スプライトの向きを変える
                    transform.Rotate(new Vector3(0, 180, 0));
                    direction2 = true;
                }
                direction = 1;
            }
            //入力がない場合移動しない
            else
            {
                direction = 0;
            }
        }
        //行動できない
        else
        {
            direction = 0;
        }

        //設置判定を確認
        isGround = ground.IsGround();
        //重力
        speed_y = -gravity;
        //地面にいる
        if (isGround)
        {
            gravity = 0;
            //ジャンプ処理
            if (Input.GetKey(KeyCode.W))
            {
                speed_y = jumpspeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else
        {
            gravity = temp2;
        }
        //ジャンプ中かつ高度制限内のときの上昇処理
        if (isJump)
        {
            if (Input.GetKey(KeyCode.W) && jumpPos + max_height > transform.position.y)
            {
                speed_y = jumpspeed;
            }
            else
            {
                isJump = false;
            }
        }
        //移動
        Move(direction, speed_y);
    }

    //強攻撃のときの回転処理
    IEnumerator Rotate()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(0, 36, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //攻撃中のプレイヤーの硬直処理など（攻撃の種類 0：弱,1：強,2：投擲）
    IEnumerator AttackCoolTime(int number)
    {
        //攻撃中に変更
        isAttack = true;
        //ハンマーに当たった場合の倍率を変更
        hummerscript.ChangeRate(atkrate[number], hitbackrate[number]);
        //ハンマーに当たり判定を付与
        hummerscript.IsTrigger(false);
        //攻撃の種類ごとの動きや硬直
        switch (number)
        {
            //弱：その場で硬直
            case 0:
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
            //その場で回転
            case 1:
                StartCoroutine(Rotate());
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
            //ハンマーの移動
            case 2:
                StartCoroutine(Throw(20, false));
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
        }
        //ハンマーの当たり判定解除
        hummerscript.IsTrigger(true);
        //自身を行動可能にする
        isAttack = false;
    }
}
