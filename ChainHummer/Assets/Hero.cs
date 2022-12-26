using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterManager))]
#endif

//CharacterManager���p��
public class Hero : CharacterManager
{
    //�U���̎�ނɂ��G�̐�����є{��
    [SerializeField]
    float[] hitbackrate;
    //�U���̎�ނɂ��З͂̔{��
    [SerializeField]
    float[] atkrate;
    //�U���ɂ��d�����ԁi�b�j
    [SerializeField]
    float[] atk_motion_length;
    //�ڒn����
    bool isGround;
    //�W�����v�����𔻒�
    bool isJump;
    //�U�������𔻒�
    bool isAttack;
    [SerializeField]
    float jumpPos;
    //�c�����̈ړ����x
    float speed_y;
    //��]�������s���̂Ń��[�J�����ƃO���[�o�����̈Ⴂ�����
    public bool direction2;
    //�n���}�[�̃X�N���v�g���i�[
    [SerializeField]
    GameObject hummer;
    Hummer hummerscript;
    //�d�͂�ۑ�
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
            //��U��
            if (Input.GetKey(KeyCode.J))
            {
                StartCoroutine(AttackCoolTime(0));
            }
            //���U��
            else if (Input.GetKey(KeyCode.I))
            {
                StartCoroutine(AttackCoolTime(1));
            }
            //�����U��
            else if (Input.GetKey(KeyCode.L))
            {
                StartCoroutine(AttackCoolTime(2));
            }

            //���ֈړ�
            if (Input.GetKey(KeyCode.A))
            {
                if (direction2 == true)
                {
                    //������ς����ꍇ�X�v���C�g�̌�����ς���
                    transform.Rotate(new Vector3(0, -180, 0));
                    direction2 = false;
                }
                //�i�s����������
                direction = -1;
            }
            //�E�ֈړ�
            else if (Input.GetKey(KeyCode.D))
            {
                if (direction2 == false)
                {
                    //������ς����ꍇ�X�v���C�g�̌�����ς���
                    transform.Rotate(new Vector3(0, 180, 0));
                    direction2 = true;
                }
                direction = 1;
            }
            //���͂��Ȃ��ꍇ�ړ����Ȃ�
            else
            {
                direction = 0;
            }
        }
        //�s���ł��Ȃ�
        else
        {
            direction = 0;
        }

        //�ݒu������m�F
        isGround = ground.IsGround();
        //�d��
        speed_y = -gravity;
        //�n�ʂɂ���
        if (isGround)
        {
            gravity = 0;
            //�W�����v����
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
        //�W�����v�������x�������̂Ƃ��̏㏸����
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
        //�ړ�
        Move(direction, speed_y);
    }

    //���U���̂Ƃ��̉�]����
    IEnumerator Rotate()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(0, 36, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //�U�����̃v���C���[�̍d�������Ȃǁi�U���̎�� 0�F��,1�F��,2�F�����j
    IEnumerator AttackCoolTime(int number)
    {
        //�U�����ɕύX
        isAttack = true;
        //�n���}�[�ɓ��������ꍇ�̔{����ύX
        hummerscript.ChangeRate(atkrate[number], hitbackrate[number]);
        //�n���}�[�ɓ����蔻���t�^
        hummerscript.IsTrigger(false);
        //�U���̎�ނ��Ƃ̓�����d��
        switch (number)
        {
            //��F���̏�ōd��
            case 0:
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
            //���̏�ŉ�]
            case 1:
                StartCoroutine(Rotate());
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
            //�n���}�[�̈ړ�
            case 2:
                StartCoroutine(Throw(20, false));
                yield return new WaitForSeconds(atk_motion_length[number]);
                break;
        }
        //�n���}�[�̓����蔻�����
        hummerscript.IsTrigger(true);
        //���g���s���\�ɂ���
        isAttack = false;
    }
}
