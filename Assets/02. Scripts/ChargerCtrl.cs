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

public class ChargerCtrl : MonoBehaviour {

	public float currentSpeed = 5.0f;
    public float attackSpeed = 7.0f;
    public float checkFrequency = 0.2f;
    public float patrolFrequency = 2f;
	
	public MonsterState currentState = MonsterState.Idle;

	// attack radius of the monster
	public float attackDist = 10.0f;
    public float patrolDist = 3f;

	private bool isDie = false;
    private bool isArrived = false;

    float currentPatrolTime;
    // Used when move state
    private Vector2 moveDestination;

	private Transform playerTr;

    // Use this for initialization
    private void Start () {

		playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

		// Starting to pursue player
		StartCoroutine(this.CheckState());
		StartCoroutine(this.DoAction());
        currentState = MonsterState.Patrol;
        currentPatrolTime = patrolFrequency;
	}

    private  void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(gameObject);
    }

	// Check current state every designated time
	private  IEnumerator CheckState() {
		while(!isDie) {
			yield return new WaitForSeconds(checkFrequency);

            switch (currentState)
            {
                case MonsterState.Idle:
                    break;
                case MonsterState.Move:
                    yield return new WaitUntil(() => (isArrived == true));
                    currentState = MonsterState.Idle;
                    break;
                case MonsterState.Patrol:
                    float playerDist = (playerTr.position - transform.position).sqrMagnitude;
                    if(playerDist < attackDist * attackDist)
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
        }
    }

    private  IEnumerator DoAction() {

		while(!isDie){
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

    private  void ActIdle()
    {
        
    }

    private  void ActMove()
    {
        MoveTo(moveDestination);
        isArrived = ((Vector2)transform.position - moveDestination).sqrMagnitude < 0.1;
    }

    private  void ActChase()
    {
        MoveTo(playerTr.position);
    }

 
    private  void ActPatrol()
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

    private  void ActAttack()
    {
        transform.Translate(moveDestination.normalized * attackSpeed * Time.deltaTime);
    }

    private  void MoveTo(Vector2 dest)
    {
        Vector2 moveDir = (dest - (Vector2)transform.position).normalized;
        transform.Translate(moveDir * currentSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }


}
