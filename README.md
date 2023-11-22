# Descripción general del trabajo.

El objetivo de este proyecto fue la creación de una REST API para un Ecommerce de una tienda de ropa. Hicimos uso de un repositorio remoto GIT para trabajar de forma autónoma, e hicimos empleo de Git Flow para gestionar nuestro trabajo. Para el fusionamiento usamos Pull Requests. Implementamos un tablero de kanban en Jira como herramienta ágil de gestión de proyectos. Desarrollamos utilizando la metodología TDD para toda la funcionalidad y logramos mantener una alta cobertura de código en todo el proyecto. La documentación se desarrolla según el modelo de vistas 4 + 1.


La REST API puede accederse via http://pawn-ecommerce.somee.com/api.

# Vista Lógica (Logical View):

La Vista Lógica nos permite sumergirnos en la estructura y funcionalidad principal de nuestra API de Ecommerce. Es aquí donde desglosamos las clases y relaciones que forman el núcleo de nuestro sistema. Cada clase, con su respectiva responsabilidad y cooperación con otras clases, ha sido diseñada para garantizar una ejecución fluida y eficiente de las operaciones requeridas en un contexto de nuestro obligatorio.


## Diagrama General de Clases y Arquitectura:
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/99bf4de6-a38b-44e0-8cb7-42dca05a490f)

 \
