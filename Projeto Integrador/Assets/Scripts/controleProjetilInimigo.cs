using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleProjetilInimigo : MonoBehaviour
{
    Rigidbody2D tiro;
    public float velocidade;

    [SerializeField] float danoInimigo;
    [SerializeField] controleVida healthController;
    [SerializeField] inimigoAtira atirador;

    void Start()
    {
        tiro = GetComponent<Rigidbody2D>();
        healthController = FindObjectOfType<controleVida>();
        atirador = FindObjectOfType<inimigoAtira>();
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if (atirador.direita)
        {
            tiro.velocity = new Vector2(velocidade, 0);
        }
        else if (atirador.esquerda)
        {
            tiro.velocity = new Vector2(-velocidade, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        Destroy(gameObject);
    }
}
