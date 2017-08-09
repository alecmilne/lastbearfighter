using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if( other.CompareTag("Player") || other.CompareTag("Weapon") )
        {
            if (this.tag == "Weapon")
            {
                Debug.Log("Weapon collision");
                gameController.addWeaponToPlayer(this.gameObject);

                return;
            }
            else
            {
                if (other.CompareTag("Player"))
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                    gameController.GameOver();
                }
            }
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
