using System.Collections;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PizzaLaunch _pizzaLaunch; // Reference to the launch of the pizza script

    [SerializeField]
    private PlayerManager _playerManager; // Reference to PlayerManager in order to get the position of the input

    [Range(1, 50)] [SerializeField] private float power; // Power of the launch
    [Range(1, 50)] [SerializeField] private float recallSpeed; // Recall speed of the pizza
    [Range(0, 3)] [SerializeField] private float recallWaitTime; // Recall speed of the pizza

    [Range(0, 1)] [SerializeField]
    private float minimalDistance; // Minimal distance between the start and the end points of the charge

    [Range(0, 3)] public float grabPizzaRadius; // Radius of the grab



    [SerializeField] private SpriteRenderer _pizzaSprite; 
    [SerializeField] private Collider2D _pizzaCollider; 


    public float Power => power;
    public float RecallSpeed => recallSpeed;

    private PlayerMovementInterface _playerInfo;

    private LaunchTrajectory
        _launchTrajectory; // Reference to the script LaunchTrajectory to draw the direction of the launch

    private Vector3
        _startPoint,
        _endPoint,
        _currentPoint; // Points that saves the position of the finger during the charge of the attack

    public bool isAttackStarted { get; private set; } // Check if the attack has started

    private void Start()
    {
        _launchTrajectory = GetComponent<LaunchTrajectory>();
        _playerInfo = GetComponent<PlayerMovementInterface>();

        _pizzaLaunch.onLaunchPizza += HandleThrownPizzaStateEvent;
        _pizzaLaunch.onReceivePizza += HandleReceivePizzaStateEvent;
    }

    private void Update()
    {
        GetCurrentPoint();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabPizzaRadius);
    }

    public void OnStartAttackInput(Vector2 startPoint)
    {
        if (!_pizzaLaunch.IsWithPlayer()) return;
        _startPoint = startPoint;
        isAttackStarted = true;
    }

    public void OnEndAttackInput(Vector2 endPoint)
    {
        if (!isAttackStarted) return;
        _endPoint = endPoint;
        isAttackStarted = false;
        _launchTrajectory.EraseLine();
        _playerInfo.LaunchPizzaTrigger();
        if (IsMinimalDistance() && _pizzaLaunch.IsWithPlayer())
        {
            _pizzaLaunch.ThrowPizza(Vector3.Normalize(_endPoint - _startPoint));
            StartCoroutine(RecallTime(recallWaitTime));
        }
    }

    private void GetCurrentPoint()
    {
        if (!isAttackStarted) return;

        _currentPoint = _playerManager.AttackPosition();

        _launchTrajectory.DrawTrajectory(_startPoint, _currentPoint);
    }


    private IEnumerator RecallTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _pizzaLaunch.RecallPizza();
    }


    public void HandleThrownPizzaStateEvent()
    {
        _pizzaSprite.enabled = true;
        _pizzaCollider.enabled = true;
        _playerInfo.SetPizzaStatus(false);
    }

    public void HandleReceivePizzaStateEvent()
    {
        _pizzaSprite.enabled = false;
        _pizzaCollider.enabled = false;
        _playerInfo.SetPizzaStatus(true);
    }

    private bool IsMinimalDistance()
    {
        return (_endPoint - _startPoint).magnitude > minimalDistance;
    }
}