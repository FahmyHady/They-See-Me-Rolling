using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerControl : MonoBehaviour
{
    float horizontalAxis;
    Rigidbody rigidbody;
    Vector3 left;
    Vector3 right;
    Vector3 targetPos;
    bool pressed;
    int count;
    bool m_isAxisInUse;
    GameManager manager;
    public Vector3 speed;
    [SerializeField] float rebound; //When movement is set to dynamic (No Lanes) this force will push player back into the zone instead of falling
    [SerializeField] float turnSpeed; //When movement is set to dynamic this is the horizontal force given on moving right or left
    [SerializeField] bool dynamic; //To Disable Lanes and move right or left freely
    void Start()
    {
        targetPos = transform.position;
        left = Vector3.forward * -7;
        right = Vector3.forward * 7;
        rigidbody = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
    }
    void GetInput()
    {
        if (horizontalAxis > 0 && count == 0)
        {
          targetPos+= right;
            pressed = true;
            count += 1;
        }
        else if (horizontalAxis > 0 && count == 1)
        {

            targetPos +=  right;
            pressed = true;
            count += 1;
        }

        if (horizontalAxis < 0 && count == 2)
        {

            targetPos +=  left;
            pressed = true;
            count -= 1;
        }
        else if (horizontalAxis < 0 && count == 1)

        {

            targetPos += left;
            pressed = true;
            count -= 1;
        }
    }
    public void getAxisDown(string whichAxis)
    {
        horizontalAxis = CrossPlatformInputManager.GetAxisRaw(whichAxis);
        if (horizontalAxis != 0)
        {
            if (m_isAxisInUse == false)
            {
                GetInput();
                m_isAxisInUse = true;
            }
        }
        if (horizontalAxis == 0)
        {
            m_isAxisInUse = false;
        }
    } // Used Cross Platform input so it can be easily transfered to mobile
    void Update()
    {
        if (dynamic)
        {
            horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
            if (horizontalAxis != 0)
            {
                rigidbody.AddForce(horizontalAxis * Vector3.forward * turnSpeed * Time.deltaTime);
            }
            if (transform.position.z < 0.5f)
            {

                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rebound);
            }
            if (transform.position.z > 18.45f)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -rebound);

            }
            if (rigidbody.velocity.magnitude < 1)
            {
                manager.GameOver();
            }
        }
        else
        {
            getAxisDown("Horizontal");
            if (pressed)
            {
                Move();
            }
        }
    }
    private void Move()
    {
        targetPos.x = transform.position.x;
        targetPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, targetPos,0.7f);
        if (Mathf.Abs(transform.position.z - targetPos.z) < 0.5f)
        {
            targetPos.x = transform.position.x;
            targetPos.y = transform.position.y;
            transform.position = targetPos;
            pressed = false;
        }
    }
    public void Left()
    {
        if (count == 2)
        {

            targetPos += left;
            pressed = true;
            count -= 1;
        }
        else if (count == 1)

        {

            targetPos += left;
            pressed = true;
            count -= 1;
        }
    }
    public void Right()
    {
        if (count == 0)
        {
            targetPos += right;
            pressed = true;
            count += 1;
        }
        else if (count == 1)
        {

            targetPos += right;
            pressed = true;
            count += 1;
        }
    }
    private void FixedUpdate()
    {
        rigidbody.AddForce(-speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==11)
        {
           manager.GameOver();
        }
    }
}