Al ser un diagrama muy extenso se hace difícil de entender su contenido en esta imagen, por lo cual dejamos el enlace al UML en el anexo , [o también siguiendo este link. ](https://viewer.diagrams.net/?tags=%7B%7D&highlight=0000ff&edit=_blank&layers=1&nav=1&title=Diagrama%20A2.drawio#Uhttps%3A%2F%2Fdrive.google.com%2Fuc%3Fid%3D190L4t8WaV-6J_QlnTTz5ctm0tWjLnW2s%26export%3Ddownload) \
 \
Como se puede observar, se mantuvo la arquitectura en capas que se diseñó en la instancia anterior, ya que nos resultó efectivo para cumplir con los siguientes principios: 


### Relaciones y Asociaciones:

Nuestro sistema está interconectado para facilitar operaciones eficientes. Por ejemplo, un Product está vinculado a su respectiva Category y Brand, permitiendo filtrados y búsquedas más intuitivas. Por otro lado, una Sale, al capturar una transacción, alude al producto vendido y al usuario que hizo la compra. La conexión entre clases es un tema que vamos a abordar en el apartado de modelo de base de datos.


### Abstracciones y Polimorfismo:

Siguiendo los principios SOLID, hemos incorporado interfaces para cada entidad principal: IUser, IProduct, entre otras. Estas interfaces no son simples protocolos, sino que están diseñadas para garantizar que cualquier implementación concreta de una entidad cumpla con ciertas reglas y estructuras. Además, nos brindan la flexibilidad de incorporar polimorfismo si, en etapas futuras, se necesitara adaptar o expandir la funcionalidad de una entidad.


### Cohesión y Desacoplamiento:

Hemos puesto un énfasis especial en mantener nuestras clases cohesivas y desacopladas. Gracias a la implementación de interfaces, son pocas las clases en nuestro proyecto que dependen directamente de una implementación específica. En lugar de ello, dependen de la abstracción de la entidad. Esto no solo garantiza un código más limpio y modular, sino que también nos prepara para futuros desarrollos y adaptaciones sin grandes alteraciones estructurales. 


## Modelo de Base de Datos:

![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/72e1dde2-f884-4e4d-83b8-7b98f90a1016)

Para gestionar todos los objetos del negocio mantuvimos la misma estructura para nuestro modelado de datos:

Para crear estas tablas en nuestra base de datos se hizo uso de la herramienta Entity Framework Core, específicamente el método OnModelCreating. Este método nos permite personalizar las tablas a nuestro criterio en lugar de trabajar con las tablas autogeneradas, las cuales son creadas a conveniencia por EFCore. Durante el proceso de realización del obligatorio fuimos iterando sobre varias versiones de la base de datos, utilizando migraciones para actualizarla ante cada nueva innovación, hasta llegar a la versión final, la cual consideramos que asegura integridad y eficiencia en nuestra base de datos,. 

Como se puede ver en el diagrama, cada objeto tiene su propia tabla en la cual se guardan sus respectivos atributos y relaciones en el caso de ser 1 a N como la de Product y Category.  Sin embargo, también se puede observar la existencia de dos tablas que no mapean objetos particulares: ProductColors y SaleProducts. Estas tablas fueron diseñadas específicamente para gestionar relaciones de muchos a muchos (N a N), como es el caso de Product y Sale así como de Product y Color. En bases de datos relacionales, como SQL ,las relaciones directas N a N son problemáticas, debido a que no se pueden representar eficientemente con una simple clave foránea en una de las tablas, como es el caso de relaciones 1 a N. En lugar de eso, se utiliza una tabla intermedia, o Join Table, que tiene claves foráneas a ambas tablas que se están relacionando.

Por ejemplo, sin esta tabla  ProductColors, si quisiéramos representar que un producto está disponible en varios colores, tendríamos que repetir las entradas del producto para cada color. Esto implicaría que si 100 productos tienen color Azul, debería tener 100 entradas del color Azul (cada uno con el Id del producto correspondiente) en lugar de tener solo una entrada de Azul en la tabla Color.  Esto no solo sería ineficiente en términos de almacenamiento, sino que también complicaría las actualizaciones y crearía redundancia. Con la tabla intermedia, cada combinación de producto y color se registra una vez, y es mucho más sencillo consultar o actualizar la disponibilidad de colores para un producto.

El beneficio de este enfoque es múltiple:

**Integridad de datos:** Se evita la redundancia y se garantiza la integridad referencial, lo que significa que no se pueden tener colores para productos que no existen, o viceversa.

**Eficiencia en consultas**: Las tablas intermedias facilitan la realización de consultas. Por ejemplo, en nuestro caso es posible encontrar fácilmente todos los colores de un producto en particular o todos los productos de un color específico mediante una simple consulta JOIN.

**Flexibilidad:** Las tablas intermedias ofrecen una gran flexibilidad para expandir el diseño en el futuro. Por ejemplo, si en algún momento se nos pide añadir un atributo adicional a la relación entre productos y colores, podemos hacerlo agregando una columna a la tabla ProductColors sin tener que modificar las tablas Products o Colors. Lo mismo podemos hacer con SaleProduct, por ejemplo, si más adelante nos interesa guardar la información sobre en qué color se compró un producto, ya que podríamos realizarlo agregando una columna a esta tabla .

**Optimización del almacenamiento:** Al evitar la redundancia, se optimiza el espacio de almacenamiento. Esto es especialmente valioso en bases de datos de gran tamaño, como podría ser nuestro caso.


# Vista de Procesos (Process View) y Vista de Escenarios(Scenarios View):

Al concluir nuestro análisis de la Vista Lógica y su estructuración clasificada, nos movemos hacia una perspectiva más dinámica del sistema: la combinación de la Vista de Procesos y la Vista de Escenarios. La Vista de Procesos pone el foco en los detalles de rendimiento, escalabilidad y concurrencia. Nos ayuda a visualizar cómo nuestro sistema gestiona interacciones simultáneas, y cómo los distintos componentes se sincronizan entre sí. Paralelamente, la Vista de Escenarios se enfoca en situaciones concretas de uso, mostrándonos cómo se espera que el sistema responda a escenarios específicos. Los diagramas de secuencia juegan un papel protagonista en ambas vistas, permitiéndonos no solo comprender la secuencia de interacciones entre objetos y componentes a través de mensajes y operaciones, sino también situar estas interacciones en el marco de situaciones reales del negocio. En estos tres ejemplos podemos observar tanto el flujo operativo como las expectativas funcionales de nuestro obligatorio, así como el cambio de estas respecto a la entrega anterior, siendo el último ejemplo el que más ha cambiado de los tres.


## Ejemplo 1: Login de usuario
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/7f280273-beb7-4881-87bf-0efa35dbcfd7)


