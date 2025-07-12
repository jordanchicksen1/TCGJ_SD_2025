using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class CharacterControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    public int moveSpeed;
    [SerializeField]
    private float JumpHeight;
    private Controls controls;
    private Vector2 lookInput;
    public Transform playerCamera;
    public Vector2 moveInput;
    private CharacterController characterController;
    private Vector3 velocity;
    public float gravity = -9.8f;
    [SerializeField]
    private float lookSpeed;
    private float verticalLookRotation = 0f;
    [SerializeField]
    private int Range;
    public GameObject projectilePrefab;
    public Transform FirePoint;
    //Interaction 
    public LayerMask InteractLayer;


    //Attack Spells
    [SerializeField]
    private bool HasFireBall, HasLightning, HasMoonStick, HasBomb, HasCocktailMolotove, HasBoxingloves;
    [SerializeField]
    private List<bool> AttackBools = new List<bool>();
    public GameObject FireBallPrefab, LightningPrefab, BombPrefab, MoonStickPrefab, BoxingGlovesPrefab, CocktailMolotovePrefab;


    //Support Spells
    [SerializeField]
    private bool HasSlimeBall, HasBubbleBall, HasIceBall, HasTeleportSpell, HasFlashStar, HasHotFeet;
    [SerializeField]
    private List<bool> SupportBools = new List<bool>();
    public GameObject SlimePrefab, IcePrefab, BubblePrefab, TeleportPrefab, FlashStarPrefab, BootsPrefab;

    [SerializeField]
    private int PlayerIndex;

    //Effects
    public float shakeMagnitude = 0.3f;
    private Vector3 originalPos;
    public GameObject Cloud;
    public GameObject flamePanel;
    public GameObject CollectionParticles;

    public Transform RayPoint;
    private void OnEnable()
    {
        if (PlayerIndex == 1)
        {
            controls = new Controls();
            controls.Player.Enable();
            controls.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Player.LookAround.canceled += ctx => lookInput = Vector2.zero;

            controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
            controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

            controls.Player.Jump.performed += ctx => Jump();
            controls.Player.Attack.performed += ctx => Attack();
            controls.Player.Support.performed += ctx => Support();
            controls.Player.Dash.performed += ctx => StartCoroutine(Dash());

        }
        else if (PlayerIndex == 2)
        {
            controls = new Controls();
            controls.Player2.Enable();
            controls.Player2.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Player2.LookAround.canceled += ctx => lookInput = Vector2.zero;

            controls.Player2.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
            controls.Player2.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

            controls.Player2.Jump.performed += ctx => Jump();
            controls.Player2.Attack.performed += ctx => Attack();
            controls.Player2.Support.performed += ctx => Support();
            controls.Player2.Dash.performed += ctx => StartCoroutine(Dash());
        }


    }

    private void Start()
    {
        AttackBools.Add(HasBomb);
        AttackBools.Add(HasCocktailMolotove);
        AttackBools.Add(HasFireBall);
        AttackBools.Add(HasBoxingloves);
        AttackBools.Add(HasLightning);
        AttackBools.Add(HasMoonStick);

        //Support Bools
        SupportBools.Add(HasHotFeet);
        SupportBools.Add(HasIceBall);
        SupportBools.Add(HasSlimeBall);
        SupportBools.Add(HasBubbleBall);
        SupportBools.Add(HasFlashStar);
        SupportBools.Add(HasTeleportSpell);

        //shake screen
        originalPos = playerCamera.localPosition;
    }

    private void Update()
    {
        ApplyGravity();
        Move();
        LookAround();

        HasBomb = AttackBools[0];
        HasCocktailMolotove = AttackBools[1];
        HasFireBall = AttackBools[2];
        HasBoxingloves = AttackBools[3];
        HasLightning = AttackBools[4];
        HasMoonStick = AttackBools[5];


        //Support Bools
        HasHotFeet = SupportBools[0];
        HasIceBall = SupportBools[1];
        HasSlimeBall = SupportBools[2];
        HasBubbleBall = SupportBools[3];
        HasFlashStar = SupportBools[4];
        HasTeleportSpell = SupportBools[5];

        //screen Shake For Boxing Gloves
        if (HasBoxingloves)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            playerCamera.localPosition = originalPos + new Vector3(x, y, 0f);
        }
        if (HasLightning)
        {
            Cloud.SetActive(true);
        }
        else if (!HasLightning)
        {
            Cloud.SetActive(false);
        }
        if(HasFireBall)
        {
            flamePanel.SetActive(true);
        }
        else if (!HasFireBall)
        {
            flamePanel.SetActive(false);
        }

        if (HasMoonStick)
        {
            EffectManager effectScript = GetComponent<EffectManager>();
            effectScript.canFloat = true;
        }
        else if (!HasMoonStick)
        {
            EffectManager effectScript = GetComponent<EffectManager>();
            effectScript.canFloat = false;
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Attack()
    {
        Ray ray = new Ray(RayPoint.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range))
        {
            if (HasFireBall)
            {
                GameObject Projectile = Instantiate(FireBallPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasLightning)
            {
                GameObject Projectile = Instantiate(LightningPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasMoonStick)
            {
                GameObject Projectile = Instantiate(MoonStickPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasCocktailMolotove)
            {
                GameObject Projectile = Instantiate(CocktailMolotovePrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasBomb)
            {
                GameObject Projectile = Instantiate(BombPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasBoxingloves)
            {
                GameObject Projectile = Instantiate(BoxingGlovesPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
        }
    }

    void Support()
    {
        Ray ray = new Ray(RayPoint.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range))
        {
            if (HasSlimeBall)
            {
                GameObject Projectile = Instantiate(SlimePrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasIceBall)
            {
                GameObject Projectile = Instantiate(IcePrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasTeleportSpell)
            {
                GameObject Projectile = Instantiate(TeleportPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
                ProjectileDestination.Player = gameObject;

            }
            else if (HasHotFeet)
            {
                GameObject Projectile = Instantiate(BootsPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasBubbleBall)
            {
                GameObject Projectile = Instantiate(BubblePrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            else if (HasFlashStar)
            {
                GameObject Projectile = Instantiate(FlashStarPrefab, FirePoint.position, Quaternion.identity);
                ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
                ProjectileDestination.hitPoint = hit.point;
            }
            
        }
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 2))
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        move = transform.TransformDirection(move);

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        moveSpeed += 10;
        yield return new WaitForSeconds(0.5f);
        moveSpeed -= 10;
    }

    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity

        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character

    }

    public void LookAround()
    {
        /// Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -50, 40);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBAll"))
        {
            if (HasLightning)
            {
                moveSpeed += 4;
            }
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[2] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            
        }
        else if (other.CompareTag("Lightning"))
        {
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[4] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            moveSpeed -= 4;
            Destroy(ParticleSystem, 2);

        }
        else if (other.CompareTag("MoonStick"))
        {
            if (HasLightning)
            {
                moveSpeed += 4;
            }
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[5] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);

        }
        else if (other.CompareTag("BoxingGloves"))
        {
            if (HasLightning)
            {
                moveSpeed += 4;
            }
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[3] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        else if (other.CompareTag("Bomb"))
        {
            if (HasLightning)
            {
                moveSpeed += 4;
            }
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[0] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        else if (other.CompareTag("Molotove"))
        {
            if (HasLightning)
            {
                moveSpeed += 4;
            }
            for (int i = 0; i < AttackBools.Count; i++)
            {
                AttackBools[i] = false;
            }
            AttackBools[1] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }


        //Support Spells
        else if (other.CompareTag("Ice"))
        {
            for (int i = 0; i < SupportBools.Count; i++)
            {
                SupportBools[i] = false;
            }
            SupportBools[1] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        else if (other.CompareTag("Slime"))
        {
            for (int i = 0; i < SupportBools.Count; i++)
            {
                SupportBools[i] = false;
            }
            SupportBools[2] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        
        else if (other.CompareTag("FlashStar"))
        {
            for (int i = 0; i < SupportBools.Count; i++)
            {
                SupportBools[i] = false;
            }
            SupportBools[4] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        else if (other.CompareTag("teleport"))
        {
            for (int i = 0; i < SupportBools.Count; i++)
            {
                SupportBools[i] = false;
            }
            SupportBools[5] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }
        else if (other.CompareTag("Bubble"))
        {
            for (int i = 0; i < SupportBools.Count; i++)
            {
                SupportBools[i] = false;
            }
            SupportBools[3] = true;
            GameObject ParticleSystem = Instantiate(CollectionParticles, transform.position, Quaternion.identity);
            ParticleSystem.transform.rotation = transform.rotation;
            Destroy(ParticleSystem, 2);
        }

    }
}
