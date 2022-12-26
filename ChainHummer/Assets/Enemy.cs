using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterManager))]
#endif

//CharacterManager���p��
public class Enemy : CharacterManager
{
    //�v���C���[�I�u�W�F�N�g���i�[
    [SerializeField]
    GameObject player;
    //�����蔻�肪����^�O
    string enemyTag = "Player";

    void FixedUpdate()
    {
        if (isDown == false)
        {
            //�v���C���[�Ƃ̈ʒu�֌W��c��
            Vector2 targeting = (player.transform.position - this.transform.position);
            //�ʒu�֌W�ɂ��X�v���C�g�̔��]�Ɛi�s���������菈��
            //�X�v���C�g�����E��Ώ̂̏ꍇ�ɕK�v
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
            //x�����ɂ̂݃v���C���[��ǂ�
            Move(direction, -gravity);
        }
    }

    //�v���C���[�ɍU������֐��i�����̐i�s�����j
    void Attack(int direction)
    {
        //�v���C���[�̐�����ԕ���
        int direction2 = direction;
        //�v���C���[�̌������琁����ԕ���������
        Hero hero = player.GetComponent<Hero>();
        if (hero.direction2)
        {
            direction2 *= -1;
        }
        //�_���[�W�����Ɛ�����я����i�v���C���[���ōs����j
        hero.Damege(atk, hitback,direction2,0.5f);
    }

    //�v���C���[�Ƃ̓����蔻��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�v���C���[�ƐڐG
        if (collision.collider.tag == enemyTag)
        {
            //�U�����s��
            Attack(direction);
            //�v���C���[�ɘA���U�����Ȃ��悤�ɓG���g�������������
            Damege(0, hitback,direction * -1,0);
        }
    }
}
