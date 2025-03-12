using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;//van toc di chuyen
    [SerializeField]
    private float _jumpVelocity = 5.0f;//van toc nhay
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;//huong htai nhan vat
    //phat nhac
    private AudioSource _audioSource;//nguon phat nhac

    [SerializeField]
    private AudioClip _gemSound;//file nhac
    private int _gemCounter = 0;
    [SerializeField]
    private TextMeshProUGUI _gemCounterText;
    //bien ghi nhan diem so
    private static int _score = 1000;
    //tham chieu vien dan
    [SerializeField]
    private GameObject _weaponPrefab;
    //tham chieu cay sung
    [SerializeField]
    private GameObject _gun;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        //cap nhat so luong gem
        _gemCounterText.text = "x " + _gemCounter.ToString();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Fire();
    }
    //ban dan
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //tao ra 1 vien dan trong game
            var weapon = Instantiate(_weaponPrefab, _gun.transform.position, Quaternion.identity);
            Vector3 direction = _facingRight ? Vector3.right : Vector3.left;
            weapon.GetComponent<Rigidbody2D>().velocity = direction * 5.0f;
            //bien mat sau 3s
            Destroy(weapon, 3.0f);
        }
    }
    private void Move()
    {
        //bat sk nhan phim ngang
        float horizontalInput = Input.GetAxis("Horizontal");
        //horizontalInput = -1: nhan phim A/ left arrow
        //horizontalInput = 1: nhan phim D/ right arrow
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        //kiem tra huong di chuyen
        if (horizontalInput < 0)
        {
            _facingRight = false;
        }
        else if (horizontalInput > 0)
        {
            _facingRight = true;
        }
        _animator.SetBool("isRuning", true);
        Flip();


    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (scale.x > 0 && !_facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            return;
        }
        if (scale.x < 0 && _facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            return;
        }
        return;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nhay
            _rigidbody2D.velocity = Vector2.up * _jumpVelocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gem"))
        {
            //phat nhac
            _audioSource.PlayOneShot(_gemSound);
            //tang diem
            _gemCounter++;
            _gemCounterText.text = "x " + _gemCounter.ToString();
            //bien mat gem
            Destroy(other.gameObject);
        }
    }
    public int GetScore()
    {
        return _score;
    }
}