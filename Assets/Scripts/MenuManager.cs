using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void NextLevel()
    {
        GameManager.NextLevel();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
