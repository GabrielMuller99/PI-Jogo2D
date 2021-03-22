using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleJogador : MonoBehaviour
{
    Rigidbody2D jogador;
    Animator animacao;

    public float velocidade = 10;
    public float forcaPulo = 400;
    public GameObject cenario;
    public GameObject pivot;

    void Start()
    {
        jogador = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
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
                jogador.AddForce(new Vector2(0, forcaPulo));
            }
        }
    }

    private void FixedUpdate()
    {
        float controle = Input.GetAxisRaw("Horizontal");

        jogador.velocity = new Vector2(controle * velocidade, jogador.velocity.y);

        if (controle == 0)
        {
            animacao.SetBool("Andando", false);
        }
        else
        {
            animacao.SetBool("Andando", true);
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
        animacao.SetBool("No chão", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animacao.SetBool("No chão", false);
    }
}
