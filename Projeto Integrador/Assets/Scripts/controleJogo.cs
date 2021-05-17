using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controleJogo : MonoBehaviour
{
    [SerializeField] controleJogador jogador;
    [SerializeField] controleInimigo inimigo;
    [SerializeField] GameObject telaDeMorte;
    [SerializeField] GameObject vidas;
    [SerializeField] GameObject morteLoopPrefab;
    [SerializeField] GameObject morteNormalPrefab;

    public static controleJogo singleton;

    public bool possoJogar = true;

    void Start()
    {
        singleton = this;
        jogador.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void MorteLimites()
    {
        Instantiate(morteNormalPrefab, jogador.transform.position, jogador.transform.rotation);
        possoJogar = false;
        StartCoroutine(RecarregarCena());
    }

    public void Morte()
    {
        Instantiate(morteNormalPrefab, jogador.transform.position, jogador.transform.rotation);
        possoJogar = false;
        StartCoroutine(RecarregarCena());
    }

    IEnumerator RecarregarCena()
    {
        controleSons.TocarAudio("morte");
        yield return new WaitForSeconds(2);
        vidas.SetActive(false);
        telaDeMorte.SetActive(true);
        jogador.gameObject.SetActive(false);
    }
}
