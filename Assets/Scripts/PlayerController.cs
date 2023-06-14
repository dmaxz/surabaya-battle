using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Models;
using UnityEngine.SceneManagement;
//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{



    private CharacterController characterController;
    private DefaultInput defaultInput;
    [SerializeField] private TextMeshProUGUI healthText;
    
    public Entity playerEntity;
    public GameObject loseScreen;

    [Header("Movement & View")]
    public Vector2 inputMovement;
    public Vector3 inputView;
    private Vector2 newCameraRotation;
    private Vector3 newCharacterRotation;
    private Vector3 newMovementSpeed;
    private Vector3 newVelocitySpeed;
    public AudioSource walk;
    public AudioSource jump;
    public AudioSource jumpmoan;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -65;
    public float viewClampYMax = 65;

    [Header("Gravity")]
    public float gravityAmount;
    public float gravityMin;
    public float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;


    //private bool jumped = false;


    //[Header("Particle System")]
    //public ParticleSystem particle;
    //public Light flash;
    private void Awake()
    {
        defaultInput = new DefaultInput();
        defaultInput.Character.Movement.performed += e => inputMovement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => inputView = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();
        //defaultInput.Character.Shoot.performed += e => Shoot();
        defaultInput.Enable();
        //healthIndicator.GetComponent<TextMeshPro>().SetText(playerEntity.Health.ToString());
        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
        
    }

    

    IEnumerator backToMainMenu()
    {
        loseScreen.SetActive(true);

        //Wait for 4 seconds
        yield return new WaitForSeconds(4);
        //Camera.current.enabled = false;
        //gameObject.GetComponent<SphereCollider>().enabled = false;
        //gameObject.SetActive(false);
        //Object.Destroy(this);
        //Destroy(gameObject);

        //Object.Destroy(characterController);
        SceneManager.LoadScene(0);
        //Camera.current.enabled = true;
    }
    private void FixedUpdate()
    {
        //Debug.Log();
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        healthText.SetText(playerEntity.Health.ToString());
        if (playerEntity.Health <= 0f)
        {
            StartCoroutine(backToMainMenu());
        }
        //healthIndicator.SetText();
        CalculateView();
        CalculateJump();
        CalculateMovement();



    }


    private void CalculateView()
    {
        newCharacterRotation.y += playerSettings.viewXSensitivity * Time.deltaTime * (playerSettings.viewXInverted ? -inputView.x : inputView.x);
        transform.localRotation = Quaternion.Euler(newCharacterRotation);


        newCameraRotation.x += playerSettings.viewYSensitivity * Time.deltaTime * (playerSettings.viewYInverted ? inputView.y : -inputView.y);
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);


        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }
    private void CalculateMovement()
    {
        if (inputMovement == Vector2.zero) walk.Stop();
        else
        {
            if (!walk.isPlaying && !jump.isPlaying && !jumpmoan.isPlaying) walk.Play();
        }
        var verticalSpeed = playerSettings.walkingForwardSpeed * inputMovement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.walkingStrafeSpeed * inputMovement.x * Time.deltaTime;

        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, new Vector3(horizontalSpeed, 0, verticalSpeed), ref newVelocitySpeed, playerSettings.movementSmoothing);
        var movementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin && jumpingForce.y < 0.1f) playerGravity -= gravityAmount * Time.deltaTime;
        if (characterController == null) return;
        if (characterController.isGrounded)
        {
            playerGravity = -1f;
        }
        if (playerGravity > -1f && playerGravity <= -0.4f && !jump.isPlaying)
        {
            jump.Play();
        }

        if (jumpingForce.y > 0.1f) { 
            playerGravity = 0;
        }

        movementSpeed.y += playerGravity;

        movementSpeed += jumpingForce;

        characterController.Move(movementSpeed);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.jumpingFalloff);
    }
    private void Jump()
    {
        if (characterController == null) return;
        if (!characterController.isGrounded) return;
        //Debug.Log("Jump!");
        //walk.enabled = false;
        walk.Stop();
        jumpmoan.Play();

        jumpingForce = Vector3.up * playerSettings.jumpingHeight;

    }
    //public static void UpdateHealth(float health)
    //{
        
    //}

    //private void Shoot()
    //{
    //    flash.enabled = true;
    //    particle.Play();
    //    flash.enabled = false;
    //}
}
