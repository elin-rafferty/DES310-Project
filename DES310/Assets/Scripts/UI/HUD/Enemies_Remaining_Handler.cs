using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesRemainingHandler : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] TextMeshProUGUI text;
    private int enemyCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.ChangeEnemyCount.AddListener(EnemyCountChangeResponse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyCountChangeResponse(int change)
    {
        enemyCount += change;
        text.text = "Enemies Remaining: " + enemyCount;
        if (enemyCount == 0)
        {
            eventHandler.PlayerDeath.Invoke();
        }
    }
}
