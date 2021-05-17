using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleCamera : MonoBehaviour
{
    public Transform jogador;
    /*public float distanciaDaCamera = 30.0f;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / distanciaDaCamera);
    }*/

    private void FixedUpdate()
    {
        if (controleJogo.singleton.possoJogar)
        {
            transform.position = new Vector3(jogador.position.x, jogador.position.y, transform.position.z);
        }
    }
}
