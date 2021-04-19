using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            sprite.flipX = false;
            inimigo.transform.localScale = new Vector3(inimigo.transform.localScale.x, inimigo.transform.localScale.y, 1f);
        }
        else
        {
            sprite.flipX = true;
            inimigo.transform.localScale = new Vector3(inimigo.transform.localScale.x, inimigo.transform.localScale.y, -1f);
        }
    }

    public virtual void Update()
    {
        
    }
}
