using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject Donkey;
    [SerializeField] CinemachineVirtualCamera Vcam;
    [SerializeField] bool StartCharIsCat;
    MainInput Input;
    AudioSource MusicSource;
    private void Start()
    {
       
        GameManager.Vcam = Vcam;
        GameManager.Cat = Cat;
        GameManager.Donkey = Donkey;
        GameManager.IsCatAlone = false;
        GameManager.IsCat = StartCharIsCat;
        GameManager.Manager = this;
        if (StartCharIsCat)
        {
            GameManager.IsCat = true;
        }
        else
        {
            GameManager.IsCat = false;
        }
        Input = new MainInput();
        Input.Enable();
        MusicSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        loadGame();
    }
    private void Update()
    {
        if (Input.MoveHorse.Restart.WasPerformedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void NextLevel()
    {
        GameManager.NextLevel();
    }
    public void Restart()
    {
        GameManager.IsCat = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void saveGame()
    {
        PlayerPrefs.SetFloat("MusicVolume", Settings.MusicVolume);
        PlayerPrefs.SetFloat("MasterVolume", Settings.MasterVolume);
        MusicSource.volume = Settings.MusicVolume;
        AudioListener.volume = Settings.MasterVolume;
        PlayerPrefs.Save();
    }

    public void loadGame()
    {
        Settings.MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        Settings.MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        MusicSource.volume = Settings.MusicVolume;
        AudioListener.volume = Settings.MasterVolume;
    }
}

public static class Settings
{
    public static float MusicVolume;
    public static float MasterVolume;
}


