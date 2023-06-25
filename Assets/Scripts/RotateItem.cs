using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField] float rotateY = 0;
    [Range(1, 30)]
    [SerializeField] float speed = 10;

    [SerializeField] float counter = 1;
    [SerializeField] float maxCounter = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        counter = (counter + 1 * Time.deltaTime) % maxCounter;
        rotateY = Mathf.Sin(counter / speed) * 360;
        gameObject.transform.Rotate(0, rotateY, 0);
    }
}
