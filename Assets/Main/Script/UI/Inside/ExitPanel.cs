using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitPanel : Window
{
    public GameObject mainMenu;
    public Action onMainMenu;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        mainMenu.GetComponent<Button>().onClick.AddListener(MainMenu);
    }

    void MainMenu()
    {
        if (onMainMenu != null)
        {
            onMainMenu();
        }
    }
}
