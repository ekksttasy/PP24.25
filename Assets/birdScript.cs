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
            birbBody.gravityScale = 0;
        }

        if (SceneManager.GetActiveScene().name == "MultiplayerPipes")
        {
            birbBody.gravityScale = 20;
        }

        if (Input.GetKeyDown(KeyCode.Space) && birdIsLive && SceneManager.GetActiveScene().name != "ServerList")
        {
            birbBody.linearVelocity = Vector2.up * flapStrength;
        }
    }

    private void setLogicScript()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        birdIsLive = false;
    }
}
