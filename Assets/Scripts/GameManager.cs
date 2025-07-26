using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float surviveTime = 180f; // 3���i�b�j
    private float timer = 0f;
    private bool isGameOver = false;

    public Text timerText;       // �^�C�}�[�\���pUI
    public GameObject winPanel;  // �������ɕ\�������UI

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

        // �^�C�}�[��UI���X�V
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // 3���o�߂����珟��
        if (timer >= surviveTime)
        {
            GameClear();
        }
    }

    void GameClear()
    {
        isGameOver = true;
        Debug.Log("�N���A�I");
        if (winPanel != null) winPanel.SetActive(true);
    }
}
