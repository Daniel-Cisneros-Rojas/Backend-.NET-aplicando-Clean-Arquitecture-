## Backend-.NET-aplicando-Clean-Arquitecture-  
Este proyecto implementa un backend en .NET siguiendo los principios de Clean Architecture, con Entity Framework como ORM para la persistencia de datos. La entidad principal es Persona, sobre la cual se definen operaciones CRUD: creación, actualización, búsqueda y eliminación. 

Además, se incluye una implementación para registrar la entrada y salida de sesión bajo el nombre de Visits, permitiendo llevar un control de actividad de los usuarios.

La arquitectura asegura una clara separación de capas, una persistencia eficiente y una API REST que facilita la interacción con clientes externos. 

# Endpoints existentes
<img width="958" height="707" alt="endpoints" src="https://github.com/user-attachments/assets/3928a945-38ca-4f08-a323-9dbdc84c7761" />


# Pruebas Personas Creando y actualizando

<img width="1479" height="596" alt="image" src="https://github.com/user-attachments/assets/45cb780d-c830-49aa-b3f2-be788897dcbe" />


<img width="1486" height="625" alt="image" src="https://github.com/user-attachments/assets/9b083d58-28af-46d8-bd85-14272671d754" />

# Pruebas con visitas, registrando entrada y salida de sesion 

<img width="1479" height="611" alt="visitas_create" src="https://github.com/user-attachments/assets/fb76936c-4867-481d-9b8a-7c7d774c6e18" />

<img width="1442" height="611" alt="exit" src="https://github.com/user-attachments/assets/79fd6da9-9b0c-4bd1-9b0b-b42e0381f127" />

# Estructura 

Domain: entidades y reglas de negocio.

Application: casos de uso y lógica de aplicación.

Data: contexto de Entity Framework y persistencia.

WebApi: controladores y configuración de la API.

MyApp.slnx: solución principal para Visual Studio.
