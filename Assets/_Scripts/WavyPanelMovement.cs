using UnityEngine;

public class WavyPanelMovement : MonoBehaviour
{
    public float widthMultiplier = 2f;
    public float heightMultiplier = 1f;
    public float speed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float t = Time.time * speed;

        float x = Mathf.Sin(t) * widthMultiplier;
        float y = Mathf.Sin(t * 2) * heightMultiplier;

        Vector3 offset = new Vector3(x, y, 0);
        transform.position = startPosition + offset;
    }
}