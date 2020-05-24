using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickBehaviour : MonoBehaviour
{
    private bool isClicking = false;
    private SpriteRenderer sr;
    private Color originalColor;
    public Color clickColor = new Color(0.5f, 0.5f, 0.5f);
    public bool hold = false;
    public bool alreadyClicked = false;
    public int clickCounter = 0;

    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isClicking || hold)
        {
            sr.color = clickColor;
        }
        else if (hold == false)
        {
            ResetSprite();
        }
    }

    private void OnMouseDown()
    {
        isClicking = true;
        //GameManager.instance.ClickSound();
    }

    private void OnMouseUp()
    {
        isClicking = false;
    }

    public void ResetSprite()
    {
        sr.color = originalColor;
        hold = false;
    }

    public void ResetClicks()
    {
        clickCounter = 0;
    }

    public void ToggleHold()
    {
        hold = !hold;
    }
}