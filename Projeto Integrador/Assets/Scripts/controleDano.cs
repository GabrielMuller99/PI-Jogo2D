using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDano : MonoBehaviour
{
    [SerializeField] float danoInimigo;
    [SerializeField] controleVida healthController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Debug.Log("Dano");
                Dano();

                break;

            default:
                break;
        }
    }

    void Dano()
    {
        healthController.vidaJogador -= danoInimigo;
        healthController.AtualizarVida();
    }
}
