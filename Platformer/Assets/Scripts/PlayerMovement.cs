using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    CharacterController cc;

    // this is the move direction
    Vector3 combinedInput;
    Vector3 movement;
    public float gravity;
    public float verticalSpeed;
    public float jumpSpeed;
    public float jumpHeight;
    public float jumpTime;
    public Camera cam;
    float timeOfLastGrounded;
    float jumpGracePeriod = 0.7f;
    bool useCameraReferenceInputs = true;
    float timeSinceJumpPress;
    bool isAllowedToJump = true;
    int jumpCounter = 0;

    public bool IsJumpHeld
    {
        get
        {
            return Input.GetKey(KeyCode.Space);
        }
    }


    // at start we need the charactercontroller
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }


    void Update()
    {
        // this sets the time when the object was last on the ground, we do this to overwrite the isGrounded from the char controller
        // overall the isGrounded doesn't work too great
        // our verticalspeed is alway 0 when we are on the ground!
        if (cc.isGrounded)
        {
            timeOfLastGrounded = Time.timeSinceLevelLoad;
            verticalSpeed = 0f;

        }

        // so this is just a replacement of the isGrounded, we give our player a grace period
        if (Time.timeSinceLevelLoad - timeOfLastGrounded <= jumpGracePeriod)
        {
            isAllowedToJump = true;

            if (isAllowedToJump && Input.GetKeyDown(KeyCode.Space))
            {
                //gravityMultiplier = 0.50f;
                CalculateJumpSpeed();
                verticalSpeed = jumpSpeed;
            }

        }
        else
        {
            isAllowedToJump = false;
        }



        if (IsJumpHeld)
        {
            timeSinceJumpPress += Time.deltaTime;
            if (timeSinceJumpPress <= 2)
            {
                verticalSpeed -= gravity * Time.deltaTime * 0.5f;

            }
            else
            {
                verticalSpeed -= gravity * Time.deltaTime;
                timeSinceJumpPress = 0f;
            }


        }
        else
        {
            // if we are not grounded, this is what we do to calculate our vertical speed, which is - gravity over time
            verticalSpeed -= gravity * Time.deltaTime;
        }



        // we set out vertical en horizontal floats from input
        float inputV = Input.GetAxis("Vertical");
        float inputH = Input.GetAxis("Horizontal");
        // we combine them in a vector
        combinedInput = new Vector3(inputH, 0f, inputV);

        // we use this to make our camera decide whats 'front'
        if (useCameraReferenceInputs)
        {
            Vector3 camForward = cam.transform.forward; // get the forward direction of the cam (this can be tilted up or down still, as it's local)
            camForward.y = 0;                           // remove the y value (no more up/down)
            camForward.Normalize();                     // make it length 1 again (removing y will change the length)

            Vector3 camRight = cam.transform.right;     // Unless we do a dutch angle, the "right" direction won't need any changes

            // Now we have 2 reference directions, we can combine them with our inputs to get inputCombined, but in camera reference
            combinedInput = camForward * inputV + camRight * inputH;
        }
        // now we normalize our combinedinput to get rid of the nasty diagonal speedproblem
        combinedInput = combinedInput.normalized;

        // now we make this into actual movement and not input, while also adding our vertical speed which is 0 or our jumpspeed
        movement = ((combinedInput * moveSpeed) + (Vector3.up * verticalSpeed)) * Time.deltaTime;
        cc.Move(movement);
        RotateTowardsMovementDir(combinedInput);

    }

    void RotateTowardsMovementDir(Vector3 direction)
    {
        this.transform.LookAt(this.transform.position + direction);
    }

    void CalculateJumpSpeed()
    {
        gravity = 2f * jumpHeight / (jumpTime * jumpTime);
        jumpSpeed = Mathf.Sqrt(2 * jumpHeight * gravity);

    }
}
