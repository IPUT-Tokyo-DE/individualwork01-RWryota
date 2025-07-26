using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float surviveTime = 180f; // 3分（秒）
    private float timer = 0f;
    private bool isGameOver = false;

    public Text timerText;       // タイマー表示用UI
    public GameObject winPanel;  // 勝利時に表示されるUI

    void Start()
    {
        timer = 0f;
        isGameOver = false;
        if (winPanel != null) winPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        timer += Time.deltaTime;
        float remaining = surviveTime - timer;

        // タイマーのUIを更新
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // 3分経過したら勝利
        if (timer >= surviveTime)
        {
            GameClear();
        }
    }

    void GameClear()
    {
        isGameOver = true;
        Debug.Log("クリア！");
        if (winPanel != null) winPanel.SetActive(true);
    }
}
