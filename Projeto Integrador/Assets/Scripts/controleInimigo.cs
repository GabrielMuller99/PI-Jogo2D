using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleInimigo : MonoBehaviour
{
    protected Rigidbody2D inimigo;
    protected SpriteRenderer sprite;

    public float velocidade = 2;
    public Transform[] limites;
    public int posicaoLimites;
    public float distancia = 0.5f;

    void Start()
    {
        inimigo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void FixedUpdate()
    {
        Vector2 direcao = limites[posicaoLimites].position - transform.position;
        direcao.Normalize();

        if (Vector2.Distance(limites[posicaoLimites].position, transform.position) <= distancia)
        {
            posicaoLimites++;
            if (posicaoLimites >= limites.Length)
            {
                posicaoLimites = 0;
            }
        }

        inimigo.velocity = direcao * velocidade;

        if (direcao.x >= 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    public virtual void Update()
    {
        
    }
}
