using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleJogador : MonoBehaviour
{
    Rigidbody2D jogador;
    Animator animacao;
    SpriteRenderer sprite;
    AudioSource idle;
    [SerializeField] controleJogo jogo;
    [SerializeField] controleVida healthController;
    [SerializeField] ParticleSystem[] fogo;

    public GameObject girando;

    public float velocidade = 10;
    public float forcaPulo = 400;
    public float forcaAtaque = 10;

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
        idle = GetComponent<AudioSource>();
    }

    void Update()
    {
        MarcarInimigoMaisProximo();
        Vector2 posicaoPlayer = jogador.position;

        if (Input.GetKeyDown(KeyCode.Space) && jogo.possoJogar)
        {
            controleSons.TocarAudio("pulo");

            if (animacao.GetBool("No chão"))
            {
                jogador.AddForce(new Vector2(0, forcaPulo));
                animacao.SetBool("Pulando", true);
                animacao.SetBool("Caindo", false);
            }
            else
            {
                if (inimigoMarcado != null)
                {
                    StartCoroutine(Atacando());
                    animacao.SetBool("Pulando", false);
                    animacao.SetBool("Atacando", true);
                    var direcao = (inimigoMarcado.transform.position - transform.position).normalized;
                    jogador.velocity = new Vector2(direcao.x, direcao.y) * forcaAtaque;
                }
            }
        }

        if (jogador.velocity.y < -0.1f && animacao.GetBool("Atacando") == false)
        {
            animacao.SetBool("Caindo", true);
            animacao.SetBool("Pulando", false);
        }

        if (!jogo.possoJogar)
        {
            jogador.velocity = Vector2.zero;
            animacao.SetBool("Morto", true);
        }
    }

    private void FixedUpdate()
    {
        float controle = Input.GetAxisRaw("Horizontal");

        if (contagemEmpurrao <= 0 && jogo.possoJogar)
        {
            if (!girando.GetComponent<AudioSource>().isPlaying && !animacao.GetBool("Pulando") && !animacao.GetBool("Machucado") && !animacao.GetBool("Caindo"))
            {
                girando.GetComponent<AudioSource>().Play();
            }
            jogador.velocity = new Vector2(controle * velocidade, jogador.velocity.y);
        }
        else if (jogo.possoJogar)
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
            girando.GetComponent<AudioSource>().Stop();
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
            }
        }
    }

    /*public void SetParent(GameObject newParent)
    {
        fogo.transform.parent = newParent.transform;
    }*/

    /*public void DetachFromParent(GameObject rotacao)
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
                if (!invencivel && animacao.GetBool("Atacando") == false && healthController.vidaJogador > 0)
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
                if (!invencivel && healthController.vidaJogador > 0)
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

            case "Respawn":
                sprite.enabled = false;
                jogo.MorteLimites();
                break;

            case "Finish":
                idle.Pause();
                gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    IEnumerator Invulnerabilidade()
    {
        controleSons.TocarAudio("dano");
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
        controleSons.TocarAudio("espinho");
        jogador.tag = "Untagged";
        yield return new WaitForSeconds(0.4f);
        jogador.tag = "Player";
    }
}
