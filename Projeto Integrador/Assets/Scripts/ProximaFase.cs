using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximaFase : MonoBehaviour
{
    [SerializeField] GameObject vitoria;
    [SerializeField] GameObject vida;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        vitoria.SetActive(true);
        vida.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
