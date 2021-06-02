using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
       
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ChangeColour()
    {
       
    }
    
    
}
