using System;
using System.Collections.Generic;

public class Producto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int CantidadEnStock { get; set; }

    public Producto(string nombre, string descripcion, decimal precio, int cantidadEnStock)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        CantidadEnStock = cantidadEnStock;
    }

    public void AgregarAlCarrito(Carrito carrito, int cantidad)
    {
        if (CantidadEnStock >= cantidad)
        {
            carrito.AgregarProducto(this, cantidad);
            CantidadEnStock -= cantidad;
        }
        else
        {
            Console.WriteLine("Stock insuficiente para el producto " + Nombre);
        }
    }
}

public class Cliente
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string CorreoElectronico { get; set; }
    public List<Pedido> HistorialDeCompras { get; private set; }
    public Carrito Carrito { get; private set; }

    public Cliente(string nombre, string direccion, string correoElectronico)
    {
        Nombre = nombre;
        Direccion = direccion;
        CorreoElectronico = correoElectronico;
        HistorialDeCompras = new List<Pedido>();
        Carrito = new Carrito();
    }

    public void RealizarCompra()
    {
        if (Carrito.Productos.Count > 0)
        {
            var nuevoPedido = new Pedido(Carrito.Productos);
            HistorialDeCompras.Add(nuevoPedido);
            Carrito.LimpiarCarrito();
            Console.WriteLine("Compra realizada con éxito.");
        }
        else
        {
            Console.WriteLine("El carrito está vacío.");
        }
    }

    public void VerCarrito()
    {
        Carrito.MostrarProductos();
    }
}

public class Pedido
{
    public int NumeroDePedido { get; private set; }
    public DateTime Fecha { get; private set; }
    public string Estado { get; set; }
    public Dictionary<Producto, int> Productos { get; private set; }

    private static int contadorPedidos = 1;

    public Pedido(Dictionary<Producto, int> productos)
    {
        NumeroDePedido = contadorPedidos++;
        Fecha = DateTime.Now;
        Estado = "Pendiente";
        Productos = productos;
    }

    public void ConfirmarPedido()
    {
        Estado = "Confirmado";
        Console.WriteLine("Pedido confirmado.");
    }

    public void CancelarPedido()
    {
        Estado = "Cancelado";
        Console.WriteLine("Pedido cancelado.");
    }
}

public class Empleado
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string CorreoElectronico { get; set; }
    public List<Pedido> HistorialDeVentas { get; private set; }

    public Empleado(string nombre, string direccion, string correoElectronico)
    {
        Nombre = nombre;
        Direccion = direccion;
        CorreoElectronico = correoElectronico;
        HistorialDeVentas = new List<Pedido>();
    }

    public void ConsultarVentas()
    {
        Console.WriteLine($"Ventas realizadas por {Nombre}:");
        foreach (var pedido in HistorialDeVentas)
        {
            Console.WriteLine($"Pedido #{pedido.NumeroDePedido} - Estado: {pedido.Estado}");
        }
    }
}

public class Tienda
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public List<Empleado> Empleados { get; private set; }
    public List<Pedido> Pedidos { get; private set; }

    public Tienda(string nombre, string direccion, string telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Empleados = new List<Empleado>();
        Pedidos = new List<Pedido>();
    }

    public void RegistrarEmpleado(Empleado empleado)
    {
        Empleados.Add(empleado);
        Console.WriteLine("Empleado registrado con éxito.");
    }

    public void RegistrarPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
        Console.WriteLine("Pedido registrado en la tienda.");
    }
}

public class Carrito
{
    public Dictionary<Producto, int> Productos { get; private set; }

    public Carrito()
    {
        Productos = new Dictionary<Producto, int>();
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (Productos.ContainsKey(producto))
        {
            Productos[producto] += cantidad;
        }
        else
        {
            Productos.Add(producto, cantidad);
        }
        Console.WriteLine($"Producto {producto.Nombre} agregado al carrito.");
    }

    public void MostrarProductos()
    {
        Console.WriteLine("Productos en el carrito:");
        foreach (var item in Productos)
        {
            Console.WriteLine($"- {item.Key.Nombre} x{item.Value} ($ {item.Key.Precio * item.Value})");
        }
    }

