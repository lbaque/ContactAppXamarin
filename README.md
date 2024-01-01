# ContactAppXamarin

1.- Requerimientos
* Visual Studio 2022
* Sql Sever > 2014
* .NET 8
* Web API
* Xamarin Form
   * SDK Android API Level > 29
 
2.-Configuraciones
  /*-----------------API-----------------*/
* Cambiar los datos de la cadena de conexión en el archivo appsettings.Development.json.
* Ejecutar el API desde el visual studio pero cambiar la url de acceso con la ip del computador , caso contrario no será visible desde fuera del computador.
* Para hacer el cambio debe dirigirse a la siguiente ubicación (Api.Contact/Properties/launchSettings.json) y modificar el atributo applicactionUrl con la ip del computador
  ![image](https://github.com/lbaque/ContactAppXamarin/assets/22845156/0d540430-441f-46a7-b8bc-7d8c3d0979d7)

  /*-----------------APP-----------------*/
  * Instalar emulador de android (Genymotion) o hacer uso de un dispositivo físico.
  * Conectar el dispositivo físico a la red local para que pudo hacer uso del AIP (backend).
  * Verificar a través del navegador del dispositivo que sea visible el api.
 
* ¿Cómo decidió las opciones técnicas y arquitectónicas utilizadas como parte de su 
Solución?

  Para el desarrollo de la app y api se hizo uso del lenguaje de programación C#, el aplicativo móvil funciona de manera offline ya que hace uso de tareas en segundo plano para la sincronización de la datos.
   /*-----------------API-----------------*/
  * Web Api Core
  * Patrón Repositorio.
  * Dependency Injection (Inyección de Dependencias).
  * Entity Famework core.
  * Migraciones.
  * Expresión de Funciones
  * Consultas dinámicas
  * Arquitectura MVC (Modelo Vista Controlador).
 
   /*-----------------APP-----------------*/
  * Xamarin Forms
  * Patrón Repositorio.
  * Dependency Injection (Inyección de Dependencias).
  * Entity Sqlite.
  * Expresion de Funciones
  * Consultas dinámicas
  * Arquitectura MVVM (Modelo-Vista Model-Vista).
  


• ¿Qué haría de manera diferente si se le asignara más tiempo? 
Haría uso de token de autenticación para el acceso y seguridad de las apis y el aplicativo móvil por ejemplo Identity Server , Oauth 2.0 y micro servicios.
