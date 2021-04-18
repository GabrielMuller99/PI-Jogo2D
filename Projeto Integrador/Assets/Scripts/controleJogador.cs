using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleJogador : MonoBehaviour
{
    Rigidbody2D jogador;
    Animator animacao;
    SpriteRenderer sprite;

    public float velocidade = 10;
    public float forcaPulo = 400;
    public float forcaAtaque = 10;

    public GameObject marcador;
    private GameObject inimigoMarcado;

    public float velocidadeDeRotacao = 5;

    public float empurraoVertical;
    public float empurraoHorizontal;
    public float duracaoEmpurrao = 1;
    public float contagemEmpurrao;
    public float duracaoInvencibilidade;
    bool invencivel = false;
    bool empurraoDireita;

    /*public GameObject cenario;
    public GameObject pivot;*/

    void Start()
    {
        jogador = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MarcarInimigoMaisProximo();
        Vector2 posicaoPlayer = jogador.position;

        /*if (Input.GetKeyDown(KeyCode.E))
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
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animacao.GetBool("No chão"))
            {
                jogador.AddForce(new Vector2(0, forcaPulo));
                animacao.SetBool("Pulando", true);
                animacao.SetBool("Caindo", false);
                //jogador.velocity = new Vector2(0, forcaPulo);
            }
            else
            {
                if (inimigoMarcado != null)
                {
                    StartCoroutine(Atacando());
                    animacao.SetBool("Pulando", false);
                    animacao.SetBool("Atacando", true);
                    var direcao = (inimigoMarcado.transform.position - transform.position).normalized;
                    //jogador.AddForce(direcao * forcaAtaque);
                    jogador.velocity = new Vector2(direcao.x, direcao.y) * forcaAtaque;
                }
            }
        }

        if (jogador.velocity.y < -0.1f && animacao.GetBool("Atacando") == false)
        {
            animacao.SetBool("Caindo", true);
            animacao.SetBool("Pulando", false);
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
            }
            else
            {
                sprite.flipX = false;
                jogador.transform.localScale = new Vector3(jogador.transform.localScale.x, jogador.transform.localScale.y, 1f);
            }
        }
    }

    public void MarcarInimigoMaisProximo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject alvo = null;
        Vector3 posicaoAtual = transform.position;
        Vector3 diferenca;

        float distancia = 10f;
        float magnitude;

        foreach (GameObject inimigo in inimigos)
        {
            diferenca = inimigo.transform.position - posicaoAtual;
            magnitude = diferenca.sqrMagnitude;

            if (magnitude < distancia)
            {
                alvo = inimigo;
                distancia = magnitude;
            }
        }

        if (alvo != null)
        {
            if (inimigoMarcado == null || inimigoMarcado.GetInstanceID() != alvo.GetInstanceID())
            {
                inimigoMarcado = alvo;
                marcador.transform.position = alvo.transform.position;
                ParentearMarcador(alvo);
            }
        }
    }

    public void ParentearMarcador(GameObject newParent)
    {
        marcador.transform.parent = newParent.transform;
    }

    /*public void SetParent(GameObject newParent)
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
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                animacao.SetBool("No chão", true);
                animacao.SetBool("Caindo", false);
                animacao.SetBool("Atacando", false);
                animacao.SetBool("Pulando", false);
                break;

            case "Enemy":
                if (!invencivel && animacao.GetBool("Atacando") == false)
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

                else if (animacao.GetBool("Atacando") == true)
                {
                    jogador.velocity = Vector2.zero;
                    jogador.angularVelocity = 0f;
                    jogador.velocity = Vector2.up * 6f;
                    inimigoMarcado = null;
                    collision.gameObject.SetActive(false);
                    animacao.SetBool("Atacando", false);
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

    IEnumerator Atacando()
    {
        jogador.tag = "Untagged";
        yield return new WaitForSeconds(0.32f);
        jogador.tag = "Player";
    }
}
