using System;
using System.Xml.Linq;
// using System.Xml.Serialization;
namespace ex_1;

class Program
{
    static void Main(string[] args)
    {
        XDocument doc = new XDocument();
        CrearArchivoXml();
        CargarArchivoXML(ref doc);
        CalcularNotaMedia(doc);
        AñadirEstudiante(ref doc);
        CalcularNotaMedia(doc);
    }
    public static void CrearArchivoXml()
    {
        string? path;
        path = "estudiantes_superdotados";
        Console.WriteLine("Escribe como quieres que se llame el archivo: ");
        // do
        // {
        //     path = Console.ReadLine()?.Trim().Replace(" ", "_");
        // } while (string.IsNullOrEmpty(path));

        XDocument doc = new XDocument(
        new XElement("estudiantesSuperdotados",
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
        nomFitxer = "estudiantes_superdotados";
        // do{
        //     Console.WriteLine("Escribe el nombre del archivo que quieres cargar: ");
        //     nomFitxer = Console.ReadLine()?.Trim().Replace(" ", "_");
        // } while (string.IsNullOrEmpty(nomFitxer));

        doc = XDocument.Load(nomFitxer + ".xml");

        foreach(var estudiante in doc.Descendants("estudiante")){
            Console.WriteLine("El estudiante " + estudiante.Element("nombre")?.Value + " (con id:" + estudiante.Attribute("id")?.Value
            + ") tiene " + estudiante.Element("edad")?.Value + " años y estudia: " + estudiante.Element("carrera")?.Value);
        }
    }
    public static void CalcularNotaMedia(XDocument doc)
    {
        foreach (var estudiante in doc.Descendants("estudiante"))
        {
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

    public static void AñadirEstudiante(ref XDocument doc){
        
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