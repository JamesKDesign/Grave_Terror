using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossKilled : MonoBehaviour
{
    public UnityEvent triggers;
    public GameObject winGameScreen;
    public ScoreBoard scoreBoard;

    private void Awake()
    {
        winGameScreen.SetActive(false);
    }

    private void Update()
    {
        if (GetComponent<EnemyHealth>().currentHealth <= 0.0f)
        {
            triggers.Invoke();
            Destroy(this);

            winGameScreen.SetActive(true);
            scoreBoard.scoreBoard.SetActive(true);
        }
    }
}