En este caso, el usuario inicia el proceso de autenticación enviando una petición POST con su email y contraseña a la ruta /api/session/login. Al recibir la solicitud, el Session Controller valida la información y la delega al Session Service para la autenticación. Este servicio, a su vez, consulta al User Repository usando el email proporcionado para buscar al usuario en la base de datos. Una vez obtenidos los detalles del usuario, el Session Service compara la contraseña proporcionada con la almacenada en la base de datos. Dependiendo de la verificación, si la contraseña coincide, se genera un token que el Session Controller devuelve al usuario; de lo contrario, se envía una respuesta "Unauthorized" al solicitante. Este flujo garantiza una autenticación estructurada y eficiente, delineando claramente las responsabilidades entre las operaciones y la lógica de negocio.


## Ejemplo 2: Get de un producto por Id
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/22255008-cdf0-42d2-b95c-357e481690da)

 \
Al solicitar detalles de un producto, un usuario envía una petición a /api/product/id, donde "id" es el identificador único del producto. Esta petición es procesada por el Product Controller que valida el ID y, a continuación, acude al Product Service para buscar el producto correspondiente. El Product Service consulta al Product Repository, que se encarga de interactuar con la base de datos y buscar el producto mediante una expresión. Si el producto se encuentra, los detalles son enviados de vuelta a través del Product Service al Product Controller, que finalmente devuelve una respuesta positiva al usuario. En caso contrario, se gestiona adecuadamente la situación informando al usuario que el producto no fue encontrado, garantizando así un flujo eficiente y coherente en la recuperación de datos del producto.


## Ejemplo 3: Creación y Adición de una Sale al sistema
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/7f6a8ee5-7dec-4a76-9f28-5376803c7e64)


En esta ocasión el flujo de crear una Sale ha cambiado dado que hay que manejar Stock de los productos y a su vez hay que obtener el usuario registrado antes de ingresar la venta, y de esta forma poder registrar el historial de compras del mismo. La parte inicial (arriba) se encarga de obtener el usuario logueado a través del token en SessionService y luego ProductService se encarga de chequear el stock de los productos del carrito y verifica si se puede realizar la compra o no, donde hay 3 posibles casos: \
 \
Caso 1: Si hay stock para todos los productos del carrito, entonces la compra se lleva a cabo: 
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/a6ae566b-9f59-438b-84a4-bdbd404f0d09)

Caso 2: Si no hay stock para todos los productos, devuelve el carrito actualizado y el statusCode 409 con un mensaje.
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/362bca0d-3519-4fde-8806-52a84bfda6fb)

Caso 3: Si se intenta hacer una venta con el carrito vacío se devuelve un StatusCode de BadRequest.
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/b85427f5-bc2e-425f-b5fd-8a44e8a09c7e)

# Vista de Desarrollo (Development View):


## Paquetización y Relaciones de Dependencia:
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/5f3b75fb-ef1b-411e-8388-f6d88c069e85)

Tras una exploración detallada de cómo se compone nuestro sistema desde una perspectiva lógica y de cómo interactúan las diversas capas de la aplicación, es esencial adentrarnos en cómo estas capas están organizadas a nivel de código en los namespaces. \
 \
En este sencillo diagrama de paquetes podemos ver cómo están conectados los namespaces de nuestro proyecto. ilustra la organización y dependencia de los namespaces en nuestra API: Controller, Service y Repository. Si bien no es muy detallado, se puede identificar una estructura clara que refleja la arquitectura por capas que hemos utilizado. Aquí, el namespace Service emerge como el núcleo lógico central de nuestra aplicación. Mientras los Controllers se encargan de interpretar y validar solicitudes del usuario, canalizándolas hacia la lógica de negocio en Service, el Repository se alinea con este diseño al implementar las interfaces definidas en Service, en lugar de tener las interfaces en Repository. Esta disposición no solo refuerza una inversión de dependencias, alineándose con el principio de inversión de dependencia de los principios SOLID, sino que también destaca cómo las capas externas se configuran y colaboran en torno a la lógica central de Service.


## Estructura de Capas y Asignación de Responsabilidades:


### Controllers:

