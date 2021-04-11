using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolingTiroInimigo : MonoBehaviour
{
    public GameObject objetoParaInstanciar;
    public int comecaInstanciado = 2;

    public List<GameObject> listaDeObjetos;

    public bool podeAumentar = true;

    void Start()
    {
        listaDeObjetos = new List<GameObject>();

        for (int i = 0; i < comecaInstanciado; i++)
        {
            listaDeObjetos.Add(Instantiate(objetoParaInstanciar));
            listaDeObjetos[i].SetActive(false);
        }
    }

    public GameObject PegaObjeto()
    {
        for (int i = 0; i < listaDeObjetos.Count; i++)
        {
            if (!listaDeObjetos[i].activeInHierarchy)
            {
                return listaDeObjetos[i];
            }
        }

        if (!podeAumentar)
        {
            return null;
        }

        listaDeObjetos.Add(Instantiate(objetoParaInstanciar));
        return listaDeObjetos[listaDeObjetos.Count - 1];
    }
}