    public void LimpiarCarrito()
    {
        Productos.Clear();
        Console.WriteLine("El carrito ha sido vaciado.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Tienda tienda = new Tienda("Tienda Online XYZ", "Calle Principal #123", "809-555-1234");
        Producto p1 = new Producto("Laptop Dell", "Laptop de alto rendimiento", 1200.99m, 10);
        Producto p2 = new Producto("Mouse Logitech", "Mouse inalámbrico", 25.50m, 50);
        Producto p3 = new Producto("Teclado Mecánico", "Teclado con luces RGB", 75.99m, 30);
        Cliente cliente = new Cliente("Juan Pérez", "Av. Siempre Viva #456", "juan.perez@example.com");
        p1.AgregarAlCarrito(cliente.Carrito, 1);
        p2.AgregarAlCarrito(cliente.Carrito, 2);
        cliente.VerCarrito();
        cliente.RealizarCompra();
        Empleado empleado = new Empleado("María Gómez", "Calle Secundaria #789", "maria.gomez@example.com");
        tienda.RegistrarEmpleado(empleado);
        Pedido pedido = new Pedido(cliente.HistorialDeCompras[0].Productos);
        tienda.RegistrarPedido(pedido);
        pedido.ConfirmarPedido();
        empleado.ConsultarVentas();
    }
}


using System;


public class Empleado
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string CorreoElectronico { get; set; }
    public List<Pedido> HistorialDeVentas { get; private set; }

    public Empleado(string nombre, string direccion, string correoElectronico)
    {
        Nombre = nombre;
        Direccion = direccion;
        CorreoElectronico = correoElectronico;
        HistorialDeVentas = new List<Pedido>();
    }

    public void ConsultarVentas()
    {
        Console.WriteLine($"Ventas realizadas por {Nombre}:");
        foreach (var pedido in HistorialDeVentas)
        {
            Console.WriteLine($"Pedido #{pedido.NumeroDePedido} - Estado: {pedido.Estado}");
        }
    }
}


public class Tienda
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public List<Empleado> Empleados { get; private set; }
    public List<Pedido> Pedidos { get; private set; }

    public Tienda(string nombre, string direccion, string telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Empleados = new List<Empleado>();
        Pedidos = new List<Pedido>();
    }

    public void RegistrarEmpleado(Empleado empleado)
    {
        Empleados.Add(empleado);
        Console.WriteLine("Empleado registrado con éxito.");
    }

    public void RegistrarPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
        Console.WriteLine("Pedido registrado en la tienda.");
    }
}


public class Carrito
{
    public Dictionary<Producto, int> Productos { get; private set; }

    public Carrito()
    {
        Productos = new Dictionary<Producto, int>();
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        if (Productos.ContainsKey(producto))
        {
            Productos[producto] += cantidad;
        }
        else
        {
            Productos.Add(producto, cantidad);
        }
        Console.WriteLine($"Producto {producto.Nombre} agregado al carrito.");
    }

    public void MostrarProductos()
    {
        Console.WriteLine("Productos en el carrito:");
        foreach (var item in Productos)
        {
            Console.WriteLine($"- {item.Key.Nombre} x{item.Value} ($ {item.Key.Precio * item.Value})");
        }
    }

    public void LimpiarCarrito()
    {
        Productos.Clear();
        Console.WriteLine("El carrito ha sido vaciado.");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Tienda tienda = new Tienda("Tienda Online XYZ", "Calle Principal #123", "809-555-1234");

      
        Producto p1 = new Producto("Laptop Dell", "Laptop de alto rendimiento", 1200.99m, 10);
        Producto p2 = new Producto("Mouse Logitech", "Mouse inalámbrico", 25.50m, 50);
        Producto p3 = new Producto("Teclado Mecánico", "Teclado con luces RGB", 75.99m, 30);

       
        Cliente cliente = new Cliente("Juan Pérez", "Av. Siempre Viva #456", "juan.perez@example.com");
        p1.AgregarAlCarrito(cliente.Carrito, 1);
        p2.AgregarAlCarrito(cliente.Carrito, 2);
        cliente.VerCarrito();
        cliente.RealizarCompra();
        Empleado empleado = new Empleado("María Gómez", "Calle Secundaria #789", "maria.gomez@example.com");
        tienda.RegistrarEmpleado(empleado);

      
        Pedido pedido = new Pedido(cliente.HistorialDeCompras[0].Productos);
        tienda.RegistrarPedido(pedido);

   
        pedido.ConfirmarPedido();

     
        empleado.ConsultarVentas();
    }
}
