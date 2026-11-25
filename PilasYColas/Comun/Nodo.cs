namespace PilasYColas.Comun;

/// <summary>
/// Nodo de una lista enlazada simple. Es la pieza base sobre la que
/// se construyen la Pila y la Cola: guarda un valor y la referencia
/// al siguiente nodo de la cadena.
/// </summary>
internal sealed class Nodo<T>
{
    public T Valor { get; }
    public Nodo<T>? Siguiente { get; set; }

    public Nodo(T valor, Nodo<T>? siguiente = null)
    {
        Valor = valor;
        Siguiente = siguiente;
    }
}
