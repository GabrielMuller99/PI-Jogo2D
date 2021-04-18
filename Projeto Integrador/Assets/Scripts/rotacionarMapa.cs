using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionarMapa : MonoBehaviour
{
    public GameObject cenario;
    public GameObject pivot;
    public GameObject localizacao;
    public int rotacionar;

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
