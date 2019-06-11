using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static float leftLimit;
    public static float rightLimit;
    private Vector3 screenBounds;
    private GameObject playerCamera;
    private GameObject player;
    private GameObject playerController;

    public static Vector3 playerPosition;
    private float larghezzaDick;



    private void Awake()
    {


        player = GameObject.Find("Dick");
        playerController = GameObject.Find("Player");
        playerCamera = GameObject.Find("PlayerCamera");
        larghezzaDick = GameObject.Find("Larghezza").GetComponent<Renderer>().bounds.size.x;




        this.adaptToScreen();

        //leftLimit = -(player.GetComponent<Renderer>().bounds.size.x * 3);
        //rightLimit = screenBounds.x + (player.GetComponent<Renderer>().bounds.size.x * 3);



        leftLimit = -(larghezzaDick);
        rightLimit = screenBounds.x + (larghezzaDick);

        print(leftLimit);
        print(rightLimit);
        print(screenBounds);

    }

    void adaptToScreen()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerCamera.transform.position.y, playerCamera.transform.position.z));
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerController.transform.position.y, playerController.transform.position.z));
        Vector3 v = screenBounds;
        v.x = screenBounds.x / 2;
        v.y = 0;
        playerController.transform.position = v;
        playerPosition = playerController.transform.position;


    }

    void resetXCamera()
    {
        Vector3 tempPos = playerCamera.transform.position;
        tempPos.x = screenBounds.x / 2;
        playerCamera.transform.position = tempPos;
    }

    public void Colpito(GameObject o)
    {

       
            o.SetActive(false);
            EnemiesManager.Instance.nemiciAttivi.Remove(o);
            EnemiesManager.Instance.nemiciInattivi.Add(o);

        //TODO: controllare il tipo di nemico colpito fare punto o togliere punto


        Debug.Log("Nemici attivi: " + EnemiesManager.Instance.nemiciAttivi.Count);
        Debug.Log("Nemici INattivi: " + EnemiesManager.Instance.nemiciInattivi.Count);

    }

    public void Mancato(GameObject o)
    {
        o.SetActive(false);
        EnemiesManager.Instance.nemiciAttivi.Remove(o);
        EnemiesManager.Instance.nemiciInattivi.Add(o);
    }

    void Start()
    {
        EnemiesManager.Instance.CreateEnemies();
    }

    void Update()
    {

        resetXCamera();

        playerPosition = playerController.transform.position;

        EnemiesManager.Instance.ManageEnemies();


    }
}
