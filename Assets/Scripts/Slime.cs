using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Slime : MonoBehaviour
{
    private float _speed = 3.5f;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private float _timeFlip = 2.0f;
    private float _timeFlipCounter = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeFlipCounter -= Time.deltaTime;
        if (_timeFlipCounter <= 0)
        {
            _timeFlipCounter = _timeFlip;
            _facingRight = !_facingRight;
            Flip();
        }
        Move();
    }
    private void Move()
    {
        Vector3 direction = _facingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        /*scale.x *= -1;
        transform.localScale = scale;*/
        if (_facingRight)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            Destroy(gameObject);
        }
    }
}