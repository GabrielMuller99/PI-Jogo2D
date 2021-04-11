using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogadorAtaca : MonoBehaviour
{
    [SerializeField]private float tempoEntreAtaques;
    public float inicioTempoEntreAtaques;

    public Transform posicaoAtaque;
    public LayerMask definicaoInimigo;
    public float distanciaAtaque;
    public int dano;

    void Start()
    {

    }

    void Update()
    {
        if (tempoEntreAtaques <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Collider2D[] danificarInimigos = Physics2D.OverlapCircleAll(posicaoAtaque.position, distanciaAtaque, definicaoInimigo);
                for (int i = 0; i < danificarInimigos.Length; i++)
                {
                    danificarInimigos[i].GetComponent<controleInimigo>().AtualizarVida();
                }
                tempoEntreAtaques = inicioTempoEntreAtaques;
            }
        }
        else
        {
            tempoEntreAtaques -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicaoAtaque.position, distanciaAtaque);
    }
}
