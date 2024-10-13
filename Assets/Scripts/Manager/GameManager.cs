using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject meteorPrefab;
    public float spawnTime = 1.5f;
    public float enemyTime = 0.0f;
    public float meteorTime = 0.0f;
    public float totalTime = 0.0f;
    public Player player; // Referencia al jugador
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI shieldsText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI powerUpText;


    public GameObject vida1;
    public GameObject vida2;
    public GameObject vida3;

    public Image powerUpSlader; 

    public int score = 0;

    // Update is called once per frame
     private void Start()
    {
        UpdateLivesDisplay(); // Inicializa la visualización de vidas al comenzar el juego
    }


    void Update()
    {
        CreateEnemy();
        CreateMeteor();
        UpdateCanvas();
        totalTime += Time.deltaTime;
        
    }

    void UpdateCanvas()
    {
        liveText.text = "      : " + player.lives;
        shieldsText.text = "Escudo: " + player.shieldsAmount;
        weaponText.text = "Arma: " + player.BulletPref.name;
        scoreText.text = "Puntos: " + score.ToString();
        timeText.text = "Tiempo: " + totalTime.ToString("F0");
        powerUpSlader.fillAmount = player.powerUPCount;
    }

    public void ActivePowerUpText(){
        powerUpText.gameObject.SetActive(true);
    }

    public void DesactivePowerUpText(){
        powerUpText.gameObject.SetActive(false);
    }

    public void UpdateLivesDisplay() 
    {
        // Establece todas las imágenes de vida como inactivas al principio
        vida1.SetActive(false);
        vida2.SetActive(false);
        vida3.SetActive(false);
        
        // Muestra las vidas restantes basándote en el número de vidas
        switch (player.lives) // Cambia "lives" a "player.lives" para acceder al valor del jugador
        {
            case 3:
                vida1.SetActive(true);
                vida2.SetActive(false);
                vida3.SetActive(false);
                Debug.Log("Vidas restantes: 3 (todas las vidas activas)");
                break;
            case 2:
                vida1.SetActive(false);
                vida2.SetActive(true);
                vida3.SetActive(false);
                Debug.Log("Vidas restantes: 2 (todas las vidas activas)");
                break;
            case 1:
                vida1.SetActive(false);
                vida2.SetActive(false);
                vida3.SetActive(true);
                Debug.Log("Vidas restantes: 1 (todas las vidas activas)");
                break;
            case 0:
                vida1.SetActive(false);
                vida2.SetActive(false);
                vida3.SetActive(true);
                Debug.Log("Vidas restantes: 0 (todas las vidas activas)");
                break;
            default:
                break;
                
        }
    
    }

    private void CreateEnemy()
    {
        enemyTime += Time.deltaTime;
        if (enemyTime > 3.0f)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0), Quaternion.identity);
            enemyTime = 0.0f; 
        }
    }

    private void CreateMeteor()
    {
        meteorTime += Time.deltaTime;
        if (meteorTime > spawnTime)
        {
            Instantiate(meteorPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0), Quaternion.identity);
            meteorTime = 0.0f; 
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }
}
