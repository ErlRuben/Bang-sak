using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 12.0f;
    public float gravity = -9.0f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public float jumpheight = 3f;
    public LayerMask groundMask;
    Vector3 velocity;

    bool isGrounded;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        // if(isGrounded && velocity.y <0){
        //     velocity.y = -2f;
        // }
        if (characterController.isGrounded) {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            velocity = transform.TransformDirection(velocity);
            if (Input.GetKey (KeyCode.LeftShift)){
                velocity *= speed * 2;
            }    
            else{
                velocity *= speed;
            } 
        }
        // if(true){
        //     Vector3 move = transform.right * x + transform.forward * z;
        //     characterController.Move(move * speed * Time.deltaTime);
        // }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}