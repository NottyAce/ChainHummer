using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    GameObject Player;
    Vector3 ScreenRight;
    Vector3 ScreenLeft;
    Rigidbody2D mainCameraRigid;
    Rigidbody2D playerRigid;
    float xps;//?????????W

    // Start is called before the first frame update
    void Start()
    {
        mainCameraRigid = MainCamera.GetComponent<Rigidbody2D>();
        playerRigid = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        ScreenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        xps = Player.transform.position.x - ScreenLeft.x;
        CameraMove();
    }
    void CameraMove()
    {

        if (xps > (ScreenRight.x - ScreenLeft.x) / 2 && playerRigid.velocity.x > 0)
        {
            mainCameraRigid.velocity = new Vector3(playerRigid.velocity.x, 0, 0);
        }
        else if (xps < (ScreenRight.x - ScreenLeft.x) / 6 && playerRigid.velocity.x < 0)
        {
            mainCameraRigid.velocity = new Vector3(playerRigid.velocity.x, 0, 0);
        }
        else if (playerRigid.velocity.x == 0)
        {
            mainCameraRigid.velocity = Vector3.zero;
        }
        else
        {

        }
    }

}
