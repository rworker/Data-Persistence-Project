using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    public List<Text> scoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        PersistenceManager.Instance.HighScores.Sort(); //sorts scores in ascending order
        int scoreCount = PersistenceManager.Instance.HighScores.Count; //gets current count of scores

        scoreTexts[0].text = $"Score 1 : {PersistenceManager.Instance.HighScores[scoreCount - 1]}"; //gets highest score (which is now at the highest index after sorting)

        if (scoreCount >= 2)
        {
            scoreTexts[1].text = $"Score 2 : {PersistenceManager.Instance.HighScores[scoreCount - 2]}";
        }

        if (scoreCount >= 3)
        {
            scoreTexts[2].text = $"Score 3 : {PersistenceManager.Instance.HighScores[scoreCount - 3]}";
        }

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