Son el primer punto de contacto con cualquier solicitud externa. Funcionan como la interfaz entre el usuario y el sistema, manejando la interacción y canalizando las solicitudes hacia los servicios apropiados. Su responsabilidad principal es interpretar las solicitudes HTTP, validar los datos entrantes y transformarlos en estructuras comprensibles para la capa de servicios. Además, se encargan de devolver respuestas adecuadas al cliente, ya sea un resultado esperado o mensajes de error en caso de fallas.Cada controlador es altamente cohesivo, concentrándose en una entidad específica de negocio y manteniendo una estrecha agrupación de operaciones y lógica relacionada. Tradicionalmente, los controladores no se abstraen porque representan una capa de entrada específica de la aplicación, donde cada controlador maneja rutas y acciones HTTP distintas que no se prestan a una generalización útil. Además, en la mayoría de los frameworks MVC, los controladores ya están diseñados para ser extendidos de clases base que proporcionan funcionalidades comunes, por lo que la abstracción adicional a través de interfaces puede resultar redundante. Además, la abstracción podría introducir una capa de complejidad que no aporta beneficios significativos, ya que los controladores ya operan en el nivel de abstracción adecuado para manejar solicitudes y enviar respuestas. En este sentido, la ausencia de abstracción en los controladores no solo es intencional sino también práctica, alineándose con la naturaleza directa y acción-específica que los controladores deben manejar en la arquitectura MVC.


### Services:

Aquí es donde reside la lógica de negocio. Estos servicios trabajan en estrecha colaboración con los controladores y repositorios para garantizar que las operaciones solicitadas se realicen correctamente. Los servicios gestionan las reglas de negocio y validaciones. Se responsabilizan de operaciones más complejas, como cálculos, transformaciones y coordinación entre diferentes entidades. También tienen la responsabilidad de trabajar con los repositorios para llevar a cabo operaciones CRUD y otras operaciones específicas. Alta Cohesión:

Cada servicio dentro del paquete está enfocado en una única área de responsabilidad. Por ejemplo, ProductService maneja todas las operaciones relativas a los productos, y PromotionService se ocupa exclusivamente de la lógica de promociones. Esta cohesión significa que cada servicio es independiente y modular, lo que facilita el mantenimiento y la escalabilidad. Las modificaciones en un área del dominio de negocio se contienen dentro de su servicio correspondiente, reduciendo el riesgo de efectos secundarios en otras partes del sistema.

Nivel Adecuado de Abstracción:

Las interfaces como definen los contratos para los servicios, lo que significa que la implementación subyacente puede variar sin impactar a los consumidores de estas interfaces. Esta capa de abstracción es beneficiosa porque desacopla la lógica de negocio de las implementaciones concretas, permitiendo así la flexibilidad en la elección o cambio de las implementaciones subyacentes. Por ejemplo, si se desea cambiar la lógica detrás de las promociones, se puede introducir una nueva implementación de IPromotionService sin necesidad de alterar los controladores o cualquier otro componente que dependa de las promociones.

La abstracción no es excesiva; es precisa y orientada a propósitos específicos dentro de la lógica de negocio. No hay interfaces sin un propósito claro ni abstracciones que no añadan valor. Esto es crucial porque una abstracción excesiva puede llevar a una complejidad innecesaria, haciendo que el sistema sea más difícil de entender y mantener. En cambio, la abstracción se aplica aquí para permitir la prueba y la sustitución de componentes, y para apoyar los principios SOLID, como la inversión de dependencias y el principio abierto/cerrado, ambos esenciales para un diseño robusto y mantenible.


### Repositories:

Representan la capa más cercana a la base de datos y están encargados de interactuar directamente con ella.Los repositorios cargan la responsabilidad de realizar las operaciones CRUD y otras operaciones específicas en la base de datos. Este paquete, a pesar de su directa responsabilidad sobre las operaciones de la base de datos, actualmente no encapsula las abstracciones (interfaces) de los repositorios que se utilizan para tales operaciones. En cambio, estas interfaces residen en el paquete Service, lo que significa que los repositorios están directamente acoplados a sus implementaciones específicas. Aunque esta configuración puede aumentar el riesgo de inestabilidad debido a la dependencia directa de las clases consumidoras en las implementaciones concretas, se ha optado intencionalmente por esta disposición para centralizar la lógica de negocio y las definiciones de operaciones en un solo lugar, lo cual simplifica la inyección de dependencias y permite una mayor cohesión. La estructura está diseñada para facilitar la introducción de nuevas bases de datos o estrategias de persistencia, permitiendo la creación de nuevos paquetes de Repository que implementen las interfaces definidas en Service. Esta elección refleja un compromiso consciente con la flexibilidad y la adaptabilidad a largo plazo.


### Middleware y Filters:

