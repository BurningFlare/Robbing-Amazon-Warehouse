using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private string propName;

    // This should be guaranteed to be normalized
    private Vector2 moveDirection;

    // Components
    private Interactor interactor;
    private Camera mainCamera;
    private PlayerInputReceiver playerInputReceiver;
    [SerializeField] public TransmutationBase currentTransmutation;

    private bool dead = false;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();
        mainCamera = Camera.main;
        playerInputReceiver = GetComponent<PlayerInputReceiver>();
        if (currentTransmutation == null)
        {
            currentTransmutation = GetComponent<TransmutationBase>();

            // TODO make default transmutation base the player one
            //currentTransmutation = new TransmutationBase(
            //currentTransmutation.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            GetMovementInput();
        }
    }

    private void FixedUpdate()
    {
        currentTransmutation.HandleMovement(moveDirection);
    }

    void GetMovementInput()
    {
        moveDirection = playerInputReceiver.MoveInput;
        moveDirection.Normalize();
    }

    public void handleInteractionInput()
    {
        interactor.handleInteractionInput();
    }

    public bool Transmute(TransmutationBase transmutation)
    {
        if (transmutation.name == currentTransmutation.name)
        {
            return false;
        }

        // saves old health so that we can change the health over
        float oldHealthPercentage = currentTransmutation.health / currentTransmutation.maxHealth;

        Vector2 oldPosition = currentTransmutation.transform.position;

        Destroy(currentTransmutation.gameObject);

        currentTransmutation = Instantiate(transmutation, oldPosition, Quaternion.identity, transform).GetComponent<TransmutationBase>();

        // modify health of transmutation to be proportionally equivalent to the previous
        // rounds up to prevent weird behavior with setting the health to 0
        // I clamped it just to make sure it ends up in the correct bounds cuz of float precision but not sure if that's necessary
        currentTransmutation.health = Mathf.Ceil(Mathf.Lerp(0, currentTransmutation.maxHealth, oldHealthPercentage));

        GameManager.Instance.handleTransmutationChanged(currentTransmutation);

        // TODO play cool transmutation animation
        // TODO replace final transmutation in hotbar
        // TODO handle transmutation cooldown
        return true;
    }

    public void Die()
    {
        // TODO play death animation
        dead = true;
        moveDirection = Vector2.zero;
    }
}