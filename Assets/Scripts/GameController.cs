using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private int score;
    private bool gameOver;
    private bool restart;

    public Transform player;
    private PlayerController playerControllerScript;

    public bool gameEnabled;

    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        
        playerControllerScript = (PlayerController)player.GetComponent(typeof(PlayerController));
    }

    private void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
                //SceneManager.LoadScene(SceneManager.scene);
                //SceneManager.LoadScene(Application.loadedLevel);
            }
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        UpdateScore();
    }

    public void addWeaponToPlayer( GameObject weapon )
    {
        

        Transform emptyWeaponSlot = playerControllerScript.getEmptyWeaponSlot();
        if (emptyWeaponSlot != null)
        {
            Mover moverScriptForWeapon = (Mover)weapon.GetComponent(typeof(Mover));
            moverScriptForWeapon.speed = 0;

            Destroy(weapon.GetComponent<Rigidbody>());
            Destroy(weapon.GetComponent("Mover"));
            Destroy(weapon.GetComponent("DestroyByContact"));
            
            weapon.transform.parent = emptyWeaponSlot;
            weapon.transform.position = emptyWeaponSlot.position;
            weapon.transform.rotation = emptyWeaponSlot.rotation;

            playerControllerScript.InitialiseWeapons();
        }
        



    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (gameEnabled)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over";
    }

}
