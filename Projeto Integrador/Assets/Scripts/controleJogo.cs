using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controleJogo : MonoBehaviour
{
    [SerializeField] controleJogador jogador;
    [SerializeField] controleInimigo inimigo;
    [SerializeField] GameObject telaDeMorte;

    void Start()
    {
        jogador.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void Morte()
    {
        telaDeMorte.SetActive(true);
        jogador.gameObject.SetActive(false);
        StartCoroutine(RecarregarCena());
    }

    IEnumerator RecarregarCena()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
