using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public bool isjump;
    private Vector2 lastTapPos;
    
    [Header("Physics Setting")]
    public float jumpSpeed=5f;
    public float gravitySpeed=3f;
    private Rigidbody rb;
    [Header("Camera Setting")]
    public OnGroundSensor sensor;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("height",sensor.height);
        if(rb.velocity.y<0)
            anim.SetBool("isFall",true);
        else
            anim.SetBool("isFall",false);
        if (Input.GetMouseButton(0) && !isjump)
        {

            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            rb.AddForce(Vector3.left * delta);
            anim.SetFloat("left",Mathf.Clamp(delta,-1f,1f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
            anim.SetFloat("left",0f);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Player" && !isjump){
                Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
                Debug.Log(hit.transform.name);
                anim.SetTrigger("jump");
            }
        }
        rb.AddForce(Physics.gravity.y * Vector3.up * gravitySpeed, ForceMode.Acceleration); 
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "Ground")
        {
            isjump=false;
            rb.velocity = Vector3.zero; // Remove velocity to not make the ball jump higher after falling done a greater distance
        }
    }
    public void Jump()
    {
        isjump = true;
        //Vector3 newV = Vector3.up * Mathf.Sqrt(height);
        //rb.velocity = newV * Mathf.Abs(speed);
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    public void LeftAttackButton(){
        anim.SetBool("isLeft",true);
        anim.SetTrigger("attack");
    }
    public void RightAttackButton(){
        anim.SetBool("isLeft",false);
        anim.SetTrigger("attack");
    }
    public void OnIdleUpdate() {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), 0f, .2f));
    }
    public void OnAttackUpdate() {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), 1f, 1f));
    }
}
