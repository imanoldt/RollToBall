using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public Data_ENEMYCONTROLLER datosEnemigo; // Referencia al ScriptableObject
    
    public float fuerzaSalto = 10f;            // Fuerza del salto
    public float tiempoEntreSaltos = 2f;      // Tiempo entre saltos

    private Rigidbody rb;
    private enum Estado { Seguimiento, Ataque, Pausa }
    private Estado estadoActual = Estado.Seguimiento;

    delegate void eventoAtaque();
    static eventoAtaque eventoAtaca;
    static eventoAtaque eventoAburre;

    private int hitCount = 0;
    private int atacando;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        atacando = 0;
        eventoAtaca += anadirAtacante;
        eventoAburre += quitarAtacante;
    }

    void anadirAtacante()
    {
        atacando++;
        if (atacando >= 3)
        {
        }
    }

    void quitarAtacante()
    {
        atacando--;
    }

    private void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, player.position);

        switch (estadoActual)
        {
            case Estado.Seguimiento:
                // Mirar al jugador
                Vector3 direccionAlJugador = (new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position).normalized;

                // Mover al enemigo hacia el jugador
                transform.Translate(direccionAlJugador * datosEnemigo.velocidad * Time.deltaTime, Space.World);

                // Asegurarse de que el enemigo mire al jugador
                transform.LookAt(player);

                // Cambiar al modo de ataque si estamos lo suficientemente cerca
                if (distanciaAlJugador <= datosEnemigo.distaciaAtaque)
                {
                    CambiarEstado(Estado.Ataque);
                }
                break;

            case Estado.Ataque:
                eventoAtaca?.Invoke();

                CambiarEstado(Estado.Pausa);
                StartCoroutine(Pausado());
                break;
        }
    }


    private void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }


    private IEnumerator Pausado()
    {
        yield return new WaitForSeconds(datosEnemigo.tiempoAtaque);
        eventoAburre?.Invoke();
        CambiarEstado(Estado.Seguimiento);
    }

    private void OnDestroy()
    {
        if (estadoActual == Estado.Pausa)
        {
            eventoAburre?.Invoke();
        }
    }
}
