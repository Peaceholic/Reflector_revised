using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EDirection{
    Horizontal,
    Vertical
}
public class ShooterCtrl : MonoBehaviour {
	public float moveSpeed = 5.0f;
	public float attackSpeed = 7.0f;
    public float attackFrequency = 5.0f;
    public float patrolEdgeRangeX = 0.1f;
    public float patrolEdgeRangeY = 0.05f;
	public GameObject bulletPrefab;
    public GameObject DieEffect;

    private bool isDie = false;
    private Vector2 moveDirection;
    private EDirection direction;

	private Transform playerTr;
	// Use this for initialization
	void Start () {
		playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SetDirection();
        StartCoroutine(Move());
        StartCoroutine(Attack());
	}

    void SetDirection(){
        var screenPointPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPointPos.x /= Screen.width;
        screenPointPos.y /= Screen.height;
        
        float x = -1;
        float y = -1;
        if((1 - screenPointPos.x) > screenPointPos.x){
            x = 1 - screenPointPos.x;
        }
        else{
            x = screenPointPos.x;
        }

        if((1 - screenPointPos.y) > screenPointPos.y){
            y = 1 - screenPointPos.y;
        }
        else{
            y = screenPointPos.y;
        }

        if(x > y){
            float r = Random.Range(-1, 1);
            r = (r == 0) ? -1 : 1;
            moveDirection = new Vector2(0, r / Mathf.Abs(r));
            direction = EDirection.Vertical;
        }
        else{
            float r = Random.Range(-1, 1);
            r = (r == 0) ? -1 : 1;
            moveDirection = new Vector2(r / Mathf.Abs(r), 0);
            direction = EDirection.Horizontal;
        }

        moveDirection.Normalize();

    }

    void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("check");
		if(other.gameObject.tag == "Bullet") {
			Destroy(this.gameObject);
			Instantiate(DieEffect, this.transform.position, Quaternion.identity);
		}
	}

    IEnumerator Move(){
        while(!isDie){
            if(direction == EDirection.Horizontal){
                var screenPointX = Camera.main.WorldToScreenPoint(transform.position).x;
                screenPointX /= Screen.width;
                if((screenPointX < patrolEdgeRangeX && moveDirection.x == -1) ||
                 (screenPointX > (1 - patrolEdgeRangeX) && moveDirection.x == 1)){
                     moveDirection.x *= -1;
                }
                
            }
            else{
                var screenPointY = Camera.main.WorldToScreenPoint(transform.position).y;
                screenPointY /= Screen.height;
                if((screenPointY < patrolEdgeRangeY && moveDirection.y == -1) ||
                 (screenPointY > (1 - patrolEdgeRangeY) && moveDirection.y == 1)){
                     moveDirection.y *= -1;
                }
            }
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Attack(){
        while(!isDie){
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 attackDir = playerTr.position - transform.position;
            attackDir.Normalize();
            bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * attackSpeed;
            yield return new WaitForSeconds(attackFrequency);
        }
    }

}
