using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public static bool jogoPausado = false;
    public GameObject menuPausaUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                Resumir();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Resumir()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        jogoPausado = true;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
