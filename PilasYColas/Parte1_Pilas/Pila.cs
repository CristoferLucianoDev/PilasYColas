using PilasYColas.Comun;

namespace PilasYColas.Parte1_Pilas;

/// <summary>
/// Pila generica implementada con nodos enlazados (LIFO: Last In, First Out).
/// El ultimo elemento en entrar es el primero en salir.
///
/// Todas las operaciones principales trabajan solo sobre la cima,
/// por eso Push, Pop y Peek son O(1).
/// </summary>
public sealed class Pila<T>
{
    // Referencia al nodo que esta en la cima de la pila
    private Nodo<T>? _cima;

    /// <summary>Cantidad de elementos almacenados.</summary>
    public int Count { get; private set; }

    /// <summary>Indica si la pila no tiene elementos.</summary>
    public bool IsEmpty => _cima is null;

    /// <summary>
    /// Push: coloca un elemento en la cima.
    /// El nuevo nodo apunta a la cima anterior y pasa a ser la nueva cima.
    /// </summary>
    public void Push(T valor)
    {
        _cima = new Nodo<T>(valor, _cima);
        Count++;
    }

    /// <summary>
    /// Pop: retira y devuelve el elemento de la cima.
    /// Lanza una excepcion si la pila esta vacia (subdesbordamiento).
    /// </summary>
    public T Pop()
    {
        if (_cima is null)
            throw new InvalidOperationException("La pila esta vacia.");

        T valor = _cima.Valor;
        _cima = _cima.Siguiente;   // la cima baja al siguiente nodo
        Count--;
        return valor;
    }

    /// <summary>
    /// Peek: devuelve el elemento de la cima sin retirarlo.
    /// </summary>
    public T Peek()
    {
        if (_cima is null)
            throw new InvalidOperationException("La pila esta vacia.");

        return _cima.Valor;
    }

    /// <summary>Elimina todos los elementos de la pila.</summary>
    public void Clear()
    {
        _cima = null;
        Count = 0;
    }

    /// <summary>
    /// Devuelve los elementos en orden de cima a fondo,
    /// sin modificar la pila (solo para mostrarla).
    /// </summary>
    public T[] ToArray()
    {
        var resultado = new T[Count];
        int i = 0;

        for (var actual = _cima; actual is not null; actual = actual.Siguiente)
            resultado[i++] = actual.Valor;

        return resultado;
    }
}
