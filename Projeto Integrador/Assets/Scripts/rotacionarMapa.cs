using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rotacionarMapa : MonoBehaviour
{
    //public GameObject prefab;
    public GameObject cenario;
    public GameObject pivot;
    public GameObject localizacao;
    public int rotacionar;
    public GameObject[] zonasParaAtivar;
    public GameObject[] zonasParaDesativar;
    GameObject[] troca;

    void Start()
    {
        //Instantiate(prefab, transform.position, transform.rotation);
        //SetParentPrefab(pivot);
    }

    private void Update()
    {
        pivot.transform.position = localizacao.transform.position;
        //prefab.transform.position = localizacao.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                SetParent(pivot);
                DOTween.Init();
                pivot.transform.DORotate(new Vector3(0, 0, rotacionar), 0.5f);
                StartCoroutine(Delay());
                //pivot.transform.Rotate(0, 0, rotacionar);

                break;

            default:
                break;
        }
    }

    /*public void SetParentPrefab(GameObject newParent)
    {
        prefab.transform.parent = newParent.transform;
    }*/

    public void SetParent(GameObject newParent)
    {
        cenario.transform.parent = newParent.transform;
    }

    public void DetachFromParent(GameObject rotacao)
    {
        rotacao.transform.parent = null;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
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
    }
}
