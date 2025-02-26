using System;
using System.Xml.Linq;
// using System.Xml.Serialization;
namespace ex_1;

class Program
{
    static void Main(string[] args){
        XDocument doc = new XDocument();
        Menu(ref doc);
    }
    static void Menu(ref XDocument doc){
        int opcion;
        do{
            Console.WriteLine("\n--- Menú de Opciones ---");
            Console.WriteLine("1. Crear archivo XML");
            Console.WriteLine("2. Cargar archivo XML");
            Console.WriteLine("3. Mostrar estudiantes");
            Console.WriteLine("4. Calcular nota media");
            Console.WriteLine("5. Añadir estudiante");
            Console.WriteLine("6. Actualizar peor nota");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out opcion)){
                switch (opcion){
                    case 1:
                        Console.WriteLine("------------------------");
                        CrearArchivoXml();
                        break;
                    case 2:
                        Console.WriteLine("------------------------");
                        CargarArchivoXML(ref doc);
                        break;
                    case 3:
                        Console.WriteLine("------------------------");
                        MostrarEstudiantes(doc);
                        break;
                    case 4:
                        Console.WriteLine("------------------------");
                        CalcularNotaMedia(doc);
                        break;
                    case 5:
                        Console.WriteLine("------------------------");
                        AñadirEstudiante(ref doc);
                        break;
                    case 6:
                        Console.WriteLine("------------------------");
                        ActualizarPeorNota(ref doc);
                        break;
                    case 7:
                        Console.WriteLine("------------------------");
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("------------------------");
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        break;
                }
            }
            else{
                Console.WriteLine("Entrada inválida. Introduce un número del 1 al 7.");
            }

        } while (opcion != 6);
    }
    public static void CrearArchivoXml(){
        string? path;
        Console.WriteLine("Escribe como quieres que se llame el archivo (sin el .xml): ");
        do
        {
            path = Console.ReadLine()?.Trim().Replace(" ", "_");
        } while (string.IsNullOrEmpty(path));

        XDocument doc = new XDocument(
        new XElement("estudiantes_superdotados",
            new XElement("estudiante",
                new XAttribute("id", 1),
                new XElement("nombre", "Abduskhan"),
                new XElement("edad", 16),
                new XElement("carrera", "doble grado fisica-quimica"),
                new XElement("notas",
                    new XElement("nota_fisica", 10),
                    new XElement("nota_quimica", 9.9),
                    new XElement("nota_matematicas", 9.5),
                    new XElement("nota_programacion", 9.8)
                )
            ),
            new XElement("estudiante",
                new XAttribute("id", 2),
                new XElement("nombre", "Hashimiri"),
                new XElement("edad", 15),
                new XElement("carrera", "doble grado medicina-biomecanica"),
                new XElement("notas",
                    new XElement("nota_medicina", 10),
                    new XElement("nota_biomecanica", 8.45),
                    new XElement("nota_biologia", 9.3),
                    new XElement("nota_quimica", 9.7)
                )
            ),
            new XElement("estudiante",
                new XAttribute("id", 3),
                new XElement("nombre", "Rashimid"),
                new XElement("edad", 15),
                new XElement("carrera", "ingenieria aeroespacial"),
                new XElement("notas",
                    new XElement("nota_aeroespaciales", 10),
                    new XElement("nota_fisica", 9.8),
                    new XElement("nota_matematicas", 9.6),
                    new XElement("nota_dibujo_tecnico", 9.4)
                )
            ),
            new XElement("estudiante",
                new XAttribute("id", 4),
                new XElement("nombre", "Abdulah"),
                new XElement("edad", 15),
                new XElement("carrera", "matematicas"),
                new XElement("notas",
                    new XElement("nota_matematicas", 9.25),
                    new XElement("nota_fisica", 9.5),
                    new XElement("nota_programacion", 9.3),
                    new XElement("nota_estadistica", 9.7)
                )
            )
        )
        );
        doc.Save(path + ".xml");
    }
    public static void CargarArchivoXML(ref XDocument doc){
        string nomFitxer;
        do{
            Console.WriteLine("Escribe el nombre del archivo que quieres cargar (sin el .xml): ");
            nomFitxer = Console.ReadLine()?.Trim().Replace(" ", "_");
        } while (string.IsNullOrEmpty(nomFitxer));
        try{
            doc = XDocument.Load(nomFitxer + ".xml");
            Console.WriteLine($"Archivo '{nomFitxer}.xml' cargado correctamente.");
        }
        catch (Exception ex){
            Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
        }
    }
    public static void MostrarEstudiantes(XDocument doc){
    if (doc.Root == null){
        Console.WriteLine("Error: No se ha cargado ningún archivo XML.");
        return;
    }

    foreach(var estudiante in doc.Descendants("estudiante")){
        Console.WriteLine($"El estudiante {estudiante.Element("nombre")?.Value} (ID: {estudiante.Attribute("id")?.Value}) " +
                          $"tiene {estudiante.Element("edad")?.Value} años y estudia {estudiante.Element("carrera")?.Value}.");
    }
}
    public static void CalcularNotaMedia(XDocument doc){
        if (doc.Root == null){
            Console.WriteLine("El documento XML no está cargado.");
            return;
        }
        else{
            foreach (var estudiante in doc.Descendants("estudiante")){
                List<double> listaNotas = new List<double>();

                foreach (var nota in estudiante.Element("notas").Elements())
                {
                    double valorNota = double.TryParse(nota.Value, System.Globalization.NumberStyles.Any, 
                        System.Globalization.CultureInfo.InvariantCulture, out double resultado) ? resultado : 0;

                    listaNotas.Add(valorNota);
                }

                double media = listaNotas.Average();

                Console.WriteLine($"Estudiante: {estudiante.Element("nombre")?.Value} - Nota Media: {media:F2}");
            }
        }
    }
    public static void AñadirEstudiante(ref XDocument doc){
        if (doc.Root == null){
            Console.WriteLine("El documento XML no está cargado.");
            return;
        }
        else{
            Console.WriteLine("Introduce los datos del nuevo estudiante:");
            Console.Write("ID: ");      int id = int.Parse(Console.ReadLine());
            Console.Write("Nombre: ");  string nombre = Console.ReadLine();
            Console.Write("Edad: ");    int edad = int.Parse(Console.ReadLine());
            Console.Write("Carrera: "); string carrera = Console.ReadLine();

            XElement notas = new XElement("notas");string input;

            do{
                Console.Write("Introduce el nombre de la asignatura (o 'fin' para terminar): ");
                string asignatura = Console.ReadLine();
                if (asignatura == "fin") break;

                Console.Write($"Introduce la nota para {asignatura}: ");
                double nota = double.Parse(Console.ReadLine());

                notas.Add(new XElement($"nota_{asignatura}", nota));
            } while (true);

            XElement nuevoEstudiante = new XElement("estudiante",
                new XAttribute("id", id),
                new XElement("edad", edad),
                new XElement("nombre", nombre),
                new XElement("carrera", carrera),
                notas
            );

            doc.Element("estudiantesSuperdotados").Add(nuevoEstudiante);
            doc.Save("estudiantes_superdotados.xml");
            Console.WriteLine("Estudiante agregado correctamente.");
        }
    }
    public static void EliminarEstudiante(ref XDocument doc){
        if (doc.Root == null){
            Console.WriteLine("El documento XML no está cargado.");
            return;
        }
        else{
            Console.Write("Introduce el ID del estudiante a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            foreach (var estudiante in doc.Descendants("estudiante"))
            {
                if (int.Parse(estudiante.Attribute("id").Value) == id)
                {
                    estudiante.Remove();
                    doc.Save("estudiantes_superdotados.xml");
                    Console.WriteLine("Estudiante eliminado correctamente.");
                    return;
                }
            }
            Console.WriteLine("Estudiante no encontrado.");
        }
    }
    public static void ActualizarPeorNota(ref XDocument doc){
        if (doc.Root == null){
            Console.WriteLine("El documento XML no está cargado.");
            return;
        }
        else{
            Console.Write("Introduce el ID del estudiante a actualizar la nota mas baja: ");
            int id = int.Parse(Console.ReadLine());
            foreach (var estudiante in doc.Descendants("estudiante"))
            {
                if (int.Parse(estudiante.Attribute("id").Value) == id)
                {
                    var notas = estudiante.Element("notas").Elements();
                    var peorNota = notas.OrderBy(n => double.Parse(n.Value)).First();
                    
                    Console.WriteLine($"La peor nota actual es {peorNota.Name.LocalName}: {peorNota.Value}");
                    Console.Write($"Introduce la nueva nota para {peorNota.Name.LocalName}: ");
                    double nuevaNota = double.Parse(Console.ReadLine());
                    peorNota.Value = nuevaNota.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    
                    doc.Save("estudiantes_superdotados.xml");
                    Console.WriteLine("Estudiante actualizado correctamente.");
                    return;
                }
            }
            Console.WriteLine("Estudiante no encontrado.");
        }
    }
}