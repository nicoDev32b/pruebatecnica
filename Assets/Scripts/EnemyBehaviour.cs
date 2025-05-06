using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Configuración de Colores")]
    public Color color1 = Color.red;
    public Color color2 = Color.yellow;
    public Color color3 = Color.blue;

    private Renderer enemyRenderer;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        AssignRandomColor();
    }

    void AssignRandomColor()
    {
        Color[] colors = { color1, color2, color3 };
        Color randomColor = colors[Random.Range(0, colors.Length)];

        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("No se encontró Renderer en el enemigo.");
        }
    }
    void OnMouseDown()
    {
        DestruirEnemigo();
    }

    public void DestruirEnemigo()
    {
        StartCoroutine(EfectoDestruccion());
    }

    IEnumerator EfectoDestruccion()
    {
        if (enemyRenderer != null) enemyRenderer.material.color = Color.black;

        yield return new WaitForSeconds(0.1f); // Pequeño delay
        SpawnObjects spawner = FindFirstObjectByType<SpawnObjects>();
        if (spawner != null) spawner.EnemigoDestruido();

        Destroy(gameObject);
    }
}