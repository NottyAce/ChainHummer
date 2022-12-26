using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterManager))]
#endif

//CharacterManagerを継承
public class Enemy : CharacterManager
{
    //プレイヤーオブジェクトを格納
    [SerializeField]
    GameObject player;
    //当たり判定があるタグ
    string enemyTag = "Player";

    void FixedUpdate()
    {
        if (isDown == false)
        {
            //プレイヤーとの位置関係を把握
            Vector2 targeting = (player.transform.position - this.transform.position);
            //位置関係によるスプライトの反転と進行方向を決定処理
            //スプライトが左右非対称の場合に必要
            if (targeting.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                direction = 1;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                direction = -1;
            }
            //x方向にのみプレイヤーを追う
            Move(direction, -gravity);
        }
    }

    //プレイヤーに攻撃する関数（自分の進行方向）
    void Attack(int direction)
    {
        //プレイヤーの吹っ飛ぶ方向
        int direction2 = direction;
        //プレイヤーの向きから吹っ飛ぶ方向を決定
        Hero hero = player.GetComponent<Hero>();
        if (hero.direction2)
        {
            direction2 *= -1;
        }
        //ダメージ処理と吹っ飛び処理（プレイヤー側で行われる）
        hero.Damege(atk, hitback,direction2,0.5f);
    }

    //プレイヤーとの当たり判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーと接触
        if (collision.collider.tag == enemyTag)
        {
            //攻撃を行う
            Attack(direction);
            //プレイヤーに連続攻撃しないように敵自身も少し吹っ飛ぶ
            Damege(0, hitback,direction * -1,0);
        }
    }
}
