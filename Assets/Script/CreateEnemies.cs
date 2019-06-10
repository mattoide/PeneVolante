using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    public static List<GameObject> nemiciAttivi = new List<GameObject>();
    public static List<GameObject> nemiciInattivi = new List<GameObject>();
    public static GameObject nemico1;
    public static GameObject nemico2;
    public static GameObject nemico3;

    private static int enemiesNumber = 10;
    private static int maxInactiveEnemies = 3;

    public static void createEnemies()
    {

        for (int i = 0; i < enemiesNumber; i++)
        {
            GameObject nemicoTemp = (GameObject)Instantiate(GameObject.Find("Nemico"));
            nemiciAttivi.Add(nemicoTemp);
        }



        for (int i = 0; i < enemiesNumber; i++)
        {

            nemiciAttivi[i].GetComponent<Transform>().transform.position = new Vector3(Random.Range(GameManager.leftLimit, GameManager.rightLimit), GameManager.playerPosition.y, GameManager.playerPosition.z + ((i*100)+100));
        }
    }

    public static void manageEnemies()
    {
        if (nemiciInattivi.Count >= maxInactiveEnemies)
        {
            for (int i = 0; i < nemiciInattivi.Count; i++)
            {
                nemiciInattivi[i].SetActive(true);
                Debug.Log("Nemici attivi: " + nemiciAttivi.Count);

                nemiciInattivi[i].GetComponent<Transform>().transform.position = new Vector3(Random.Range(GameManager.leftLimit, GameManager.rightLimit), GameManager.playerPosition.y, nemiciAttivi[nemiciAttivi.Count-1].transform.position.z + (i * 100) + 100);
            }
            nemiciAttivi.AddRange(ShuffleList<GameObject>(nemiciInattivi));

        }

        //Debug.Log("Nemici attivi: " + nemiciAttivi.Count);
        //Debug.Log("Nemici inattivi: " + nemiciInattivi.Count);




    }

    private static List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        Random r = new Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
