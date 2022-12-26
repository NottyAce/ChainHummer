using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : MonoBehaviour
{
    //���݂̈З͔{��
    float atkrate;
    //���݂̐�����΂��{��
    float hitbackrate;
    //�����蔻�肪��������^�O
    string enemyTag = "Enemy";
    //�v���C���[�̃X�e�[�^�X���Q�Ƃ��邽�߂ɃI�u�W�F�N�g��ۑ�
    [SerializeField]
    GameObject player;
    //�����̓����蔻��𑀍�
    Collider2D istrigger;

    void Awake()
    {
        istrigger = GetComponent<Collider2D>();
    }

    //�{����ύX�i�З͔{���A������΂��{���j
    public void ChangeRate(float newrate , float newhitbackrate)
    {
        atkrate = newrate;
        hitbackrate = newhitbackrate;
    }

    //
    void Attack(string enemyname)
    {
        //���������I�u�W�F�N�g���擾
        GameObject enemy = GameObject.Find(enemyname);
        //���������I�u�W�F�N�g�̃X�N���v�g���擾
        Enemy enemy2 = enemy.GetComponent<Enemy>();
        //�v���C���[�̃X�e�[�^�X���擾
        Hero hero = player.GetComponent<Hero>();
        //�X�e�[�^�X�Ɗe��{�����|���ă_���[�W����
        enemy.GetComponent<Enemy>().Damege((int)(atkrate*hero.atk), (int)(hitbackrate* hero.hitback),enemy2.direction * -1,0.5f);
    }

    //�ڐG����
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

    //�����蔻��̐؂�ւ�
    public void IsTrigger(bool flag)
    {
        istrigger.isTrigger = flag;
    }
}
