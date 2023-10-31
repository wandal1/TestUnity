using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Touch touch;
    public float speed;
    public float posXmin;
    public float posXmax;

    void Update()
    {
        // Si le joueur a au moins 1 doigt sur l'écran
        if (Input.touchCount > 0)
        {
            // On récupère les infos du premier doigt posé sur l'écran tactile
            touch = Input.GetTouch(0);

            // On teste si le joueur bouge le doigt
            if( touch.phase == TouchPhase.Moved)
            {
                // On bouge le cube en suivant le mouvement du doigt
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speed,
                    transform.position.y,
                    transform.position.z
                    );
            }
        }

        // Bloquer le cube entre 2 valeurs sur l'axe X
        if (transform.position.x < posXmin)
        {
            transform.position = new Vector3(
                posXmin,
                transform.position.y,
                transform.position.z
                );
        }
        if (transform.position.x > posXmax)
        {
            transform.position = new Vector3(
                posXmax,
                transform.position.y,
                transform.position.z
                );
        }
    }
}
