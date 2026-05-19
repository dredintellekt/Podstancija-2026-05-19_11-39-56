using UnityEngine;

public class PlayerVisual: MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash( IsDie);
    private static readonly int Running = Animator.StringToHash( IsRunning);
    private Animator animator;
    private SpriteRenderer _spriteRenderer;
    private FlashBlink _flashBlink;

    private const string IsRunning = "IsRunning";
    private const string IsDie = "IsDie";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool( Die, true);
        _flashBlink.StopBlinking();
    }

    private void Update() 
    {
         animator.SetBool(Running, Player.Instance.IsRunning());

         if (Player.Instance.IsAlive())
             AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection() 
    {
         Vector3 mousePos = GameInput.Instance.GetMousePosition();
         Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

         _spriteRenderer.flipX = mousePos.x < playerPosition.x;
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
