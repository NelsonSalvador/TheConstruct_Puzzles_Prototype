using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundcheck;

    //Distance where it's considered that the player is toutching the ground
    public float groundDistance = 0.4f;
    //Base speed of the player
    public float speed = 12f;
    //Gravity 
    public float gravity = -9.8f;
    //Height that the player will jump
    public float jumpHeight = 2f;
    
    //Layer that is considered ground
    public LayerMask groundMask;

    //Vector for gravity and jump
    Vector3 velocity;
    //Bool that is true when player is toutching ground
    bool isGrounded;
    //Speed of the player with updates
    float currentSpeed;
    // Update is called once per frame
    void Update()
    {
        //Cheks if player is on the floor, if yes turn true, is not turn false
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        //Updates de speed of falling and sets de speed to normal when player is 
        //on the floor
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            currentSpeed = speed;
        }
        else
            //set the speed to half when player is in the air
            currentSpeed = speed * 0.5f;

        //Cheks if the player is sprinting
        if (Input.GetButton("Sprint"))
            currentSpeed = speed * 2;

        //Checks if the player is trying to move
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Updates de move vector
        Vector3 move = transform.right * x + transform.forward * z;

        //Apllies the move vector
        controller.Move(move * currentSpeed * Time.deltaTime);

        //Checks if the player is trying to jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //Apllies force to jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        //Aplies gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
