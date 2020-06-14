using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 12.0f;
    [SerializeField] private float runSpeed = 20.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float gravity = 5.0f;
    [SerializeField] private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool crouch;
    private float crouchHeight;
    private float normalHeight;


    void Start()
    {
        crouch = false;
        normalHeight = controller.height;
        crouchHeight = controller.height / 2;
    }
    
    void Update()
    {
        this.Move();
    }

    private void Move()
    {
        float speed = walkSpeed;
        if (controller.isGrounded)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            moveDirection = inputX * transform.right + inputZ * transform.forward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            if (Input.GetButton("Jump") && crouch == false)
            {

                moveDirection.y += jumpSpeed;
            }
            if (crouch == true)
                speed = crouchSpeed;
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (crouch)
                {
                    crouch = false;
                    controller.height = normalHeight;
                    controller.center = new Vector3(0f, 0f, 0f);
                }
                else
                {

                    crouch = true;
                    controller.height = crouchHeight;
                    speed = crouchSpeed;
                    controller.center = new Vector3(0f, 0.50f, 0f);

                }
            }


        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
