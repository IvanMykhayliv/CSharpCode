using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float gravity;
    public Rigidbody rb;
    public bool isFalling;

    private Vector3 moveDirection = Vector3.zero;
    private CameraController cam;
    

    // Use this for initialization
    void Start (){
        rb = GetComponent<Rigidbody>();
       Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update (){
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
    }
}

    private void OnCollisionEnter(Collision collision){
        isFalling = false;
    }

    private void FixedUpdate(){

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isFalling == false){
            rb.velocity = new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            isFalling = true;
            StartCoroutine(jumpGravity());
            //rb.AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
            //rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
        }

        /*
        if (Input.GetKey(KeyCode.RightArrow)){
            //rb.AddForce(Vector3.right * speed * Time.deltaTime);
            //controller.Move(Vector3.right * speed * Time.deltaTime);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            //rb.AddForce(Vector3.left * speed * Time.deltaTime);
            //controller.Move(Vector3.left * speed * Time.deltaTime);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            //rb.AddForce(Vector3.forward * speed * Time.deltaTime);
            //controller.Move(Vector3.forward * speed * Time.deltaTime);
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            // rb.AddForce(Vector3.back * speed * Time.deltaTime);
            //controller.Move(Vector3.back * speed * Time.deltaTime);
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        /*
        if (Input.GetKey(KeyCode.Space)){
            //controller.Move(Vector3.up * speed * Time.deltaTime);
            //transform.position += Vector3.up * jump * Time.deltaTime;
            //isGrounded = false;
        }
        */

    }

    IEnumerator jumpGravity(){
        print(Time.time);
        rb.useGravity = false;
        yield return new WaitForSeconds(3);
        rb.useGravity = true;
        print(Time.time);
    }

}
