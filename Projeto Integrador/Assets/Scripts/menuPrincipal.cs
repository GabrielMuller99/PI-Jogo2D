using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    public GameObject menuEscolherUI;
    public GameObject menuPrincipalUI;

    public void Iniciar()
    {
        SceneManager.LoadScene("PrimeiraFase");
    }

    public void IniciarUm()
    {
        SceneManager.LoadScene("PrimeiraFase");
    }

    public void IniciarDois()
    {
        SceneManager.LoadScene("SegundaFase");
    }

    public void IniciarTres()
    {
        SceneManager.LoadScene("TerceiraFase");
    }

    public void EscolherFase()
    {
        menuPrincipalUI.SetActive(false);
        menuEscolherUI.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        menuPrincipalUI.SetActive(true);
        menuEscolherUI.SetActive(false);
    }
}
