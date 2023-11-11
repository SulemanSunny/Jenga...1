using UnityEngine;
using UnityEngine.SceneManagement;

public class JengaBlockController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;
    private GameOver gameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameOver = GetComponent<GameOver>();
    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) + offset;
            rb.MovePosition(newPosition);
        }
    }


    void OnMouseDown()
    {
        if (!isDragging)
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            rb.freezeRotation = true; 
        }
    }

   

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            rb.freezeRotation = false; 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "finish")
        {
            SceneManager.LoadScene(3);
        }
        else if(collision.gameObject.tag == "ground")
        {
            SceneManager.LoadScene(2);
        }

        
    }

}
