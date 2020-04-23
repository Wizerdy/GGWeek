using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float normalSpeed = 12f;

    private float speed;
    private Rigidbody _rb;
    private CapsuleCollider colli;

    public bool isGrounded = true;

    public LayerMask groundMask;
    [SerializeField]
    bool mouved = false;
    private float helptime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        colli = GetComponent<CapsuleCollider>();
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && !MouseLook.instance.visionLock)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = (transform.right * x + transform.forward * z);
            bool inMouvement = move.x + move.z + move.y != 0;

            if (!inMouvement && mouved)
            {
                helptime -= Time.deltaTime;
            }
            else
            {
                helptime = 0.1f;
            }
            if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && !mouved)
            {
                mouved = true;
            }
            move = Vector3.ClampMagnitude(move, 1);
            _rb.MovePosition(transform.position + (move * speed * Time.deltaTime));

            if (Input.GetKeyDown(KeyCode.A))
            {
                Inventory.instance.Drop();
            }
        }
        else
        {
            mouved = false;
        }
    }

    
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(colli.bounds.center, new Vector3(colli.bounds.center.x, colli.bounds.min.y, colli.bounds.center.z), colli.radius * 0.9f, groundMask);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platforme")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "platforme")
        {
            isGrounded = false;
        }
    }
}
