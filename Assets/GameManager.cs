using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float leftLimit;
    public static float rightLimit;
    private Vector3 screenBounds;
    private GameObject playerCamera;
    private GameObject player;

    public static Vector3 playerPosition;


    public static GameManager instance; //Dichiaro un'istanza del GamaManager

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
            return;
        }

        player = GameObject.Find("Player");
        playerCamera = GameObject.Find("PlayerCamera");




        this.adaptToScreen();


        //leftLimit = 0;
        //rightLimit = screenBounds.x + player.GetComponent<Renderer>().bounds.size.x;
        leftLimit = -(player.GetComponent<Renderer>().bounds.size.x * 3);
        rightLimit = screenBounds.x + (player.GetComponent<Renderer>().bounds.size.x * 3);


    }

    void adaptToScreen()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerCamera.transform.position.y, playerCamera.transform.position.z));
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, player.transform.position.y, player.transform.position.z));
        Vector3 v = screenBounds;
        v.x = screenBounds.x / 2;
        v.y = 0;
        player.transform.position = v;
        playerPosition = player.transform.position;


    }

    void resetXCamera()
    {
        Vector3 tempPos = playerCamera.transform.position;
        tempPos.x = screenBounds.x / 2;
        playerCamera.transform.position = tempPos;
    }

    public static void Colpito(GameObject o)
    {


        Debug.Log(CreateEnemies.nemiciInattivi.Count);

        CreateEnemies.nemiciAttivi.Remove(o);
        CreateEnemies.nemiciInattivi.Add(o);
        o.SetActive(false);


    }

    void Start()
    {
        CreateEnemies.createEnemies();
    }

    void Update()
    {     
        resetXCamera();

        playerPosition = player.transform.position;

        CreateEnemies.manageEnemies();


    }
}
