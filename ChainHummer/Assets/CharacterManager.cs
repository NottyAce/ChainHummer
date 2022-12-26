using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HeroクラスとEnemyクラスが継承するクラス
public class CharacterManager : MonoBehaviour
{
    //位置
    //[SerializeField]
    //protected float[] position;
    //最大体力
    [SerializeField]
    protected int maxhp;
    //体力
    [SerializeField]
    protected int hp;
    //移動速度(横)
    [SerializeField]
    protected float speed;
    //ジャンプの高度制限
    [SerializeField]
    protected float max_height;
    //攻撃力
    [SerializeField]
    public int atk;
    //防御力
    [SerializeField]
    protected int def;
    //攻撃を当てたときに相手を吹っ飛ばす距離
    //　※正確には吹き飛ぶ距離ではなく、細かい距離を何回移動するか
    [SerializeField]
    public int hitback = 10;
    //自身の吹っ飛びにくさ(0～1)大きいと飛ばない
    [SerializeField]
    protected float blowrate;
    //重力
    [SerializeField]
    protected float gravity;
    //ジャンプの上昇速度
    [SerializeField]
    protected float jumpspeed;
    //自身の物理判定
    protected Rigidbody2D rb = null;
    //接地判定のスクリプト
    [SerializeField]
    protected CheckGround ground;
    //攻撃を受けている最中かどうか trueだと行動ができなくなる
    protected bool isDown = false;
    //自身のタグを変更するためのタグの一時保存
    string temp;
    //飛ばすオブジェクトを格納
    [SerializeField]
    GameObject throwobject;
    //進行方向（-1,0,1）
    public int direction;
    //飛び道具の速度
    [SerializeField]
    float throwspeed;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //キャラクターを移動させる関数（進行方向、縦方向の速さ）
    public void Move(float direction,float speed_y)
    {
        rb.velocity = new Vector2(speed * direction, speed_y);
    }

    //攻撃が当たったときの処理（ダメージ、(※)吹っ飛ぶ距離、吹っ飛ぶ方向、無敵時間）
    //　※正確には吹き飛ぶ距離ではなく、細かい距離を何回移動するか
    public void Damege(int damege, int knockbacklength, int direction,float cooltime)
    {
        if (isDown == false)
        {
            //ダメージ処理
            isDown = true;
            if (damege - def >= 0)
            {
                hp -= damege - def;
                if (hp <= 0)
                {
                    //死亡処理
                }
            }

            //ヒットバック処理
            StartCoroutine(HitBack((int)(knockbacklength * (1 - blowrate)),direction));
            
            //連続ヒットを防ぐための無敵時間
            //タグを変更することで当たり判定が発生しない
            temp = tag;
            tag = "Untagged";
            StartCoroutine(CoolTime(cooltime));
        }
    }

    //飛び道具を投げる処理（投げる距離、敵かどうか）
    //  ※当たり判定やダメージ処理ではない
    protected IEnumerator Throw(int length, bool isenemy)
    {
        //投げる向きを決定
        //プレイヤーと敵で進行方向の決め方が異なるため、プレイヤーと敵を判別
        int direction = -1;
        if (isenemy)
        {
            direction = this.direction;
        }

        //投げる処理
        int i = 0;
        while (i < length)
        {
            yield return new WaitForSeconds(0.01f);
            throwobject.transform.Translate(direction * throwspeed, 0, 0);
            i++;
        }

        //プレイヤーのハンマーが手元に戻って来る処理
        if (isenemy == false)
        {
            while (i > 0)
            {
                yield return new WaitForSeconds(0.01f);
                throwobject.transform.Translate(-1 * direction * throwspeed, 0, 0);
                i--;
            }
        }
    }

    //攻撃されたときに吹き飛ぶ処理（(※)吹き飛ぶ距離、吹き飛ぶ方向）
    //　※正確には吹き飛ぶ距離ではなく、細かい距離を何回移動するか
    protected IEnumerator HitBack(int n, int direction)
    {
        int i = 0;
        while (i < n)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction*0.1f,0.05f,0);
            i++;
        }
        isDown = false;
    }

    //無敵時間を計測（無敵時間）
    IEnumerator CoolTime(float cooltime)
    {
        yield return new WaitForSeconds(cooltime);
        tag = temp;
    }

}
