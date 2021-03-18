using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleJogador : MonoBehaviour
{
    public float velocidade = 10;
    public Rigidbody2D fisica;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float controle = Input.GetAxisRaw("Horizontal");

        fisica.velocity = new Vector2(controle * velocidade, fisica.velocity.y);
    }
}
