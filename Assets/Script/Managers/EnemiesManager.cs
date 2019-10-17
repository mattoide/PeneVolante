using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    public List<GameObject> nemiciAttivi = new List<GameObject>();
    public List<GameObject> nemiciInattivi = new List<GameObject>();
    public GameObject nemico1;
    public GameObject nemico2;
    public GameObject nemico3;

    private int enemiesNumber = 10;
    private int maxInactiveEnemies = 3;

    public void CreateEnemies()
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

    public void ManageEnemies()
    {
        if (nemiciInattivi.Count >= maxInactiveEnemies)
        {
            float lastActiveEnemyZ = nemiciAttivi[nemiciAttivi.Count - 1].transform.position.z;
            for (int i = 0; i < nemiciInattivi.Count; i++)
            {
                nemiciInattivi[i].SetActive(true);
                nemiciInattivi[i].GetComponent<Transform>().transform.position = new Vector3(Random.Range(GameManager.leftLimit, GameManager.rightLimit), GameManager.playerPosition.y, lastActiveEnemyZ + ((i * 100) + 100));
            }
            nemiciAttivi.AddRange(ShuffleList<GameObject>(nemiciInattivi));
            nemiciInattivi.Clear();

        }
    }


    private List<E> ShuffleList<E>(List<E> inputList)
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
    
}
