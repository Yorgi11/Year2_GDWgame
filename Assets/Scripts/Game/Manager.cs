using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private float maxNumEnemies;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Enemy enemy;

    [SerializeField] private Text scoreText;

    private int currentPoints;

    private float currentNumEnemies;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentPoints;
        if (currentNumEnemies < maxNumEnemies + 4 && maxNumEnemies != 0)
        {
            Instantiate(enemy, spawnPoints[(int)Random.Range(0, spawnPoints.Length)]);
            currentNumEnemies++;
        }
        maxNumEnemies = (int)currentPoints / 500;
    }

    public void RemoveEnemy()
    {
        currentNumEnemies--;
    }
    public void SetPoints(int val)
    {
        currentPoints += val;
    }
}