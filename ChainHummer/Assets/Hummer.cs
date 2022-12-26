using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : MonoBehaviour
{
    //現在の威力倍率
    float atkrate;
    //現在の吹き飛ばし倍率
    float hitbackrate;
    //当たり判定が発生するタグ
    string enemyTag = "Enemy";
    //プレイヤーのステータスを参照するためにオブジェクトを保存
    [SerializeField]
    GameObject player;
    //自分の当たり判定を操作
    Collider2D istrigger;

    void Awake()
    {
        istrigger = GetComponent<Collider2D>();
    }

    //倍率を変更（威力倍率、吹き飛ばし倍率）
    public void ChangeRate(float newrate , float newhitbackrate)
    {
        atkrate = newrate;
        hitbackrate = newhitbackrate;
    }

    //
    void Attack(string enemyname)
    {
        //当たったオブジェクトを取得
        GameObject enemy = GameObject.Find(enemyname);
        //当たったオブジェクトのスクリプトを取得
        Enemy enemy2 = enemy.GetComponent<Enemy>();
        //プレイヤーのステータスを取得
        Hero hero = player.GetComponent<Hero>();
        //ステータスと各種倍率を掛けてダメージ処理
        enemy.GetComponent<Enemy>().Damege((int)(atkrate*hero.atk), (int)(hitbackrate* hero.hitback),enemy2.direction * -1,0.5f);
    }

    //接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == enemyTag)
        {
            Attack(collision.gameObject.name);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == enemyTag)
        {
            Attack(collision.gameObject.name);
        }
    }

    //当たり判定の切り替え
    public void IsTrigger(bool flag)
    {
        istrigger.isTrigger = flag;
    }
}
