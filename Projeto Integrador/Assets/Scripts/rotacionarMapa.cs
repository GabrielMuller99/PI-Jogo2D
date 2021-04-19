using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionarMapa : MonoBehaviour
{
    public GameObject cenario;
    public GameObject pivot;
    public GameObject localizacao;
    public int rotacionar;
    public GameObject[] zonasParaAtivar;
    public GameObject[] zonasParaDesativar;
    GameObject[] troca;

    void Start()
    {
        
    }

    private void Update()
    {
        pivot.transform.position = localizacao.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                SetParent(pivot);
                pivot.transform.Rotate(0, 0, rotacionar);
                DetachFromParent(cenario);
                rotacionar *= -1;

                foreach (GameObject ativar in zonasParaAtivar)
                {
                    ativar.SetActive(true);
                }
                foreach (GameObject desativar in zonasParaDesativar)
                {
                    desativar.SetActive(false);
                }

                troca = zonasParaAtivar;
                zonasParaAtivar = zonasParaDesativar;
                zonasParaDesativar = troca;
                break;

            default:
                break;
        }
    }

    public void SetParent(GameObject newParent)
    {
        cenario.transform.parent = newParent.transform;
    }

    public void DetachFromParent(GameObject rotacao)
    {
        rotacao.transform.parent = null;
    }
}
