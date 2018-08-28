using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EDirection {
    Horizontal,
    Vertical
}

public enum ShooterAttackType {
    Direct,
    RandomAttack,
    ConsecutiveBullets,
    SixWays
}

public class ShooterCtrl : MonoBehaviour {

	public float moveSpeed = 7.0f;

    // Bullet attack speed
	public float directAttackSpeed = 7.0f;
    public float randomAttackSpeed = 7.0f;
    public float consecutiveAttackSpeed = 6.0f;
    public float sixwaysAttackSpeed = 6.0f;

    // Bullet attack frequency
    public float directAttackFrequency = 0.3f;
    public float randomAttackFrequency = 0.3f;
    public float consecutiveAttackFrequency = 1.0f;
    public float sixwaysAttackFrequency = 3.5f;


    public int numOfConsecutiveBullets = 5;

    public float patrolEdgeRangeX = 0.1f;
    public float patrolEdgeRangeY = 0.05f;
	public GameObject bulletPrefab;
    public float checkTime = 0.5f;
    public ShooterAttackType shooterAttackType;

    private bool isDie = false;
    private Vector2 moveDirection;
    private EDirection direction;

	private Transform playerTr;

    public GameObject dieEffect;

	// Use this for initialization
	void Start () {
        var player = GameObject.FindWithTag("Player");
        if(player != null) {
		    playerTr = player.GetComponent<Transform>();
        }
        ChooseAttackStyle();
        shooterAttackType = ShooterAttackType.RandomAttack;
        SetDirection();
        StartCoroutine(Move());
        Attack();
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
            float r = UnityEngine.Random.Range(-1, 1);
            r = (r == 0) ? -1 : 1;
            moveDirection = new Vector2(0, r / Mathf.Abs(r));
            direction = EDirection.Vertical;
        }
        else{
            float r = UnityEngine.Random.Range(-1, 1);
            r = (r == 0) ? -1 : 1;
            moveDirection = new Vector2(r / Mathf.Abs(r), 0);
            direction = EDirection.Horizontal;
        }

        moveDirection.Normalize();

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
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

    void ChooseAttackStyle() {
        shooterAttackType = (ShooterAttackType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ShooterAttackType)).Length);
    }

    void Attack() {
        switch(shooterAttackType) {
            case ShooterAttackType.Direct:
            StartCoroutine(Direct());
            break;

            case ShooterAttackType.RandomAttack:
            StartCoroutine(RandomAttack());
            break;

            case ShooterAttackType.ConsecutiveBullets:
            StartCoroutine(ConsecutiveBullets());
            break;

            case ShooterAttackType.SixWays:
            StartCoroutine(SixWays());
            break;

            default:
            break;
        }
    }

    IEnumerator Direct() {
		while(!isDie && (playerTr != null)) {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 attackDir = playerTr.position - transform.position;
            attackDir.Normalize();
            bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * directAttackSpeed;

            Destroy(bulletObject, 8); // May erase after optimization
            yield return new WaitForSeconds(directAttackFrequency);
        }
	}

	IEnumerator RandomAttack() {
		while(!isDie && (playerTr != null)) {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 attackDir = playerTr.position - transform.position;
            int angle = Random.Range(0, 180);
            if(direction == EDirection.Vertical) {
                attackDir.y += Mathf.Cos(Mathf.Deg2Rad * angle) * 5;
            } else {
                attackDir.x += Mathf.Cos(Mathf.Deg2Rad * angle) * 5;
            }
            attackDir.Normalize();
            bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * directAttackSpeed;

            Destroy(bulletObject, 8); // May erase after optimization
            yield return new WaitForSeconds(directAttackFrequency);
        }
	}

    IEnumerator ConsecutiveBullets() {
		while(!isDie && (playerTr != null)) {
            for(int i=0 ; i<numOfConsecutiveBullets ; i++) {
                GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector2 attackDir = new Vector2(0, 0);
                if(direction == EDirection.Vertical) {
                    if(transform.position.x < 0) {
                        attackDir.x += 1;
                        bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * consecutiveAttackSpeed;    
                    } else {
                        attackDir.x -= 1;
                        bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * consecutiveAttackSpeed;

                    }
                } else if (direction == EDirection.Horizontal) {
                    attackDir.y -= 1;
                    bulletObject.GetComponent<Rigidbody2D>().velocity = attackDir * consecutiveAttackSpeed;
                }

                yield return new WaitForSeconds(0.06f);

            }

			yield return new WaitForSeconds(consecutiveAttackFrequency);
		}
	}

    IEnumerator SixWays() {
		while(!isDie && (playerTr != null)) {
            GameObject[] bulletObject = new GameObject[6];
            Vector2 dir = new Vector2(0, 0);
            if(direction == EDirection.Vertical) {
                dir.y -= 3.0f;
            } else if (direction == EDirection.Horizontal) {
                dir.x -= 3.5f;
            }
            for(int i=0 ; i<6 ; i++) {
                bulletObject[i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector2 attackDir = playerTr.position - transform.position;
                attackDir += dir;
                attackDir.Normalize();
                bulletObject[i].GetComponent<Rigidbody2D>().velocity = attackDir * sixwaysAttackSpeed;
                
                if(direction == EDirection.Vertical) {
                    dir.y += 1.2f;
                } else if (direction == EDirection.Horizontal) {
                    dir.x += 1.4f;
                }
            }
            
			yield return new WaitForSeconds(sixwaysAttackFrequency);
		}
	}
}
