
namespace PilasYColas.Parte2_Colas;

/// <summary>
/// Modela la atencion de clientes en una caja bancaria.
/// Los clientes esperan en una cola (FIFO), por lo que siempre se atiende
/// primero a quien llego primero; nadie puede adelantarse en la fila.
/// </summary>
public sealed class SistemaAtencion
{
    private readonly Cola<Cliente> _espera = new();
    private int _ultimoTurno;

    // Reloj simulado para que la salida sea reproducible en la demo.
    // En modo interactivo se usa la hora real del equipo.
    private readonly Func<TimeOnly> _reloj;

    /// <summary>Cliente que se encuentra en la caja en este momento.</summary>
    public Cliente? EnAtencion { get; private set; }

    /// <summary>Cantidad de clientes ya atendidos.</summary>
    public int Atendidos { get; private set; }

    public int EnEspera => _espera.Count;

    public SistemaAtencion(Func<TimeOnly>? reloj = null)
    {
        _reloj = reloj ?? (() => TimeOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    /// Llega un cliente: se le asigna el siguiente turno y se agrega al final de la cola (enqueue).
    /// </summary>
    public Cliente LlegaCliente(string nombre, string operacion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio.", nameof(nombre));

        var cliente = new Cliente(++_ultimoTurno, nombre.Trim(), operacion.Trim(), _reloj());
        _espera.Enqueue(cliente);
        return cliente;
    }

    /// <summary>
    /// Llama al siguiente cliente: se retira el frente de la cola (dequeue) y pasa a la caja.
    /// Si habia alguien en la caja, su atencion se da por terminada.
    /// Devuelve null si no hay nadie esperando.
    /// </summary>
    public Cliente? AtenderSiguiente()
    {
        FinalizarAtencion();

        if (_espera.IsEmpty)
            return null;

        EnAtencion = _espera.Dequeue();
        return EnAtencion;
    }

    /// <summary>Termina la atencion del cliente que esta en la caja.</summary>
    public Cliente? FinalizarAtencion()
    {
        var terminado = EnAtencion;

        if (terminado is not null)
        {
            Atendidos++;
            EnAtencion = null;
        }

        return terminado;
    }

    /// <summary>Muestra el cliente que se esta atendiendo.</summary>
    public void MostrarEnAtencion()
    {
        Console.WriteLine(EnAtencion is null
            ? "  En caja: (nadie, la caja esta libre)"
            : $"  En caja: {EnAtencion}");
    }

    /// <summary>
    /// Lista los clientes que esperan, en el orden exacto en que seran atendidos.
    /// Se recorre la cola sin sacar a nadie de ella.
    /// </summary>
    public void ListarEspera()
    {
        var clientes = _espera.ToArray();
        Console.WriteLine($"  Clientes en espera: {clientes.Length}");

        if (clientes.Length == 0)
        {
            Console.WriteLine("     (no hay clientes en la fila)");
            return;
        }

        for (int i = 0; i < clientes.Length; i++)
        {
            string marca = i == 0 ? "<- siguiente" : "";
            Console.WriteLine($"     {i + 1}. {clientes[i]} {marca}");
        }
    }
}