Incorporamos middlewares y filters que tienen la responsabilidad de manejar la autenticación de los usuarios y garantizan que todas las solicitudes sean legítimas. El middleware de autenticación verifica cada solicitud para asegurar que proviene de un usuario autenticado. Una vez que una solicitud está autenticada, la autorización basada en roles determina qué acciones puede realizar un usuario, garantizando que las operaciones sensibles estén protegidas y solo sean accesibles para usuarios con los permisos adecuados.


### Beneficio de nuestra elección de arquitectura:


#### 1. Escalabilidad:

Nuestra arquitectura modular brinda una enorme flexibilidad para la evolución del sistema. Esta organización hace que el sistema pueda adaptarse fácilmente a cambios en los requisitos sin requerir una reescritura completa o cambios masivos. Si en el futuro se quisiera integrar un nuevo método de pago o una nueva funcionalidad, gracias a la modularidad, se podría añadir como un nuevo módulo o servicio sin perturbar las funciones existentes.


#### 2. Mantenibilidad:

Al tener un diseño claro y una separación de responsabilidades se reduce significativamente la complejidad del código. Esto se traduce en un código más legible y, por ende, más fácil de mantener. Al adherirse a los principios SOLID, el código no solo es más estructurado, sino que también es más predecible. Si surge un error o se necesita actualizar una funcionalidad, se puede identificar rápidamente en qué módulo o capa trabajar. Basándonos en el Principio de Inversión de Dependencia, utilizamos interfaces para definir contratos claros entre las diferentes capas. Estas interfaces actúan como acuerdos que las clases deben respetar, lo que promueve un código más limpio y modular.


#### 3. Testabilidad:

Las interfaces y el principio de inversión de dependencia permiten la creación de versiones simuladas (mocks) de ciertos componentes, facilitando la realización de pruebas unitarias y de integración. Esta estructura favorece un desarrollo basado en pruebas (TDD), garantizando que cada función o módulo cumpla con su propósito esperado. Al querer probar un servicio en particular, podemos mockear el repositorio asociado, permitiendo testear la lógica del servicio de manera aislada sin la necesidad de interactuar con la base de datos real.


#### 4. Seguridad:

A través del uso de middlewares para autenticación y autorización, se asegura que cada solicitud sea legítima y que los usuarios solo puedan realizar acciones que se les permita según su rol. Además, esta capa adicional de seguridad minimiza los riesgos asociados con ataques o intentos de acceso no autorizado.


## Análisis de Métricas

Como parte del proceso de revisión y mejora de nuestro sistema Ecommerce, se llevó a cabo un análisis de métricas utilizando herramientas de evaluación de código estático. La siguiente sección presenta observaciones detalladas derivadas de este análisis, específicamente examinando el equilibrio entre abstracción e inestabilidad en nuestros ensamblajes principales.


### Abstracción vs Inestabilidad
   
La herramienta NDepend proporciona una visualización que correlaciona la abstracción y la inestabilidad, revelando cómo nuestros ensamblajes se alinean con prácticas de diseño sólidas y principios SOLID: \
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/c77493d0-e62f-482d-bea5-5815813534b2)



#### Service:

El ensamblaje Service presenta una abstracción de 0.30 y una inestabilidad de 0.81, con una distancia de la secuencia principal de 0.08. Estos números indican que, aunque la abstracción es moderada, reflejando un esfuerzo por seguir el Principio de Abstracciones Estables, la inestabilidad es significativa, lo que sugiere una fuerte dependencia de otros componentes del sistema. A pesar de esta inestabilidad, la relativa proximidad a la secuencia principal sugiere que el ensamblaje Service mantiene un balance adecuado entre abstracción e inestabilidad, mostrando un diseño flexible y robusto que aún se adhiere en gran medida a los principios SOLID. En retrospectiva notamos que quizás este paquete tiene una cantidad excesiva de clases, y por ende no todas tienen que ver entre sí, lo cual va en contra del Principio de Clausura Común.


#### Repository:

