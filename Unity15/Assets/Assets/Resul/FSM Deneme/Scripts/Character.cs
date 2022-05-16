using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    //public float crouchColliderHeight = 1.35f; // karakterdeki Character Controller componentinde ba�lang�� boyu 1.8

    [Header("Animation Smoothing")] //idle animasyonundan hareket animasyonlar�na ne kadar �abuk ge�mek istedi�imizi belirledik.
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f; // karakter z�plad���nda havada ne kadar hareket edebilirini kontrol ettik. (sa�a sola)

    //State'leri belirledik
    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    //public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    //public SprintJumpState sprintjumping;
    public CombatState combatting;
    public AttackState attacking;

    // Di�er scriptlerden ula�abilece�imiz ancak inpector panelinde g�r�nmeyecek de�i�kenler.
    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;


    // Start is called before the first frame update
    private void Start() // caching'leri yapt�k.
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine(); // state machine'deki de�eri burda cach'ledik, a�a��da ise state machine'ye ba�lad�k.
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        combatting = new CombatState(this, movementSM);
        attacking = new AttackState(this, movementSM);

        movementSM.Initialize(standing); // b�t�n kontrolleri tuttu�umuz state'i init ettik.

        normalColliderHeight = controller.height; // komponentin colliderine eri�mi� olduk.
        gravityValue *= gravityMultiplier; // yer�ekimi ivmesini daha ho� bir seviyeye ta��d�k.
    }

    private void Update()
    {
        // Bu iki fonksiyon her frame'de �al��mas� gerekti�i i�in update i�inde �a��rd�k.
        movementSM.currentState.HandleInput();
        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        // Fizik kontrollerinin FixedUpdate'te yap�lmas� gerekti�i i�in burada �a��rd�k.
        movementSM.currentState.PhysicsUpdate();
    }
}