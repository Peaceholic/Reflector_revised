using System.Collections;
using System.Threading;
using UnityEngine;

public enum MonsterState {
    Idle = 0,
    Move, //Move to designated position. Do not care about player.
    Patrol,
    Chase, //Move to player's position
    Attack //Do special action(ex: fire bullets, etc.)
};

public enum ChargerAttackType {
    Direct,
    Explosion,
    Discharge
};

public class ChargerCtrl : MonoBehaviour {

	public float currentSpeed = 5.0f;
    public float attackSpeed = 7.0f;
    public float checkFrequency = 0.1f;
    public float patrolFrequency = 2f;
	
	public MonsterState currentState = MonsterState.Idle;
    public Sprite attackSp;
    public Sprite patrolSp;

	// attack radius of the monster
    public float attackDist = 10.0f;
    public float patrolDist = 3.0f;
    public float checkTime = 0.5f;
    public float explosionTime = 2.0f;
    public float dischargeTime = 1.0f;

	private bool isDie = false;
    private bool isArrived = false;

    private ChargerAttackType chargerAttackType;

    float currentPatrolTime;
    // Used when move state
    private Vector2 moveDestination;
    private SpriteRenderer spriteRenderer;
	private Transform playerTr;

    private float prevX;
    private float curX;

    // Charger die effect
    public GameObject dieEffect;

    // Charger explosion attack effect
    public GameObject explosionEffect;

    // Use this for initialization
    private void Start () {

        spriteRenderer = GetComponent<SpriteRenderer>();

		var player = GameObject.FindWithTag("Player");
        if(player != null) {
		    playerTr = player.GetComponent<Transform>();
        }

        // Starting to pursue player
        ChooseAttackStyle();        StartCoroutine(this.CheckState());
        StartCoroutine(this.DoAction());
        currentState = MonsterState.Move;
        currentPatrolTime = patrolFrequency;

        prevX = transform.position.x;

	}

    void DoFlip() {
		curX = transform.position.x;
		float dir = curX - prevX;
		if(dir > 0) {
			spriteRenderer.flipX = true;
		} else if(dir < 0) {
			spriteRenderer.flipX = false;
		} else {
			// do Nothing
		}
		prevX = curX;
	}

    void Update() {
        DoFlip();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Charger" || coll.gameObject.tag == "Player") {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

	// Check current state every designated time
	private IEnumerator CheckState() {
		while(!isDie && playerTr != null) {
            switch (currentState)
            {
                case MonsterState.Idle:
                    break;
                case MonsterState.Move:
                    yield return new WaitUntil(() => (isArrived == true));
                    currentState = MonsterState.Patrol;
                    break;
                case MonsterState.Patrol:
                    Vector3 playerDist = playerTr.position - transform.position;
                    const float recogDistX = 2.0f;
                    const float recogDistY = 1.0f;

                    Vector2 dist = new Vector2(Mathf.Pow(playerDist.x, 2) / Mathf.Pow(recogDistX, 2), Mathf.Pow(playerDist.y, 2) / Mathf.Pow(recogDistY, 2));

                    if(dist.sqrMagnitude < attackDist * attackDist)
                    {
                        currentState = MonsterState.Attack;
                    }
                    break;
                case MonsterState.Chase:
                    break;
                case MonsterState.Attack:
                    moveDestination = playerTr.position - transform.position;
                    yield break;
                default:
                    break;
            }
			yield return new WaitForSeconds(checkFrequency);
        }
    }

    private IEnumerator DoAction() {

		while(!isDie && playerTr != null) {
			switch(currentState) {
				case MonsterState.Idle:
                    ActIdle();
                    break;

                case MonsterState.Move:
                    ActMove();
                    break;
                case MonsterState.Patrol:
                    ActPatrol();
                    break;

				case MonsterState.Chase:
                    ActChase();
				    break;

                case MonsterState.Attack:
                    ActAttack();
                    break;

                default:

                    break;
			}
			yield return null;
		}
	}

    private void ActIdle()
    {
        
    }

    private void ActMove()
    {
        MoveTo(playerTr.position);
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        if((pos.x >= 0 && pos.x <= Screen.width) && (pos.y >= 0 && pos.y <= Screen.height)) {
            isArrived = true;
        }

    }

    private void ActChase()
    {
        MoveTo(playerTr.position);
    }

 
    private void ActPatrol()
    {
        //Set MoveDestination
        currentPatrolTime += Time.deltaTime;
        if(currentPatrolTime > patrolFrequency)
        {
            moveDestination = new Vector2(Random.Range(-patrolDist, patrolDist), Random.Range(-patrolDist, patrolDist));
            currentPatrolTime = 0;
        }

        //Actual movement
        MoveTo(transform.position + (Vector3)moveDestination);
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPoint.x > 1)
        {
            moveDestination = new Vector2(-1, moveDestination.y);
        }
        else if(screenPoint.x < 0)
        {
            moveDestination = new Vector2(1, moveDestination.y);
        }

        if(screenPoint.y > 1)
        {
            moveDestination = new Vector2(moveDestination.x, -1);
        }
        else if(screenPoint. y < 0)
        {
            moveDestination = new Vector2(moveDestination.x, 1);

        }
    }

    IEnumerator DestroyOnOutOfScreen() {
        while(true) {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if(pos.x > Screen.width || pos.y > Screen.height || pos.x < 0 || pos.y < 0) {
                yield return new WaitForSecondsRealtime(checkTime); // Wait for charger to completely disappear
                Die();
            }

            yield return new WaitForSecondsRealtime(checkTime);
        }
    }

    private void ActAttack()
    {
        switch(chargerAttackType) {
            case ChargerAttackType.Direct:
            Direct();
            break;

            case ChargerAttackType.Explosion:
            StartCoroutine(Explosion());
            break;

            case ChargerAttackType.Discharge:
            StartCoroutine(Discharge());
            break;
        }
    }

    void Direct() {
        spriteRenderer.sprite = attackSp;
        transform.Translate(moveDestination.normalized * attackSpeed * Time.deltaTime);
        StartCoroutine(DestroyOnOutOfScreen());
	}

    IEnumerator Explosion() {
        spriteRenderer.sprite = attackSp;
        yield return new WaitForSeconds(explosionTime);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Die();
	}

    IEnumerator Discharge() {
        spriteRenderer.sprite = attackSp;
        yield return new WaitForSeconds(dischargeTime);

        Vector2[] dischargeLoc = new Vector2[12];


        spriteRenderer.sprite = patrolSp;
        currentState = MonsterState.Patrol;
	}

    void ChooseAttackStyle() {
        chargerAttackType = (ChargerAttackType)Random.Range(0, System.Enum.GetValues(typeof(ChargerAttackType)).Length);
    }

    private void MoveTo(Vector2 dest)
    {
        Vector2 moveDir = (dest - (Vector2)transform.position).normalized;
        transform.Translate(moveDir * currentSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }


}
