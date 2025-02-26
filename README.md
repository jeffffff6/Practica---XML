# **Gestión de Estudiantes en XML**  

Este proyecto proporciona una aplicación en **C#** para gestionar un archivo **XML** con información sobre estudiantes superdotados. Incluye funciones para crear, cargar, modificar y visualizar los datos de los estudiantes de forma interactiva.  

## **Funcionalidades**  

✅ **Crear archivo XML**: Genera un archivo XML con datos iniciales de estudiantes.  
✅ **Cargar archivo XML**: Permite seleccionar un archivo XML existente y cargarlo en memoria.  
✅ **Mostrar estudiantes**: Muestra la información de todos los estudiantes cargados.  
✅ **Calcular nota media**: Calcula la media de las notas de cada estudiante.  
✅ **Añadir estudiante**: Permite agregar un nuevo estudiante al archivo XML.  
✅ **Eliminar estudiante**: Permite eliminar un estudiante por su ID.  
✅ **Actualizar peor nota**: Modifica la nota más baja de un estudiante específico.  

---

## **Requisitos**  

🔹 .NET SDK instalado  
🔹 Editor de código (Visual Studio, VS Code, Rider, etc.)  

---

## **Ejemplo de Uso**  

**Ejecutar el programa**  
```bash
dotnet run
```
**Seleccionar una opción del menú** 
```bash
--- Menú de Opciones ---
1. Crear archivo XML
2. Cargar archivo XML
3. Mostrar estudiantes
4. Calcular nota media
5. Añadir estudiante
6. Actualizar peor nota
7. Salir
Seleccione una opción:
```
**Ejemplo de salida al mostrar estudiantes**
```bash
El estudiante Abduskhan (ID: 1) tiene 16 años y estudia doble grado física-química.
El estudiante Hashimiri (ID: 2) tiene 15 años y estudia doble grado medicina-biomecánica.
...
```

## **Mejoras futuras**  

🔹 **Validaciones más precisas** al añadir estudiantes (evitar ID duplicados).  
🔹 **Mejor manejo de excepciones** al procesar archivos XML.  
🔹 **Warnings** tener en cuenta los warnings.

## **Autor**  

👨‍💻 **Youssef B**  
