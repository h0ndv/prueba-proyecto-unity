using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool CLICK_LOAD_GAME;
    public string CLICK_SCENE_NAME;
    public bool OPEN_MENU_ON_M;

    public void LoadJuego()
    {
        SceneManager.LoadScene("Juego");
    }

    public void LoadInstrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    public void LoadCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (OPEN_MENU_ON_M && Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu");
        }

        if (CLICK_LOAD_GAME)
        {
            if (Input.anyKeyDown)
            {
                if (string.IsNullOrEmpty(CLICK_SCENE_NAME) == false)
                {
                    SceneManager.LoadScene(CLICK_SCENE_NAME);
                }
                else
                {
                    SceneManager.LoadScene("Juego");
                }
            }
        }
    }
}