Por otro lado, el ensamblaje Repository registra una abstracción de 0 y una inestabilidad de 0.88, con una distancia de la secuencia principal de 0.09. Esto se debe a que todas las interfaces de repositorios se alojan en Service, ya que forman parte del “core” de nuestra lógica de negocios, con el fin de poder expandir fácilmente nuestro sistema de persistencia respetando el principio Open-Closed. Esta ausencia de abstracción y alta inestabilidad apunta a un ensamblaje altamente concreto con muchas dependencias salientes, lo cual es característico de los repositorios que actúan como la interfaz directa con la persistencia de datos. Aunque esta configuración conlleva ciertos riesgos de mantenibilidad, es coherente con su función de interactuar con la base de datos, lo cual justifica su diseño orientado hacia la implementación. Esto resuena con el Principio de Responsabilidad Única, ya que encapsula la lógica de acceso a datos, aislando así las preocupaciones de la base de datos de la lógica de negocio superior. 


#### PawnEcommerce (Controladores):

El ensamblaje PawnEcommerce, que alberga los controladores, muestra una abstracción de 0 y una inestabilidad de 1, lo que resulta en una distancia de 0 de la secuencia principal. Este perfil indica un ensamblaje completamente inestable, lo cual es esperado para los controladores que deben responder dinámicamente a las solicitudes externas. La falta de abstracción refleja la naturaleza directa y específica de los controladores dentro de la arquitectura en capas, los cuales no se benefician de la abstracción adicional debido a su rol especializado en el manejo de solicitudes y la entrega de respuestas. a nuestro parecer, no es necesario ni tenía mucho sentido escribir interfaces para los Controllers. 


### Análisis de Complejidad Ciclomática mediante TreeMap Metric View
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/f6eece4c-b597-4673-9ecb-ae5d6d680fa1)

Nota: Los cuadrados más grandes han de ignorarse pues se tratan de nuestras migraciones.

En este caso, Treemap proporciona una instantánea de la base de código, donde el área de los cuadrados indica la cantidad de LOC y el color representa la CC. Este gráfico ilustra una imagen integral de la base de código, destacando áreas de alta densidad de código y complejidad potencial.Como podemos ver, La distribución y tamaño de los cuadrados implican que el sistema ha sido diseñado siguiendo los principios SOLID, con una clara separación de responsabilidades y una abstracción adecuada. Esto se refleja en la presencia de una cantidad importante de áreas pequeñas, junto con una CC generalmente baja, lo que sugiere que se han definido interfaces claras y se ha promovido un acoplamiento flojo. La CC, indicada por el color, es verde para casi todas las clases, lo que sugiere que, en general, logramos que nuestro código se mantenga manejable y que eviten la complejidad excesiva.


### Análisis de valores de Cohesión Relacional

#### Service (4.04):

Este valor indica un posible exceso de cohesión, donde las clases pueden estar demasiado interrelacionadas, potencialmente violando el Principio de Clausura Común, ya que los cambios en una clase podrían tener un efecto dominó. Para abordar esto en un futuro, deberíamos considerar descomponer el paquete en unidades más pequeñas y cohesivas que compartan cierres comunes, lo que facilitaría tanto el mantenimiento como el cumplimiento de los principios de abstracciones y dependencias estables. También cometimos el error de incluir clases que no son del todo utilizadas en este paquete (como los DTOs que se usan para los Controller), violando también el Principio de Reuso Común.


#### Repository (2.23):

El valor para el paquete Repository está dentro del rango óptimo, lo que sugiere que las clases tienen un nivel adecuado de interdependencia y una buena alineación con el Principio de Reuso Común. Esto indica que hay suficientes relaciones para dar al paquete un sentido de unidad sin sobrecargar las clases con interdependencias. Sin embargo, como está más cerca del límite superior del rango ideal, se debe tener cuidado de no introducir más dependencias que podrían aumentar este valor demasiado.


#### PawnEcommerce (1.64):

Este valor está ligeramente por encima del límite inferior de lo que se considera un rango bueno. Aunque está dentro del rango aceptable, sugiere que el paquete PawnEcommerce podría estar al borde de tener clases que no están lo suficientemente interrelacionadas. Sería conveniente revisar este paquete para asegurarse de que todas las clases tengan un propósito claro y justificado dentro del paquete, o considerar si algunas clases deberían moverse a paquetes diferentes para mejorar la cohesión. \


### Conclusión

