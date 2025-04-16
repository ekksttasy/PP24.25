using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birdScript : MonoBehaviour
{
    public Rigidbody2D birbBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsLive = true;
    public bool inLobby = false;
    public bool inMultiplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SinglePlayerPipes")
        {
            setLogicScript();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ServerList")
        {
            inLobby = true;
            birbBody.gravityScale = 10;
        }else if (SceneManager.GetActiveScene().name == "MultiplayerPipes")
        {
            inMultiplayer = true;
            birbBody.gravityScale = 20;
        } else
        {
            inLobby = false;
            inMultiplayer = false;

            if (Input.GetKeyDown(KeyCode.Space) && birdIsLive)
            {
                birbBody.linearVelocity = Vector2.up * flapStrength;
            }
        }
    }

    public static void ResetBirdPosition()
    {
        //TODO: fix birb position reset
        //birbBody.transform.position = Vector2.zero;
    }

    private void setLogicScript()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(birbBody.IsTouchingLayers(8))
        {
            //bounce
            birbBody.linearVelocity = Vector2.up * (flapStrength/4);
        }
        else
        {
            // dead birb (touched pipes)
            logic.GameOver();
            birdIsLive = false;
        }
    }
}
