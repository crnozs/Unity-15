using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    

    bool heroDeath;
    float deathDelay = 3f;

    [Header("Animation Smoothing")] //idle animasyonundan hareket animasyonlarýna ne kadar çabuk geçmek istediðimizi belirledik.
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f; // karakter zýpladýðýnda havada ne kadar hareket edebilirini kontrol ettik. (saða sola)

    //State'leri belirledik
    public StateMachine movementSM; //** 1
    public StandingState standing; //** 1
    public JumpingState jumping; //** 1   
    public LandingState landing;//** 1
    public SprintState sprinting;//** 1
    public CombatState combatting;//** 1
    public AttackState attacking;//** 1

    // Diðer scriptlerden ulaþabileceðimiz ancak inpector panelinde görünmeyecek deðiþkenler.
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

    [HideInInspector]
    public bool isOnlyWalk;
    public GameObject OnlyWalk;

    // Start is called before the first frame update
    private void Start() // caching'leri yaptýk.
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine(); //** 2 state machine'deki deðeri burda cach'ledik, aþaðýda ise state machine'ye baðladýk.
        standing = new StandingState(this, movementSM);//** 2
        jumping = new JumpingState(this, movementSM);//** 2
        landing = new LandingState(this, movementSM);//** 2
        sprinting = new SprintState(this, movementSM);//** 2
        combatting = new CombatState(this, movementSM);//** 2
        attacking = new AttackState(this, movementSM);//** 2

        movementSM.Initialize(standing); // bütün kontrolleri tuttuðumuz state'i init ettik.

        normalColliderHeight = controller.height; // komponentin colliderine eriþmiþ olduk.
        gravityValue *= gravityMultiplier; // yerçekimi ivmesini daha hoþ bir seviyeye taþýdýk.

        isOnlyWalk = false;
        heroDeath = false;
    }

    // Karakter havuza düþerse yada 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pool"))
        {
            StartCoroutine(heroDeathEnum());

            // Burada artýk hangi sahneyi çaðýrtacaksak onu koymamýz gerekiyor

        }
        if (other.CompareTag("onlyWalk"))
        {
            isOnlyWalk = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("onlyWalk"))
        {
            isOnlyWalk = false;
            Destroy(OnlyWalk);
        }
        
    }
    IEnumerator heroDeathEnum()
    {
        Debug.Log("ÖLDÜÜÜNNNN");
        animator.SetTrigger("death");
        heroDeath = true;

        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        // Bu iki fonksiyon her frame'de çalýþmasý gerektiði için update içinde çaðýrdýk.
        movementSM.currentState.HandleInput();
        movementSM.currentState.LogicUpdate();
        
    }

    private void FixedUpdate()
    {
        // Fizik kontrollerinin FixedUpdate'te yapýlmasý gerektiði için burada çaðýrdýk.
        movementSM.currentState.PhysicsUpdate();
    }
}