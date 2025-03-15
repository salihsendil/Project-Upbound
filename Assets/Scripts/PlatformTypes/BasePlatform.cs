using UnityEngine;
using Zenject;

public abstract class BasePlatform : MonoBehaviour
{
    public virtual float jumpForce { get; set; } = 7.5f;
    public virtual float platformMarginMin { get; set; } = 0.4f;
    public virtual float platformMarginMax { get; set; } = 1.2f;

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerJump(jumpForce);
        }
    }

    public float GenerateRandomYPosition()
    {
        return Random.Range(platformMarginMin, platformMarginMax);
    }
}
