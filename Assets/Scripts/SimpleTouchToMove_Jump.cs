using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTouchToMove_Jump : MonoBehaviour
{
    Touch touch;
    Vector2 initPos;
    Vector2 direction;
    public CharacterController characterController;
    Vector3 moveDirection;
    public float speed = 5.0f;
    bool canMove = false;
    public float gravity = 10f;
    public float jumpForce = 3f;
    public float stopForce = 2f;
    public Animator animator;
    public GameObject jumpEffect;

    void Update()
    {
        // Gestion du déplacement
        if (Input.touchCount > 0)
        {
            canMove = true;
            // Calcul du mouvement du personnage
            touch = Input.GetTouch(0);
            // Au moment ou le joueur touche l'écran
            if(touch.phase == TouchPhase.Began)
            {
                initPos = touch.position;
            }
            // Lorsqu'il déplace son doigt sur l'écran tactile
            if (touch.phase == TouchPhase.Moved)
            {
                direction = touch.deltaPosition;
            }

            if(characterController.isGrounded)
            {
                // Calcul de la direction de déplacement
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
        }
        else
        {
            canMove = false;
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * stopForce);
        }
        // Gestion des animations
        animator.SetBool("canWalk", canMove);
        // Gestion du saut
        if (Input.GetMouseButtonUp(0) && characterController.isGrounded)
        {
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
            // Faire sauter le personnage
            moveDirection.y += jumpForce;
        }
        // Calculer la gravité
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        // On applique le mouvement au personnage
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
