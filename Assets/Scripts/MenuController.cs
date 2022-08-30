using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    SceneManager sceneManager;
    public GameObject[] menus;

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
}
