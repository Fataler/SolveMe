using UnityEngine;

public class Level2 : MonoBehaviour
{
    public Transform nextLvlPos;
    public float maxX;
    public float maxY;
    private float timer = 0f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, nextLvlPos.transform.position) < 0.1f)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                Debug.Log("bang");
                GameManager.instance.NextLevel();
            }
            return;
        }

        if (transform.position.x >= maxX || transform.position.x <= -maxX || transform.position.y >= maxY || transform.position.y <= -maxY)
            transform.position = transform.position;
    }
}