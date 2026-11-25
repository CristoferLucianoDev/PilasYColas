using PilasYColas.Comun;

namespace PilasYColas.Parte2_Colas;

/// <summary>
/// Cola generica implementada con nodos enlazados (FIFO: First In, First Out).
/// El primer elemento en entrar es el primero en salir.
///
/// Se mantienen dos referencias: el frente (por donde se atiende)
/// y el final (por donde se agrega). Asi Enqueue y Dequeue son O(1).
/// </summary>
public sealed class Cola<T>
{
    private Nodo<T>? _frente;   // primer elemento, el proximo en salir
    private Nodo<T>? _final;    // ultimo elemento en llegar

    /// <summary>Cantidad de elementos en espera.</summary>
    public int Count { get; private set; }

    /// <summary>Indica si la cola no tiene elementos.</summary>
    public bool IsEmpty => _frente is null;

    /// <summary>
    /// Enqueue: agrega un elemento al final de la cola.
    /// </summary>
    public void Enqueue(T valor)
    {
        var nuevo = new Nodo<T>(valor);

        if (_final is null)
        {
            // Cola vacia: el nuevo nodo es a la vez frente y final
            _frente = nuevo;
            _final = nuevo;
        }
        else
        {
            // Se enlaza detras del ultimo y pasa a ser el nuevo final
            _final.Siguiente = nuevo;
            _final = nuevo;
        }

        Count++;
    }

    /// <summary>
    /// Dequeue: retira y devuelve el elemento del frente.
    /// Lanza una excepcion si la cola esta vacia.
    /// </summary>
    public T Dequeue()
    {
        if (_frente is null)
            throw new InvalidOperationException("La cola esta vacia.");

        T valor = _frente.Valor;
        _frente = _frente.Siguiente;

        // Si se retiro el unico elemento, el final tambien queda vacio
        if (_frente is null)
            _final = null;

        Count--;
        return valor;
    }

    /// <summary>
    /// Peek: devuelve el elemento del frente sin retirarlo.
    /// </summary>
    public T Peek()
    {
        if (_frente is null)
            throw new InvalidOperationException("La cola esta vacia.");

        return _frente.Valor;
    }

    /// <summary>
    /// Devuelve los elementos en orden de llegada (frente a final),
    /// sin modificar la cola (solo para mostrarla).
    /// </summary>
    public T[] ToArray()
    {
        var resultado = new T[Count];
        int i = 0;

        for (var actual = _frente; actual is not null; actual = actual.Siguiente)
            resultado[i++] = actual.Valor;

        return resultado;
    }
}
