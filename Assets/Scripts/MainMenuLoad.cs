using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuLoad : MonoBehaviour
{
    Manager manager;
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Donkey"))
        {
            manager.saveGame();
            SceneManager.LoadScene("Menu");
        }
        
    }
    private void Update()
    {
        manager.saveGame();
    }
}
