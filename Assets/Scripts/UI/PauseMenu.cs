using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] private SettingsMenu settingsMenu;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        gameObject.SetActive(false);
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void ToggleScreen()
    {
        if (gameObject.activeSelf)
        {
            HideScreen();
        } else
        {
            ShowScreen();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
