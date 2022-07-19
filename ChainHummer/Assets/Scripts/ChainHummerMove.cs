using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainHummerMove : MonoBehaviour
{
    [SerializeField]
    GameObject ChainHummer;
    [SerializeField]
    GameObject Player;

    float playerHand = 3;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = Player.transform.position;
        playerPos.x += playerHand;
        //ChainHummer.transform.position = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
