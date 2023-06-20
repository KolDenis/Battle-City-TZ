using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static public UIManager Instance;

    [SerializeField]
    private TMP_Text m_Lives;
    [SerializeField]
    private TMP_Text m_Enemys;
    [SerializeField]
    private TMP_Text m_Score;
    [SerializeField]
    private TMP_Text m_ResultGame;

    [SerializeField]
    private GameObject m_PanelFinish;

    private void Start()
    {
        Instance = this;
    }

    public void SetLives(int lives)
    {
        m_Lives.text = $"lives: {lives}";
    }

    public void SetEnemys(int enemy)
    {
        m_Enemys.text = $"Enemys: {enemy}";
    }

    public void gameOver(bool win, int score)
    {
        m_PanelFinish.SetActive(true);
        if (win)
        {
            m_ResultGame.text = "Win";
        }
        else
        {
            m_ResultGame.text = "Lose";
        }
        m_Score.text = $"Score: {score}";
    }

    public void restartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
