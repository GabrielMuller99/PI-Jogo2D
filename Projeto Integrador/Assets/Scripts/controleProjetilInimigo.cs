using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleProjetilInimigo : MonoBehaviour
{
    Rigidbody2D tiro;
    SpriteRenderer sprite;
    public float velocidade;

    [SerializeField] int danoInimigo;
    [SerializeField] controleVida healthController;
    [SerializeField] inimigoAtira atirador;

    void Start()
    {
        tiro = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        healthController = FindObjectOfType<controleVida>();
        atirador = FindObjectOfType<inimigoAtira>();
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if (atirador.direita)
        {
            tiro.velocity = new Vector2(velocidade, 0);
            sprite.flipX = true;
            
        }
        else if (atirador.esquerda)
        {
            tiro.velocity = new Vector2(-velocidade, 0);
            sprite.flipX = false;
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