El análisis meticuloso de las métricas de nuestro sistema Ecommerce revela una realidad multifacética del estado actual de la arquitectura y el código. Aunque en gran medida seguimos los principios de diseño SOLID y hemos logrado un sistema funcional y adaptable, las métricas específicas como la abstracción, la inestabilidad y la cohesión relacional destacan áreas donde la alineación con los principios de diseño podría mejorar.

A pesar de que el paquete Service muestra una cohesión que excede ligeramente el rango ideal, su proximidad a la secuencia principal sugiere que hemos logrado un nivel razonable de abstracciones estables. No obstante, debemos tener en cuenta los riesgos asociados con la interconexión interna, que puede amenazar el Principio de Clausura Común. La inclusión de clases no utilizadas dentro del paquete mismo, como los DTOs, también sugiere que podríamos estar comprometiendo el Principio de Reuso Común. Una acción correctiva sería reevaluar la necesidad y la colocación de cada clase para garantizar que cada una tenga un propósito claro y esté justificada en su lugar dentro del paquete. El paquete Repository, aunque también se encuentra cerca de la secuencia principal, debe ser monitoreado para prevenir una dependencia excesiva que podría afectar su estabilidad. La alineación con el Principio de Dependencias Estables se mantiene; sin embargo, un equilibrio cuidadoso debe ser gestionado para asegurar que cualquier nueva clase o cambio mantenga o mejore esta estabilidad. Los controladores, ubicados en el punto de inestabilidad máxima pero dentro de la zona verde, cumplen adecuadamente su función dentro de la arquitectura en capas, sin necesidad de abstracciones adicionales. Esta posición valida nuestra decisión de no introducir interfaces para los controladores y resalta su adecuación al Principio de Clausura Común, concentrando los cambios y las interacciones de usuario en un área definida y aislada del sistema.

Además, la Complejidad Ciclomática revela que, aunque la mayoría de las clases mantienen una complejidad manejable, hay áreas que podrían beneficiarse de una refactorización para simplificar y mejorar la claridad del código, alineándose con el Principio de Clausura Común al limitar los cambios a un solo paquete.

En resumen, aunque nuestro sistema muestra fortalezas en ciertas áreas, como el bajo acoplamiento general y una buena separación de responsabilidades, la revisión de las métricas nos impulsa a considerar ajustes enfocados en los principios de Clausura Común y Reuso Común para mejorar la cohesión, así como en los principios de Abstracciones Estables y Dependencias Estables para fortalecer la robustez de nuestra arquitectura. La adhesión a estos principios no solo facilitará la futura mantenibilidad y escalabilidad de nuestro sistema, sino que también optimizará la eficiencia del desarrollo y la calidad general del software.




# Vista Física (Physical View):

Representamos mediante el siguiente diagramas de despliegue que utilizamos para representar cómo los componentes de un sistema interactúan físicamente en un entorno de ejecución.
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/d4840b86-bd67-4ff3-b5a4-ba8f1f47f48e)

En el diagrama ejemplificamos la comunicación entre los diferentes componentes. El usuario interactúa con una aplicación web Angular (Cliente HTTP) y envía peticiones al servidor en formato JSON, comunicándose a través de la REST API que este expone. El servidor atiende peticiones que son manejadas por la lógica, que consume datos de un servidor local de bases de datos que usa el motor SQL SERVER.


# Justificación de diseño


## Promociones
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/85bd1c0f-4223-46ff-baf3-44a44ea63925)

 Adoptamos el patrón Strategy para el manejo de promociones, lo cual nos ofrece la flexibilidad de cambiar la implementación del algoritmo de descuento en tiempo de ejecución. En lugar de codificar un solo algoritmo de promoción, diseñamos una familia de algoritmos representados por estrategias concretas que se pueden intercambiar dinámicamente.

La interfaz IPromotionStrategy establece el contrato para nuestras estrategias, con un método GetDiscountPrice que procesa una lista de productos y calcula el total con los descuentos aplicables. PromotionSelector, en colaboración con PromotionCollection, gestiona las instancias de estrategias concretas y determina la promoción óptima según el contexto de la solicitud.

Para facilitar la selección e instanciación de estrategias de promoción específicas sin un acoplamiento rígido, utilizamos Reflection. Esto mejora la adherencia a los principios de diseño avanzado de la siguiente manera:

Clausura Común:

