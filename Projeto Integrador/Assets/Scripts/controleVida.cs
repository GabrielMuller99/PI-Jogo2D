using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controleVida : MonoBehaviour
{
    public int vidaJogador;
    [SerializeField] Image[] vidas;
    [SerializeField] controleJogo jogo; 

    private void Start()
    {
        AtualizarVida();
    }

    public void AtualizarVida()
    {
        if (vidaJogador > 0)
        {
            vidas[vidaJogador].enabled = false;
        }
        else
        {
            vidas[vidaJogador].enabled = false;
            jogo.Morte();
        }
    }
}
