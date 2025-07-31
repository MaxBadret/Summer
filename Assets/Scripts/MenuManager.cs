using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private GameObject menuPanel;
    private bool isInMenu = false;
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            isInMenu = !isInMenu;

        if (isInMenu)
        {
            ShowMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    private void ShowMenu()
    {
        mouseManager.ChangeUseMouse(false);
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        isInMenu = false;
        mouseManager.ChangeUseMouse(true);
        menuPanel.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        mouseManager.ChangeUseMouse(true);
        SceneManager.LoadScene("Menu");
    }
}
