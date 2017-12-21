using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnum : MonoBehaviour
{
    enum State { idle,move,rotate}
    private State myState;
    [SerializeField]
    private float timeRotate = 5;
    [SerializeField]
    private float timeRotate2 = 6;

    void Start ()
    {
        myState = State.idle;
        StartCoroutine(Step());
	}
	
	void Update ()
    {
        if (myState == State.move)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (myState == State.rotate)
        {
            transform.Rotate(Vector3.right * Time.deltaTime*100);
        }

    }

    private IEnumerator Step()
    {
        yield return new WaitForSeconds(timeRotate);
        myState = State.move;
        yield return new WaitForSeconds(timeRotate2);
        myState = State.rotate;

    }
}
