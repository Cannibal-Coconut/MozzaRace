using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Roller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer _warningSignal;

    [Header("Settings")]
    [SerializeField] [Range(0, 10)] float _speed;
    [SerializeField] [Range(0, 20)] float _flippedSpeed;
    [SerializeField] [Range(0, 5)] float _bounceHeight;

    [SerializeField] [Range(0, 5)] float _signalTime;

    Rigidbody2D _rigidbody;
    Removable _removabable;

    bool _flipped;
    SoundSettingManager sound;
    private void Awake()
    {
        sound = FindObjectOfType<SoundSettingManager>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _removabable = GetComponent<Removable>();

        _warningSignal.transform.parent = null;
        _removabable.AddRemoveListener(() =>
        {
            Destroy(_warningSignal.gameObject);
        });

        StartCoroutine(CountDownSignal());
    }

    private void Start()
    {
        //h*m*g = 1/2*m*(v^2) => v = (2*g*h)^0.5; _bounceHeight is h, just calculate speed for such height
        float verticalSpeed = Mathf.Pow(Mathf.Abs(2 * Physics2D.gravity.y * _bounceHeight), 0.5f);

        var desiredVelocity = _rigidbody.velocity;
        desiredVelocity.y = verticalSpeed;
        _rigidbody.velocity = desiredVelocity;

        transform.parent = null;
        sound.PlayRollerAlert();
        SetOnBounds();
    }

    void SetOnBounds()
    {
        var bounds = FindObjectOfType<ProjectileBounds>();

        _warningSignal.transform.position = bounds.rollerSignalPosition.position;
    }

    private void FixedUpdate()
    {
        if (_flipped)
        {
            _rigidbody.velocity = new Vector2(_flippedSpeed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
        }
    }

    IEnumerator CountDownSignal()
    {
        _warningSignal.enabled = true;
        yield return new WaitForSeconds(_signalTime);
        _warningSignal.enabled = false;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var launch = collision.GetComponent<PizzaLaunch>();

        if (launch)
        {
            _flipped = true;
            sound.PlayRollerHit();
            launch.RollHit();
            Collider2D col = this.gameObject.GetComponent<Collider2D>();   
        if(col!=null) col.enabled = false;
        }
    }

}
