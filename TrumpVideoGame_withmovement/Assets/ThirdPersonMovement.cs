using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    private Animation anim;
	private Rigidbody rb;
	
	public float turnSmoothTime= 0.1f;
	private float turnSmoothVelocity;
	
	public Transform cam;  		 //to change the character in the direction the camera is facing


	
	// Use this for initialization
	void Start (){
		anim = GetComponent<Animation> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {

		
		
		float x1 = Input.GetAxisRaw("Horizontal");
		float y1 = Input.GetAxisRaw("Vertical");
		
		float x2 = CrossPlatformInputManager.GetAxis ("Horizontal");
		float y2 = CrossPlatformInputManager.GetAxis ("Vertical");

		float x=0;
		float y=0;
		//transform.position += new Vector3(0,0,y/100);
		//transform.position += new Vector3(x/100,0,0);

		//Vector3 movement = new Vector3(x, 0.0f, y).normalized;

		//enter trumps speed here!!!
		//rb.velocity = movement * 4f;
		
		if(x1+y1 !=0 ){
			x=x1;
			y=y1;
		}
		if(x2+y2 !=0 ){
			x=x2;
			y=y2;
		}
   
		if (x != 0 || y != 0) {
			float targetangle=Mathf.Atan2 (x, y) * Mathf.Rad2Deg + cam.eulerAngles.y; //(addition) 		 //to change the character in the direction the camera is facing

			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle,ref turnSmoothVelocity,turnSmoothTime);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,angle , transform.eulerAngles.z);
			
			Vector3 moveDirection= Quaternion.Euler(x,targetangle,y)*Vector3.forward;
			rb.velocity = moveDirection.normalized * 4f;
			anim.Play ("walk");
		} else {
			anim.Play ("idle");
		}
	}
}



