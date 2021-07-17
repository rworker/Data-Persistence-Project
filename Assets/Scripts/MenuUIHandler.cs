using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text warningText;

    public void StartGame()
    {
        if (nameText.text != "")
        {
            StoreName();
            SceneManager.LoadScene(1);
        }
        else
            warningText.gameObject.SetActive(true);
    }

    public void StoreName()
    {
        PersistenceManager.Instance.Name = nameText.text;
    }
}
