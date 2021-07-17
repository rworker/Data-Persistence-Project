using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject ScoreButton;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public int GetLowestScorePos(List<int> scores) //gets index of lowest score
    {
        int lowestScore = scores[0];
        int lowestScorePos = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] < lowestScore)
            {
                lowestScore = scores[i];
                lowestScorePos = i;
            }
        }
        return lowestScorePos;
    }


    public void GameOver()
    {
        m_GameOver = true;
        if (PersistenceManager.Instance.HighScores.Count >= 3) //checks if high score count is greater or equal to 3
        {
            int lowestScore = PersistenceManager.Instance.GetLowestScore(PersistenceManager.Instance.HighScores);
            if (lowestScore < m_Points) //checks if the lowest score is less than the score of the current game
            {
                int lowestScoreIndex = GetLowestScorePos(PersistenceManager.Instance.HighScores);
                PersistenceManager.Instance.HighScores[lowestScoreIndex] = m_Points; //replaces lowest score with the current game's score at the index of the lowest score
            }
        }
        else //adds current score to list of high scores if high score list count is less than 3
            PersistenceManager.Instance.HighScores.Add(m_Points);

        PersistenceManager.Instance.SaveScores(); //saves high scores when game is over
        GameOverText.SetActive(true);
        ScoreButton.SetActive(true);

    }
}
