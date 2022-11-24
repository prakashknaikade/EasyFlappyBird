using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _isBirdLaunched;
    private float _idleTime;

    [SerializeField] private float _launchSpeed = 100;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    
    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (_isBirdLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _idleTime += Time.deltaTime;
        }
        if (transform.position.y > 15 || 
            transform.position.x > 15 || 
            transform.position.y < -15 ||
            transform.position.x < -15 ||
            _idleTime > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;

    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 inTheDirectionOfRelease = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(inTheDirectionOfRelease * _launchSpeed);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _isBirdLaunched = true;
        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x,newPosition.y);
    }
}
