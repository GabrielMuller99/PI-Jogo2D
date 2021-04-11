using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleJogador : MonoBehaviour
{
    Rigidbody2D jogador;
    Animator animacao;
    SpriteRenderer sprite;
    public GameObject posicaoAtaque;

    public float velocidade = 10;
    public float forcaPulo = 400;

    public float velocidadeDeRotacao = 5;

    public float empurraoVertical;
    public float empurraoHorizontal;
    public float duracaoEmpurrao = 1;
    public float contagemEmpurrao;
    public float duracaoInvencibilidade;
    bool invencivel = false;
    bool empurraoDireita;

    public GameObject cenario;
    public GameObject pivot;

    void Start()
    {
        jogador = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 posicaoPlayer = jogador.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            pivot.transform.position = posicaoPlayer;
            SetParent(pivot);
            pivot.transform.Rotate(0, 0, -90);
            DetachFromParent(cenario);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pivot.transform.position = posicaoPlayer;
            SetParent(pivot);
            pivot.transform.Rotate(0, 0, 90);
            DetachFromParent(cenario);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animacao.GetBool("No chão"))
            {
                //jogador.AddForce(new Vector2(0, forcaPulo));
                animacao.SetTrigger("Pulando");
                jogador.velocity = new Vector2(0, forcaPulo);
            }
        }
    }

    private void FixedUpdate()
    {
        float controle = Input.GetAxisRaw("Horizontal");

        if (contagemEmpurrao <= 0)
        {
            jogador.velocity = new Vector2(controle * velocidade, jogador.velocity.y);
        }
        else
        {
            if (empurraoDireita)
            {
                jogador.velocity = new Vector2(empurraoHorizontal, empurraoVertical);
            }
            else
            {
                jogador.velocity = new Vector2(-empurraoHorizontal, empurraoVertical);
            }
            contagemEmpurrao -= Time.deltaTime;
        }

        if (controle == 0)
        {
            animacao.SetBool("Andando", false);
        }
        else
        {
            animacao.SetBool("Andando", true);
            if (controle < 0)
            {
                sprite.flipX = true;
                jogador.transform.localScale = new Vector3(jogador.transform.localScale.x, jogador.transform.localScale.y, -1f);
                posicaoAtaque.transform.localPosition = new Vector3(-1.17f, 0, 0);
            }
            else
            {
                sprite.flipX = false;
                jogador.transform.localScale = new Vector3(jogador.transform.localScale.x, jogador.transform.localScale.y, 1f);
                posicaoAtaque.transform.localPosition = new Vector3(1.17f, 0, 0);
            }
        }
    }

    public void SetParent(GameObject newParent)
    {
        cenario.transform.parent = newParent.transform;

        if (newParent.transform.parent != null)
        {
            Debug.Log("Player's Grand parent: " + cenario.transform.parent.parent.name);
        }
    }

    public void DetachFromParent(GameObject rotacao)
    {
        rotacao.transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                animacao.SetBool("No chão", true);
                break;

            case "Enemy":
                if (!invencivel)
                {
                    StartCoroutine(Invulnerabilidade());
                    contagemEmpurrao = duracaoEmpurrao;
                    if (collision.gameObject.transform.position.x < transform.position.x)
                    {
                        empurraoDireita = true;
                    }
                    else
                    {
                        empurraoDireita = false;
                    }
                }
                break;

            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animacao.SetBool("No chão", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":
                if (!invencivel)
                {
                    StartCoroutine(Invulnerabilidade());
                    contagemEmpurrao = duracaoEmpurrao;
                    if (collision.gameObject.transform.position.x < transform.position.x)
                    {
                        empurraoDireita = true;
                    }
                    else
                    {
                        empurraoDireita = false;
                    }
                }

                break;

            default:
                break;
        }
    }

    IEnumerator Invulnerabilidade()
    {
        invencivel = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        animacao.SetBool("Machucado", true);

        yield return new WaitForSeconds(duracaoInvencibilidade);

        invencivel = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        animacao.SetBool("Machucado", false);
    }
}
