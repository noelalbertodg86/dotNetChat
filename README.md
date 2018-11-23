# dotNetChat

Para el desarrollo de la aplicacion se tuvieron en cuenta varias tecnologias todas sobre .net con el IDE VISUAL STUDIO y varias soluciones separadas.

1- la conexion y manejo de base de datos se realizo utilizando la libreria de EntityFramework y un patron de diseno de n capas [Entity, Data, BusinessClass].
En el aplicativo se puede ver la estructura y relaciones entre bases de datos, tratando siempre de mantener un sistema simple y que cumpla con los requisitos solicitados.
Todos los mensajes enviados son guardados en la base de datos, asi como los perfiles de usuarios.


2 - El desarrollo del Robot se realizo en un proyecto de consola [Bot] que se debe iniciar a partir de la ejecucion del .exe, en este momento se va a inicar un servidor ciclico  
escuchando por la cola del rabbitMQ. Una vez  invocado se descarga desde internet la cotizacion de la bolsa y se le devuelve la misma al usuario. El robot queda escuchando cualquier 
nueva solicitud. Se aplico el patron singleton la ejecucion del robot.

3 - Para el desarrollo del chatRoom se utilizo la tecnologia basada en websockes SignalR, creando un proyecto API .NET CORE donde controladores y HUB de signalR interactuan para brindar todas funcionalidades necesarias al sistema. Login, manejo, persistencia y difusion de mensajes a los usuarios conectados. [Server]

4 - Para el desrrollo de la parte cliente se genero un webbrowser con windows forms el cual invoca una pagina html previamente disenada y probada que brinda la funcionalidad visual de Login y ChatRoom.
[Cliente]

Gracias por la opotunidad!!!!!