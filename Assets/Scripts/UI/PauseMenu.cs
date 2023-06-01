using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] private SettingsMenu settingsMenu;
    private Animator animator;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        animator = GetComponent<Animator>();
        instance = this;
        gameObject.SetActive(false);
    }

    public void ShowScreen()
    {
        Debug.Log("Showing screen");
        gameObject.SetActive(true);
        animator.SetBool("Open", true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void HideScreen()
    {
        Debug.Log("Hiding screen");
        animator.SetBool("Open", false);
        Invoke("DeactivateScreen", 1);
        settingsMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    private void DeactivateScreen()
    {
        gameObject.SetActive(false);
    }

    public void ToggleScreen()
    {
        if (gameObject.activeSelf)
        {
            HideScreen();
        }
        else
        {
            ShowScreen();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
