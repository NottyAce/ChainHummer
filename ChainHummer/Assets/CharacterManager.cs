using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hero�N���X��Enemy�N���X���p������N���X
public class CharacterManager : MonoBehaviour
{
    //�ʒu
    //[SerializeField]
    //protected float[] position;
    //�ő�̗�
    [SerializeField]
    protected int maxhp;
    //�̗�
    [SerializeField]
    protected int hp;
    //�ړ����x(��)
    [SerializeField]
    protected float speed;
    //�W�����v�̍��x����
    [SerializeField]
    protected float max_height;
    //�U����
    [SerializeField]
    public int atk;
    //�h���
    [SerializeField]
    protected int def;
    //�U���𓖂Ă��Ƃ��ɑ���𐁂���΂�����
    //�@�����m�ɂ͐�����ԋ����ł͂Ȃ��A�ׂ�������������ړ����邩
    [SerializeField]
    public int hitback = 10;
    //���g�̐�����тɂ���(0�`1)�傫���Ɣ�΂Ȃ�
    [SerializeField]
    protected float blowrate;
    //�d��
    [SerializeField]
    protected float gravity;
    //�W�����v�̏㏸���x
    [SerializeField]
    protected float jumpspeed;
    //���g�̕�������
    protected Rigidbody2D rb = null;
    //�ڒn����̃X�N���v�g
    [SerializeField]
    protected CheckGround ground;
    //�U�����󂯂Ă���Œ����ǂ��� true���ƍs�����ł��Ȃ��Ȃ�
    protected bool isDown = false;
    //���g�̃^�O��ύX���邽�߂̃^�O�̈ꎞ�ۑ�
    string temp;
    //��΂��I�u�W�F�N�g���i�[
    [SerializeField]
    GameObject throwobject;
    //�i�s�����i-1,0,1�j
    public int direction;
    //��ѓ���̑��x
    [SerializeField]
    float throwspeed;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //�L�����N�^�[���ړ�������֐��i�i�s�����A�c�����̑����j
    public void Move(float direction,float speed_y)
    {
        rb.velocity = new Vector2(speed * direction, speed_y);
    }

    //�U�������������Ƃ��̏����i�_���[�W�A(��)������ԋ����A������ԕ����A���G���ԁj
    //�@�����m�ɂ͐�����ԋ����ł͂Ȃ��A�ׂ�������������ړ����邩
    public void Damege(int damege, int knockbacklength, int direction,float cooltime)
    {
        if (isDown == false)
        {
            //�_���[�W����
            isDown = true;
            if (damege - def >= 0)
            {
                hp -= damege - def;
                if (hp <= 0)
                {
                    //���S����
                }
            }

            //�q�b�g�o�b�N����
            StartCoroutine(HitBack((int)(knockbacklength * (1 - blowrate)),direction));
            
            //�A���q�b�g��h�����߂̖��G����
            //�^�O��ύX���邱�Ƃœ����蔻�肪�������Ȃ�
            temp = tag;
            tag = "Untagged";
            StartCoroutine(CoolTime(cooltime));
        }
    }

    //��ѓ���𓊂��鏈���i�����鋗���A�G���ǂ����j
    //  �������蔻���_���[�W�����ł͂Ȃ�
    protected IEnumerator Throw(int length, bool isenemy)
    {
        //���������������
        //�v���C���[�ƓG�Ői�s�����̌��ߕ����قȂ邽�߁A�v���C���[�ƓG�𔻕�
        int direction = -1;
        if (isenemy)
        {
            direction = this.direction;
        }

        //�����鏈��
        int i = 0;
        while (i < length)
        {
            yield return new WaitForSeconds(0.01f);
            throwobject.transform.Translate(direction * throwspeed, 0, 0);
            i++;
        }

        //�v���C���[�̃n���}�[���茳�ɖ߂��ė��鏈��
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

    //�U�����ꂽ�Ƃ��ɐ�����ԏ����i(��)������ԋ����A������ԕ����j
    //�@�����m�ɂ͐�����ԋ����ł͂Ȃ��A�ׂ�������������ړ����邩
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

    //���G���Ԃ��v���i���G���ԁj
    IEnumerator CoolTime(float cooltime)
    {
        yield return new WaitForSeconds(cooltime);
        tag = temp;
    }

}
