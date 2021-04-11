using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoVoa : controleInimigo
{
    [Header("Controles do Inimigo voador")]
    public Transform target;

    public override void FixedUpdate()
    {
        if (target == null)
        {
            base.FixedUpdate();
        }
        else
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();

            inimigo.velocity = direction * velocidade;

            if (direction.x >= 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
        }
    }
}
