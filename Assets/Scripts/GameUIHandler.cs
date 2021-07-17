using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{

    [SerializeField] Text nameAndBestScore;
    private string Name;
    private int bestScore;
    // Start is called before the first frame update
    void Start()
    {
        Name = PersistenceManager.Instance.Name;
        bestScore = PersistenceManager.Instance.GetHighestScore(PersistenceManager.Instance.HighScores);
        nameAndBestScore.text = "Best Score: " + bestScore + " Name: " + Name;
    }

    public void ShowScores()
    {
        SceneManager.LoadScene(2);
    }
}
