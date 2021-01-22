using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Menus
    public GameObject Mainmenu;
    public GameObject OptionsMenu;

    // Play game
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    // Display Options
    public void Options()
    {
        Mainmenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
    }
}
