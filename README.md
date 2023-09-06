# Aplicación de Gestión de Tareas

Esta aplicación de gestión de tareas te permite organizar tus actividades diarias de manera eficiente. Aquí tienes una guía para comenzar a utilizarla.

## Requerimientos Mínimos

* **Tareas Simplificadas:** Cada tarea se representa como un texto corto acompañado de una acción. Por ejemplo, "Regar las plantas".

* **Lista de Tareas:** Puedes ver todas tus tareas en una vista de lista. Además, tienes la opción de filtrarlas por texto, estado u otros criterios (opcional).

* **Agregar Tareas:** Puedes añadir nuevas tareas a tu lista en cualquier momento.

* **Editar Tareas:** La aplicación te permite editar las tareas existentes para realizar ajustes o actualizaciones.

* **Eliminar Tareas:** Si decides que una tarea ya no es relevante, puedes eliminarla de tu lista.

* **Cambiar el Estado:** Puedes marcar tus tareas como completadas o no completadas según avances en tus actividades.

## Requerimientos Técnicos
* **Frontend con ReactJS y TypeScript:** Utilizamos ReactJS junto con TypeScript para la interfaz de usuario. Esto proporciona una experiencia de usuario interactiva y sólida.

* **Creación Rápida de Aplicaciones (CRA):** La aplicación está configurada con Create React App (CRA), lo que facilita la inicialización y el desarrollo del frontend.

* **Backend con ASP.NET Core:** Para la gestión de la API y la lógica del servidor, hemos implementado ASP.NET Core 7. Esto garantiza un rendimiento eficiente y escalabilidad.

* **Base de Datos SQL con Entity Framework:** La información de tus tareas se almacena en una base de datos SQL utilizando Entity Framework. Esto asegura la persistencia de los datos y su acceso eficiente.

* **Opción de Utilizar Ant Design:** Hemos integrado la opción de utilizar Ant Design para la interfaz de usuario, aunque puedes personalizar la interfaz según tus preferencias.

## Ejecucion

Instale npm v-9.6.7, node v18.17.1 y use el comando npm start para ejecutar el front por el puerto 3000.

```bash
npm start
```

Para la ejecución del backend, instale .net core 7 y ejecute el comando dotnet run para ejecutarlo por el puerto 7246.

```bash
dotnet run
```
Si desea ejecutar el backend por otro puerto deberá cambiar el frontend en la ruta /Task-App/http-common.ts en la linea 4 el puerto por el lo va a utilizar.

```TypeScript
baseURL: "https://localhost:7246/api",
```

Si desea ejecutar el frontend por otro puerto deberá cambiar el backend en el Program.cs en la linea 66 el puerto por el lo va a utilizar.

```C#
 builder => builder.WithOrigins("http://localhost:3000")
```

Recuerde ejecutar primero el back y luego el front.

La base de datos se encuentra alojada en azure, el acceso esta disponible desde cualquier ip, la cadena de conexion se encuntra en la WebAPI en el appsettings.json