Reflection nos permite agregar nuevas estrategias de promoción o modificar las existentes sin alterar el código cliente que depende de IPromotionStrategy. Esto cumple con el Principio de Clausura Común, agrupando las estrategias que cambian por las mismas razones —un cambio en la política de promoción— y aislándolas del resto del sistema.

Reuso Común:

Las estrategias de promoción implementan IPromotionStrategy y se alojan en PromotionCollection, lo que facilita el reuso de comportamientos promocionales comunes a través de la Reflection. Esto está en línea con el Principio de Reuso Común, ya que estrategias que se utilizan juntas están accesibles y encapsuladas en un solo lugar.

Abstracciones Estables:

IPromotionStrategy sirve como una abstracción estable en nuestro sistema, con implementaciones concretas que evolucionan independientemente. Reflection fortalece este principio al permitir que el sistema sea extensible y que las abstracciones permanezcan estables a pesar de los cambios en las implementaciones concretas.

Dependencias Estables:

Al usar Reflection para instanciar estrategias de promoción, evitamos dependencias directas en implementaciones específicas, lo cual refuerza el Principio de Dependencias Estables. PromotionSelector y las clases que consumen promociones dependen de la interfaz abstracta en lugar de detalles concretos, asegurando la estabilidad de las dependencias. \
 \
En el Anexo, se encuentran las instrucciones para manejar las promociones dinámicamente.


## Filtros
![image](https://github.com/MateoGiraz/pawn-ecommerce/assets/100039777/a1ed1b68-998e-45b6-b282-b32b9d9cbcc4)

Para la implementación de los filtros, empleamos un patrón Template Method. La clase abstracta FilterTemplate tiene un método que recibe una lista de productos y una condición de filtrado, ya sea de identificador o de nombre, y hace una selección de los productos que cumplen el criterio indicado. Para ello, delega la decisión del filtrado de cada producto a la subclase, que es la implementación concreta del filtro.

Los beneficios de aplicar el patrón Template Method en este contexto incluyen:

**Reusabilidad**

La clase FilterTemplate centraliza la lógica de aplicación común, reduciendo la duplicación de código y facilitando la reutilización en múltiples contextos de filtrado. Esto es un reflejo del Principio de Reuso Común, ya que se crea una base que es comúnmente utilizada por todas las subclases sin necesidad de replicar la lógica.

**Mantenibilidad**

Los filtros concretos, como PriceRangeFilter, IdsFilter, y NameFilter, sobrescriben solo el método Match. Esto localiza las responsabilidades de filtrado específicas y minimiza el impacto de los cambios en el algoritmo de aplicación general. Esta especialización permite que las modificaciones sean aisladas y manejables, mejorando la mantenibilidad y alineándose con el Principio de Clausura Común, donde los cambios debido a una sola razón de cambio afectan a un módulo específico.

**Extensibilidad**

Si en el futuro es necesario agregar más funcionalidades o características a los filtros, hacerlo en la clase base asegurará que todos los filtros la implementen.

**Desacoplamiento**

La lógica específica de filtrado se encapsula dentro de cada filtro concreto, mientras que la lógica general de cómo se aplican y procesan los filtros reside en la clase template. Esto significa que si un filtro particular necesita ser modificado, no afectará a los otros filtros ni a la lógica general de filtrado. Esto favorece el principio SRP, porque cada filtro se encarga solamente de implementar su lógica.




# Anexo


## Reflection

Utilizando Reflection, hemos mejorado la flexibilidad y el mantenimiento de las promociones, permitiendo que se manejen de manera dinámica.El uso de Reflection para instanciar dinámicamente estrategias de promoción apoya el Principio de Reuso Común y Abstracciones Estables, facilitando la incorporación de nuevas promociones y la adaptación a requisitos cambiantes con un acoplamiento mínimo.

## Instrucciones para el manejo de Promociones

Si se desea crear una nueva promoción, deberá primero ser implementada correctamente para luego ser compilada en una biblioteca de clases aparte a Service, preferiblemente con el nombre de la promoción, creando de esta forma el archivo .dll.  Una vez compilada debe ser movida a la carpeta _aplicacion/back/promotions_, donde deben ser alojadas todas las promociones.



## Credenciales de usuario administrador


```
Email admin@gmail.com
Contraseña password
```

