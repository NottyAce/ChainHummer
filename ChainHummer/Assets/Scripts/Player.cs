using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    //[SerializeField]
    //GameObject chainHummer;
    [SerializeField]
    int jumpPower;
    [SerializeField]
    int speed;
    Rigidbody2D playerRigid;
    Transform playerTrans;
    //Rigidbody2D hummerRigid;
    float horizontal;
    bool turnRight;//âEÇ»ÇÁtrue,ç∂Ç»ÇÁfalse

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        //hummerRigid = chainHummer.GetComponent<Rigidbody2D>();
        playerTrans = player.GetComponent<Transform>();
        turnRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Run(horizontal);
        if (Input.GetKeyDown(KeyCode.UpArrow) && !(playerRigid.velocity.y < -0.5f)) {
            Jump();
        }

    }
    private void Turn(float horizontal) {
        if (turnRight == true && horizontal < 0)
        {
            //å¸Ç´îΩì]
        }
        else if (turnRight == false && horizontal > 0)
        {
            //å¸Ç´îΩì]
        }
        else { 
        
        }
    }

    private void Run(float horizontal)
    {
        playerRigid.velocity = new Vector2(horizontal * speed, playerRigid.velocity.y);
    }

    private void Jump() {
        playerRigid.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
    }

    void PlayerAction(Vector2 direction) {
        
    }
}
