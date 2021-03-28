using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoAtira : controleInimigo
{
    [Header("Controles do atirador")]
    public GameObject tiro;
    public Transform alvo;
    public Transform miraDireita;
    public Transform miraEsquerda;
    public float cadencia;
    [SerializeField] float delay;
    bool atirar = false;
    bool direita;
    bool esquerda;

    void Start()
    {
        inimigo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        switch (atirar)
        {
            case true:
                delay -= Time.deltaTime;

                if (esquerda == true && delay < 0)
                {
                    Instantiate(tiro, miraEsquerda.position, miraEsquerda.rotation);
                    delay = cadencia;
                }
                else if (direita == true && delay < 0)
                {
                    Instantiate(tiro, miraDireita.position, miraDireita.rotation);
                    delay = cadencia;
                }
                break;

            default:
                break;
        }
    }

    public override void FixedUpdate()
    {
        if (alvo == null)
        {
            base.FixedUpdate();
        }
        else
        {
            Vector2 lado = alvo.position - transform.position;
            lado.Normalize();
            inimigo.velocity = Vector2.zero;

            if (lado.x >= 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            alvo = collision.transform;
            atirar = true;

            if (alvo.position.x < transform.position.x)
            {
                Debug.Log("Esquerda");
                esquerda = true;
                direita = false;
            }
            else
            {
                Debug.Log("Direita");
                esquerda = false;
                direita = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alvo = null;
            atirar = false;
            esquerda = false;
            direita = false;
        }
    }
}
