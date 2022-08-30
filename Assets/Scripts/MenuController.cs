using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuController : MonoBehaviour
{

    
    public GameObject[] menus;
    public TextAsset ranSen;

    public void ManageUI(int active)
    {
        
        if (active > menus.Length)
        {
            Debug.Log("uwu");
            return;

        }
        for (int i = 0; i < menus.Length; i++)
        {
            if (i != active - 1)
            {
                menus[i].SetActive(false);
                break;
            }
        }
        menus[active - 1].SetActive(true);

    }
    public void LoadScene(int a)
    {
        switch (a)
        {
            default:
                SceneManager.LoadScene("MainMenu");
                break;
            case 1:
                SceneManager.LoadScene("Level1");
                break;
        }
        
    }
    
     
    
}
