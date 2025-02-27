using UnityEngine;

public abstract class BasePlatform : MonoBehaviour
{
    public virtual float jumpForce { get; set; } = 7f;
    public virtual float platformMarginMin { get; set; } = 0.4f;
    public virtual float platformMarginMax { get; set; } = 1.2f;

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public float GenerateRandomYPosition()
    {
        return Random.Range(platformMarginMin, platformMarginMax);
    }
}
