using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controleVida : MonoBehaviour
{
    public float vidaJogador;
    [SerializeField] Text vidaTexto;
    [SerializeField] controleJogo jogo; 

    private void Start()
    {
        AtualizarVida();
    }

    public void AtualizarVida()
    {
        if (vidaJogador > 0)
        {
            vidaTexto.text = vidaJogador.ToString("0");
        }
        else
        {
            jogo.Morte();
        }
    }
}
