using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnnemiPatrol : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform[] positions;
    [SerializeField] float offSetDestination = 0.3f;
    Transform destination;
    public float speed = 5f;
    private int loops = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = startPoint.position;
        destination = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(destination.position.x,transform.position.y), speed * Time.deltaTime);
        FindNextDestination();
    }

    void FindNextDestination()
    {
        if ((gameObject.transform.position.x == destination.transform.position.x))
        {
            loops += 1;
            StartCoroutine(WaitBeforeGoingToNextDestination());
        }
    }

    IEnumerator WaitBeforeGoingToNextDestination()
    {
        yield return new WaitForSeconds(1f);
        destination = positions[loops % positions.Length];
        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - destination.position.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
