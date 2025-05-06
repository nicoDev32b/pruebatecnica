using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnObjects : MonoBehaviour
{
    public Transform[] positions = new Transform[0];
    public Transform[] positionsDisponibles = new Transform[0];
    public GameObject prefabEnemy;
    public int objetosSpawneados, maxEnemigos, puntaje;
    public TMP_Text textoContadorEnemigos, textoPuntaje;
    private AudioSource audioX;
    private bool isSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioX = GetComponent<AudioSource>();
        objetosSpawneados = 0;
        positionsDisponibles = (Transform[])positions.Clone();
        StartCoroutine(SpawnEnemies());
    }
    private void Update()
    {
        textoContadorEnemigos.text = "Enemigos : " + objetosSpawneados.ToString();
        textoPuntaje.text = "Puntaje " + puntaje.ToString();
    }
    void StartSpawning()
    {
        if (!isSpawning && objetosSpawneados < maxEnemigos)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    // Update is called once per frame

    IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        while (objetosSpawneados < maxEnemigos)
        {
            yield return new WaitForSeconds(2f); // Espera 2 segundos
            if (positionsDisponibles.Length == 0)
            {
                positionsDisponibles = (Transform[])positions.Clone();
            }

            int randomIndex = Random.Range(0, positionsDisponibles.Length);
            Transform spawnPos = positionsDisponibles[randomIndex];

            Transform[] newArray = new Transform[positionsDisponibles.Length - 1];
            for (int i = 0, j = 0; i < positionsDisponibles.Length; i++)
            {
                if (i != randomIndex)
                {
                    newArray[j++] = positionsDisponibles[i];
                }
            }
            positionsDisponibles = newArray;

            Instantiate(prefabEnemy, spawnPos.position, spawnPos.rotation);
            objetosSpawneados++;



        }
        isSpawning = false;
    }

    public void EnemigoDestruido()
    {
        audioX.Play();
        puntaje += 10;
        objetosSpawneados--;
        StartSpawning();
    }
}
