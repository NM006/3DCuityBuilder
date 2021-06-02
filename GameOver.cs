using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RestartButton();
        }
    }
    public void RestartButton()
    {
        Debug.Log("Restart angeklickt");
        SceneManager.LoadScene("Game");
    }
}
