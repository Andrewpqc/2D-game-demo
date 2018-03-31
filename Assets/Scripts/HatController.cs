using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour {
	public GameObject effect;
	private Vector3 rawPosition;
	private Vector3 hatPosition;
	private float maxWidth;


	// Use this for initialization
	void Start () {
		//将屏幕的宽度转化成世界坐标
		Vector3 screenPos = new Vector3(Screen.width,0,0);
		Vector3 moveWidth = Camera.main.ScreenToWorldPoint(screenPos);

		//计算帽子的宽度
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;

		//获取帽子的初始位置
		hatPosition = transform.position;

		//计算帽子的移动宽度
		maxWidth = moveWidth.x-hatWidth;
	}


	void FixedUpdate(){
		//将鼠标的屏幕位置转换成世界坐标
		rawPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//设置帽子将要移动的位置，帽子移动范围控制
		hatPosition = new Vector3(rawPosition.x,hatPosition.y,0);
		hatPosition.x=Mathf.Clamp(hatPosition.x,-maxWidth,maxWidth);

		//帽子移动
		GetComponent<Rigidbody2D>().MovePosition(hatPosition);
	}	
	// Update is called once per frame
	// void Update () {
		
	// }

	void OnTriggerEnter2D(Collider2D col){
		//删除该碰撞体的物体
		GameObject newEffect =(GameObject)Instantiate(effect,transform.position,effect.transform.rotation);
		newEffect.transform.parent=transform;
		Destroy(col.gameObject);
		Destroy(newEffect,1.0f);
	}
}
