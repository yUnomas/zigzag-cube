using UnityEngine;

public class SpikeLane : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform spike;
    [SerializeField] private Transform leftEndPoint;
    [SerializeField] private Transform rightEndPoint;

    Vector3 direction = Vector3.right;

    private void Update()
    {
        spike.position += speed * direction * Time.deltaTime;
        TryReturn();
    }

    private void TryReturn()
    {
        if(leftEndPoint.position.x > spike.position.x)
        {
            spike.position = leftEndPoint.position;
            direction = Vector3.right;
        }
        else if(rightEndPoint.position.x < spike.position.x)
        {
            spike.position = rightEndPoint.position;
            direction = Vector3.left;
        }
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
    public void Set(int direction)
    {
        this.direction = direction == 1 ? Vector3.right : Vector3.left;
        gameObject.SetActive(true);
    }
}