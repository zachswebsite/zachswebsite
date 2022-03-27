using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public float speed;
	public List<Vector3> waypoints;

        // start patrolling immediately, but we could also make a method to trigger this later
        // save the coroutine, so we can stop it if we need to
	void Start () {
		path = StartCoroutine (PatrolWaypoints ());
	}

    private void Update() {
        if(Input.GetKeyDown("space")){
                Debug.Log("Space bar pressed. Ending Coroutine.");
                StopPatrolling();
            }
    }
	
	public virtual void StopPatrolling() {
		StopCoroutine (path);
	}

	
	public IEnumerator PatrolWaypoints()
	{
		// path forever, unless StopPatrolling is called
		while (true) {
			// iterate through all points
			foreach (Vector3 point in waypoints) {
				while (transform.position != point) {
					transform.position = Vector3.MoveTowards(transform.position, point, speed * .001f);
					yield return ChangeColor();
				}
			}
                        yield return null;
		}
	}

    public IEnumerator ChangeColor(){
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red,Color.blue,Mathf.PingPong(Time.time,1));
            yield return null;
    }
	private Coroutine path;
}
