using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public static class GameManager 
{
    static bool _IsCat;
    public static GameObject Donkey;
    public static GameObject Cat;
    public static CinemachineVirtualCamera Vcam;
    public static Manager Manager;
    public static bool HasWon = false;
    public static bool IsCatAlone;
    public static bool IsCat
    {
        get { return _IsCat; }
        set 
        { 
            _IsCat = value;
            if (value)
            {
                //turn the cat on
                //donkey off
                Donkey.GetComponent<Horse>().enabled = false;
                Cat.GetComponent<Cat>().enabled = true;
                Cat.GetComponent<Collider2D>().enabled = true;
                Vcam.Follow = Cat.transform;
                Donkey.GetComponent<FixedJoint2D>().enabled = false;
                Donkey.GetComponentInChildren<Animator>().SetFloat("Speed", 0);
                Donkey.GetComponentInChildren<Animator>().SetBool("IsInAir", false);
                Donkey.GetComponent<AudioSource>().Pause();
                for (int i = 0; i < Cat.GetComponentsInChildren<SpriteRenderer>().Length; i++)
                {
                    Cat.GetComponentsInChildren<SpriteRenderer>()[i].enabled = true;
                }
            }
            else
            {
                //turn the cat off
                //donkey on
                Cat.GetComponent<Cat>().enabled = false;
                Donkey.GetComponent<Horse>().enabled = true;
                Vcam.Follow = Donkey.transform;
                if (!IsCatAlone)
                {
                    Cat.GetComponent<Collider2D>().enabled = false;
                    Donkey.GetComponent<Horse>().ChangeSpriteToCat();
                    Donkey.GetComponent<FixedJoint2D>().enabled = true;
                    for (int i = 0; i < Cat.GetComponentsInChildren<SpriteRenderer>().Length; i++)
                    {
                        Cat.GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
                    }
                }
                
            }
        }
    }
    static public void Win()
    {
        if (!IsCatAlone)
        {
            Donkey.GetComponent<Horse>().enabled = false;
            Cat.GetComponent<Cat>().enabled = false;
            Donkey.GetComponentInChildren<Animator>().SetFloat("Speed", 0f);
            Donkey.GetComponentInChildren<Animator>().SetBool("IsInAir", false);
            Donkey.GetComponent<AudioSource>().Pause();
            HasWon = true;
            NextLevel();
        }
       
    }

    static public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    static public void Restart()
    {
        Manager.Restart();
    }
    
}
