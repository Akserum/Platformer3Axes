using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;
    bool isInvincible = false;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnnemiCollider"))
        {

            if (transform.position.y > other.gameObject.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().bounds.size.y)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
                other.gameObject.SetActive(false);
                //on saute au dessus de l'ennemi
            }
            else if (transform.position.y <= other.gameObject.transform.position.y + other.gameObject.GetComponent<BoxCollider2D>().bounds.size.y && isInvincible == false)
            {
                StartCoroutine(DamageTaken());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Coin")
        {
            GameManager.Instance.GetCoin();
            other.gameObject.SetActive(false);
        }
        if (other.tag == "DeathBox")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            transform.position = GameManager.Instance.respawnPoint.position;
            GameManager.Instance.SetPlayerHealth(1);
        }
    }

    void BlinkCharacter(float alpha)
    {
        playerSprite.color = new Color(1f, 1f, 1f, alpha);
    }

    IEnumerator DamageTaken()
    {
        BlinkCharacter(.2f);
        GameManager.Instance.SetPlayerHealth(1);
        isInvincible = true;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(.2f);
            BlinkCharacter(.2f);
            yield return new WaitForSeconds(.2f);
            BlinkCharacter(1f);
        }
        isInvincible = false;
    }
}
