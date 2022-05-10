using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject chainHummer;
    [SerializeField]
    int power;
    [SerializeField]
    int speed;
    Rigidbody2D playerRigid;
    Rigidbody2D hummerRigid;
    Vector2 right = new Vector2(1,0);
    Vector2 left = new Vector2(-1, 0);
    Vector2 jump = new Vector2(0,1);
    Vector2 dir = new Vector2(0,0);
    Vector2 nowVec = new Vector2(0,0);
    Vector2 jumppow = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        hummerRigid = chainHummer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = left;
        }
        else {
            //dir = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir = jump;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //dir = Vector2.zero;
        }
        else { 
        
        }
        PlayerAction(dir);
    }

    void PlayerAction(Vector2 direction) {
        Debug.Log("Action!");
        if (direction == right && Mathf.Abs(playerRigid.velocity.x) < speed)
        {
            playerRigid.AddForce(right * power);
        }
        else if (direction == left && Mathf.Abs(playerRigid.velocity.x) < speed)
        {
            playerRigid.AddForce(left * power);
        }
        //else if (direction == Vector2.zero)
        //{
        //    playerRigid.velocity = Vector2.zero;
        //}
        else { 
        
        }

        if (direction == jump) {
            nowVec = playerRigid.velocity;
            jumppow = nowVec + jump; ;
            playerRigid.AddForce(jumppow.normalized * power);
        }
    }
}
