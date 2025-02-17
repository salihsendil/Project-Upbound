using UnityEngine;

public abstract class PlatformBase : MonoBehaviour
{
    public virtual float jumpForce { get; set; } = 7f;

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            //Debug.Log("zýplama üst sýnýftan geldi");
        }
    }

}
