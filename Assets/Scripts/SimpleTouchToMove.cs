using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTouchToMove : MonoBehaviour
{
    Touch touch;
    Vector2 initPos;
    Vector2 direction;
    public CharacterController characterController;
    Vector3 moveDirection;
    public float speed = 5.0f;
    bool canMove = false;
    public float stopForce = 2f;
    public Animator animator;

    void Update()
    {
        // Gestion du d�placement
        if (Input.touchCount > 0)
        {
            canMove = true;
            // Calcul du mouvement du personnage
            touch = Input.GetTouch(0);
            // Au moment ou le joueur touche l'�cran
            if(touch.phase == TouchPhase.Began)
            {
                initPos = touch.position;
            }
            // Lorsqu'il d�place son doigt sur l'�cran tactile
            if (touch.phase == TouchPhase.Moved)
            {
                direction = touch.deltaPosition;
            }

            // Calcul de la direction de d�placement
            moveDirection = new Vector3(
                touch.position.x - initPos.x,
                0,
                touch.position.y - initPos.y
            );
            // Calcul de la rotation
            Quaternion targetRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            // On applique la rotation
            transform.rotation = targetRotation;
            moveDirection = moveDirection.normalized * speed;
            
        }
        else
        {
            canMove = false;
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * stopForce);
        }
        // Gestion des animations
        animator.SetBool("CanRun", canMove);
        // On applique le mouvement au personnage
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
