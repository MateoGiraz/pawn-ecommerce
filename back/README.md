# Criterios REST                                               

Para el cumplimiento de los criterios REST, seguimos una serie de estándares que detallamos a continuación.                                                                                          

Se utilizan los verbos HTTP de manera adecuada: GET, POST, PUT, DELETE para operaciones de lectura, escritura, actualización y eliminación respectivamente. Asimismo desarrollamos URIs estructuradas de manera significativa, representando los recursos de forma clara y comprensible, facilitando el acceso a los recursos por su nombre. Nos apegamos al estándar empleando los nombres en singular. En suma, todo esto favorece que nuestra API cuente con una interfaz uniforme.

Cada solicitud se desarrolla contemplando que debe contener toda la información necesaria para su procesamiento, sin depender de ningún estado almacenado en el servidor. De esta forma cumplimos con su propiedad de no tener estado.

Las operaciones en la API retornan códigos de estado HTTP apropiados en sus respuestas, como lo son 200 (Success), 400 (Bad Request), 401 (Unauthorized), 404 (Not Found), 500 (Internal Server Error). De esta forma se estandariza el resultado de las operaciones. Las respuestas contienen mensajes autodescriptivos que son claros y permiten al cliente tener una idea clara de cuál es el error. Acompasando esto, se devuelven en formato JSON, lo cual es un estándar comúnmente aceptado.

Existe una jerarquía entre los recursos que expone la API, y es posible navegar entre ellos. Aquellos recursos que guardan una relación lógica también son mostrados con esta relación explícita permitiendo al cliente su vinculación.

Usamos Objetos de transferencia de datos (DTO) para el envío de información hacia el cliente, de forma que no se exponga la totalidad de propiedades de los modelos, sino aquellas propiedades que se consideran relevantes.

La implementación de todas estas características permiten que nuestra API cumpla con los criterios REST.


# 


# Mecanismo de Autenticación

Nuestra API implementa mecanismos sólidos de autenticación y autorización con el fin de permitir el acceso a los recursos protegidos solo a aquellos usuarios con los permisos adecuados.

Para la autenticación, empleamos un sistema basado en tokens JWT (JSON Web Tokens). Cuando un usuario se autentica con éxito mediante la ruta de login, la API genera un token JWT único para ese usuario. Este token, que lleva información codificada del usuario, debe ser incluido en el encabezado de las solicitudes siguientes para verificar la identidad del usuario. Específicamente, se debe enviar en el encabezado Authorization con el formato "Bearer {token}". Al recibir las solicitudes que involucran autenticación, empleamos un Middleware de autenticación y autorización que validan tanto que el token exista y sea válido, como también que el usuario cuente con los permisos requeridos.

Los usuarios que son creados mediante registros a través del endpoint de creación de usuarios tienen asignado el rol “User” por defecto. El rol "Admin" es especial y sólo puede ser asignado por el sistema. Esto limita el acceso a las funcionalidades administrativas.

**Rutas públicas**


```js
GET api/color
GET api/brand
GET api/category
GET api/products
POST api/session/login
POST api/sale/discount
POST api/user
```


**Rutas de usuarios**


```js
POST api/sale
```


**Rutas de administrador**


```js
POST, PUT, DELETE api/product
PUT, GET, DELETE api/user
GET api/sale
```



# Códigos de respuesta

Estos son los posibles códigos de respuesta manejados en la API 


## Código 200 - Éxito (Success)

Este es el código de respuesta de estado satisfactorio que indica que la solicitud fue exitosa.


## Código 400 - (Bad Request)

Este es el código de error que indica que el servidor no puede procesar la petición a causa de un error del cliente. Se corresponde con excepciones de modelo o servicio.


## Código 401 - (Unauthorized)

Este código de error indica que la petición no se ejecutó porque carece de credenciales de autenticación para el recurso.


##  Código 404 - (Not Found)

Este código de error implica que el servidor no procesó la petición porque no encontró el recurso esperado. Se corresponde con excepciones de repositorio.


##  Código 500 - (Internal Server Error)

Este código de error indica que algo falló pero desconocemos la causa. El error ocurre del lado del servidor. Se envía esta respuesta de estado cuando la excepción lanzada no se corresponde con excepciones de modelo, servicio, repositorio o credenciales.


# Resources


## **Brand**


```js
BaseURL: /api/brand
```

```js
GET /api/brand
```


Devuelve todas las marcas registradas en el sistema.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de las siguientes propiedades:



* `id:` El identificador de la marca.
* `name:` El nombre de la marca.

```js
500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/brand
```

Response:


```js
[
  {
    "id": 3,
    "name": "Nike"
  },
  {
    "id": 4,
    "name": "Apple"
  },
  {
    "id": 5,
    "name": "Coca-Cola"
  },
  {
    "id": 6,
    "name": "Samsung"
  }
]
```

```js
GET /api/brand/{id}
```

Devuelve la marca con el identificador especificado.

**Parameters**

No recibe parámetros.

**Response**

```js
 200 - Éxito (Success)
```

Devuelve un JSON con las siguientes propiedades:

* `id:` El identificador de la marca.
* `name:` El nombre de la marca.

```js
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Example**

Request:

```js
GET /api/brand/4
```

Response:

```js
{
  "id": 4,
  "name": "Apple"
}
```

## **Category**

```js
BaseURL: /api/category
```

```js
GET /api/category
```
Devuelve todas las categorías registradas en el sistema.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```

Devuelve un JSON con una colección de las siguientes propiedades:

* `id:` El identificador de la categoría.
* `name:` El nombre de la categoría.

```js
 500 - (Internal Server Error)
```

**Example**

Request:

```js
GET /api/category
```

Response:

```js
[
  {
    "id": 3,
    "name": "Electronics"
  },
  {
    "id": 4,
    "name": "Apparel"
  },
  {
    "id": 5,
    "name": "Automotive"
  },
  {
    "id": 6,
    "name": "Food & Beverage"
  },
  {
    "id": 7,
    "name": "Health & Beauty"
  }
]
```

```js
GET /api/category/{id}
```

Devuelve la categoría con el identificador especificado.

**Parameters**

No recibe parámetros.

**Response**

```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades:

* `id:` El identificador de la categoría.
* `name:` El nombre de la categoría.

```js
 500 - (Internal Server Error)
```


**Example**

Request:

```js
GET /api/category/7
```

Response:

```js
{
  "id": 7,
  "name": "Health & Beauty"
}
```

## **Color**


```js
BaseURL: /api/color
```

```js
GET /api/color
```


Devuelve todos los colores registrados en el sistema.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de las siguientes propiedades:



* `id:` El identificador del color.
* `name:` El nombre del color.
* `code:` El codigo de color hexadecimal.

```js
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/color
```


Response:


```js
[
  {
    "id": 2,
    "name": "RED",
    "code": "#FF0000"
  },
  {
    "id": 3,
    "name": "BLUE",
    "code": "#0000FF"
  },
  {
    "id": 4,
    "name": "GREEN",
    "code": "#008000"
  },
  {
    "id": 5,
    "name": "YELLOW",
    "code": "#FFFF00"
  }
]
```





```js
GET /api/color/{id}
```


Devuelve el color con el identificador especificado.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades



* `id:` El identificador del color.
* `name:` El nombre del color
* `code:` El codigo de color hexadecimal.

```js
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/color/3
```


Response:


```js
{
  "id": 3,
  "name": "BLUE",
  "code": "#0000FF"
}
```



## **User**


```js
BaseURL: /api/user
```


**Header**


```js
Authorization: Bearer {token}
```



```js
GET /api/user
```


Devuelve todos los usuarios registrados en el sistema.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de las siguientes propiedades:



* `id:` El identificador del usuario.
* `name:` El nombre del usuario.
* `address:` La dirección del usuario.

```js
 401 - (Unauthorized)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/user
```


Response:


```js
[
  {
    "id": 2,
    "name": "Pablo",
    "address": "Suite 703 20040 Roberts Green"
  },
  {
    "id": 4,
    "name": "MateGz",
    "address": "69444 Fritz Union, North Corenebury"
  },
  {
    "id": 5,
    "name": "Paxo",
    "address": "Suite 734 5948 Forrest Oval"
  }
]
```





```js
POST /api/user
```


Crea un usuario

**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 400 - (Bad Request)
 500 - (Internal Server Error)
```


**Body**


```js
{
  "email" (required) : "string",
  "pass word" (required) : "string",
  "address" (required) : "string"
}
```


**Example**

Request:


```js
POST /api/user

{
  "email": "pacho@gmail.com",
  "password": "secret",
  "address": "2539 Meadowview Drive"
}
```


Response: 


```js
200 - Success
```





```js
PUT /api/user/{id}
```


Actualiza un usuario

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**



* <code>id<strong> </strong></code>(requerido): El identificador del usuario.

<strong>Response</strong>


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 400 - (Bad Request)
 401 - (Unauthorized)
 404 - (Not Found)
 500 - (Internal Server Error)
```


**Body**


```js
{
  "email" (required) : "string",
  "password" (required) : "string",
  "address" (required) : "string"
}
```


**Example**

Request:


```js
PUT /api/user/5

{
  "email": "pacho@gmail.com",
  "password": "newSecret",
  "address": "2539 Meadowview Drive"
}
```


Response: 


```js
200 - Success
```



```js
DELETE /api/user/{id}
```


Elimina un usuario

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 404 - (Not Found)
 401 - (Unauthorized)
 500 - (Internal Server Error)
```


**Example**

Request:


```js
DELETE /api/user/5
```


Response: 


```js
200 - Success
```

```js
GET /api/user/History
```


Devuelve el perfil del usuario actualmente logueado en el sistema.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de todas las compras realizadas por el usuario logueado, cada una de ellas con las siguientes propiedades:



* `id:` El identificador de la compra.
* `date:` La fecha de la compra.
* `promotionName:` El nombre de la promoción si hubiera alguna aplicada.
* `price:` El precio de la compra.
* `paymentMethod:` El método de pago de la compra.
* `products:`  Los productos de la venta, una colección de las siguientes propiedades:
    * `productId:` El identificador del producto.
    * `product:` El producto, con las siguientes propiedades:
        * `id:` El identificador del producto.
        * `name:` El nombre del producto.
        * `description:` La descripción del producto.
        * `price:` El precio del producto.
        * `categoryId:`  El identificador de la categoría del producto.
        * `brandId:`  El identificador de la marca del producto.
        * `colors:`  Los colores, una colección de las siguientes propiedades:
            * `id:` El identificador del color.
            * `name:` El nombre del color.
            * `code:` El codigo de color hexadecimal.

```js
 401 - (Unauthorized)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/user/History
```


Response:


```js
 [
    {
        "id": 14,
        "products": [
            {
                "productId": 2,
                "product": {
                    "id": 2,
                    "name": "Pantalón Chino Profesional",
                    "description": "Pantalón chino de Zara en algodón de alta calidad, perfecto para cualquier ocasión. Disponible en verde y amarillo.",
                    "price": 2500,
                    "stock": 86,
                    "brandId": 1,
                    "brand": null,
                    "categoryId": 2,
                    "category": null,
                    "isExcludedFromPromotions": false,
                    "colors": []
                }
            }
        ],
        "userId": 6,
        "user": null,
        "price": 6260,
        "promotionName": "20% OFF",
        "date": "2023-11-16T01:33:46.7363043",
        "paymentMethod": "Mastercard"
    }
]
```

## **Session**


```js
BaseURL: /api/session
```



```js
POST /api/session/login
```


Valida las credenciales de un usuario y genera un token con el que puede identificarse.

**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades:



* `token:` El token que identifica al usuario y puede usar para crear requests.

```js
 400 - (Bad Request)
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Body**


```js
{
  "email" (required) : "string",
  "password" (required) : "string",
}
```


**Example**

Request:


```js
POST /api/session/login

{
  "email": "pacho@gmail.com",
  "password": "secret",
}
```


Response: 


```js
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBhY2hvQGdtYWlsLmNvbSIsIm5iZiI6MTY5NjQ0MTI3OSwiZXhwIjoxNjk2NTI3Njc5LCJpYXQiOjE2OTY0NDEyNzl9.mOxmrec2CyZDG94XJm28flibEma75jly5jnBTy1TOyU"
}
```





## **Product**


```js
BaseURL: /api/product
```



```js
GET /api/product
```


Devuelve todos los productos registrados en el sistema.

**Parameters**



* <code>name<strong> </strong></code>(opcional): El nombre del producto.
* <code>categoryId<strong> </strong></code>(opcional): El identificador de la categoría del producto.
* <code>brandId<strong> </strong></code>(opcional):  El identificador de la marca del producto.

<strong>Response</strong>


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de las siguientes propiedades:



* `id:` El identificador del producto.
* `name:` El nombre del producto.
* `description:` La descripción del producto.
* `price:` El precio del producto.
* `categoryId:`  El identificador de la categoría del producto.
* `category:`  La categoría del producto, con las siguientes propiedades:
    * `id:` El identificador de la categoría.
    * `name:` El nombre de la categoría.
* `brandId:`  El identificador de la marca del producto.
* `brand:`  La marca del producto, con las siguientes propiedades:
    * `id:` El identificador de la marca.
    * `name:` El nombre de la marca.
* `colors:`  Los colores del producto, una colección de las siguientes propiedades:
    * `id:` El identificador del color.
    * `name:` El nombre del color.
    * `code:` El codigo de color hexadecimal.

```js
 400 - (Bad Request)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET api/Product?name=phone&categoryId=3&brandId=4
```


Response:


```js
[
  {
    "id": 8,
    "name": "iPhone X",
    "description": "Apple iPhone X 2017",
    "price": 1200,
    "brandId": 4,
    "brand": {
      "id": 4,
      "name": "Apple"
    },
    "categoryId": 3,
    "category": {
      "id": 3,
      "name": "Electronics"
    },
    "colors": [
      {
        "id": 2,
        "name": "RED",
        "code": "#FF0000"
      },
      {
        "id": 3,
        "name": "BLUE",
        "code": "#0000FF"
      }
    ]
  }
]
```





```js
GET /api/product/{id}
```


Devuelve el producto con el identificador especificado.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades:



* `id:` El identificador del producto.
* `name:` El nombre del producto.
* `description:` La descripción del producto.
* `price:` El precio del producto.
* `categoryId:`  El identificador de la categoría del producto.
* `category:`  La categoría del producto, con las siguientes propiedades:
    * `id:` El identificador de la categoría.
    * `name:` El nombre de la categoría.
* `brandId:`  El identificador de la marca del producto.
* `brand:`  La marca del producto, con las siguientes propiedades:
    * `id:` El identificador de la marca.
    * `name:` El nombre de la marca.
* `colors:`  Los colores del producto, una colección de las siguientes propiedades:
    * `id:` El identificador del color.
    * `name:` El nombre del color.
    * `code:` El codigo de color hexadecimal.

```js
 400 - (Bad Request)
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/product/9
```


Response:


```js
{
  "id": 9,
  "name": "Galaxy S23",
  "description": "Samsung Galaxy S23 2023",
  "price": 1200,
  "brandId": 6,
  "brand": {
    "id": 6,
    "name": "Samsung"
  },
  "categoryId": 3,
  "category": {
    "id": 3,
    "name": "Electronics"
  },
  "colors": [
    {
      "id": 2,
      "name": "RED",
      "code": "#FF0000"
    }
  ]
}
```





```js
POST /api/product
```


Crea un producto.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 400 - (Bad Request)
 401 - (Unauthorized)
 500 - (Internal Server Error)
```


**Body**


```js
{
  "name" (required) : "string",
  "description" (required) : "string",
  "price" (required) : 0,
  "brandId" (required) : 0,
  "categoryId" (required) : 0,
  "colors" (required) : [
    0
  ]
}
```


**Example**

Request:


```js
POST /api/product

{
  "name": "P60 Pro",
  "description": "HUAWEI P60 Pro 2023",
  "price": 890,
  "brandId": 7,
  "categoryId": 3,
  "colors": [
    4
  ]
}
```


Response: 


```
200 - Success
```





```js
PUT /api/product/{id}
```


Actualiza un producto.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 400 - (Bad Request)
 401 - (Unauthorized)
 404 - (Not Found)
 500 - (Internal Server Error)
```


**Body**


```js
{
  "name" (required) : "string",
  "description" (required) : "string",
  "price" (required) : 0,
  "brandId" (required) : 0,
  "categoryId" (required) : 0,
  "colors" (required) : [
    0
  ]
}
```


**Example**

Request:


```js
PUT /api/product/9

{
  "name": "P30 lite",
  "description": "Huawei P30 lite 2017",
  "price": 600,
  "brandId": 4,
  "categoryId": 3,
  "colors": [
    3
  ]
}
```


Response: 


```js
200 - Success
```





```js
DELETE /api/product/{id}
```


Elimina un producto.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 401 - (Unauthorized)
 404 - (Not Found)
 500 - (Internal Server Error)
```


**Example**

Request:


```js
DELETE /api/product/5
```


Response: 


```js
200 - Success
```





## **Sale**


```js
BaseURL: /api/sale
```



```js
GET /api/sale
```


Devuelve todas las compras registradas en el sistema.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con una colección de las siguientes propiedades:



* `id:` El identificador de la compra.
* `date:` La fecha de la compra.
* `promotionName:` El nombre de la promoción si hubiera alguna aplicada.
* `price:` El precio del producto.
* `products:`  Los productos de la venta, una colección de las siguientes propiedades:
    * `productId:` El identificador del producto.
    * `product:` El producto, con las siguientes propiedades:
        * `id:` El identificador del producto.
        * `name:` El nombre del producto.
        * `description:` La descripción del producto.
        * `price:` El precio del producto.
        * `categoryId:`  El identificador de la categoría del producto.
        * `category:`  La categoría, con las siguientes propiedades:
            * `id:` El identificador de la categoría.
            * `name:` El nombre de la categoría.
        * `brandId:`  El identificador de la marca del producto.
        * `brand:`  La marca del producto, con las siguientes propiedades:
            * `id:` El identificador de la marca.
            * `name:` El nombre de la marca.
        * `colors:`  Los colores, una colección de las siguientes propiedades:
            * `id:` El identificador del color.
            * `name:` El nombre del color.
            * `code:` El codigo de color hexadecimal.

```js
 401 - (Unauthorized)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET api/Sale
```


Response:


```js
[
  {
    "id": 18,
    "products": [
      {
        "productId": 2,
        "product": {
          "id": 2,
          "name": "iPhone X",
          "description": "great prod",
          "price": 100,
          "brandId": 4,
          "brand": {
            "id": 4,
            "name": "Apple"
          },
          "categoryId": 3,
          "category": {
            "id": 3,
            "name": "Electronics"
          },
          "colors": [
            {
              "id": 2,
              "name": "RED",
              "code": "#FF0000"
            }
          ]
        }
      },
      {
        "productId": 2,
        "product": {
          "id": 2,
          "name": "iPhone X",
          "description": "great prod",
          "price": 100,
          "brandId": 4,
          "brand": {
            "id": 4,
            "name": "Apple"
          },
          "categoryId": 3,
          "category": {
            "id": 3,
            "name": "Electronics"
          },
          "colors": [
            {
              "id": 2,
              "name": "RED",
              "code": "#FF0000"
            }
          ]
        }
      },
      {
        "productId": 2,
        "product": {
          "id": 2,
          "name": "iPhone X",
          "description": "Apple iPhone X 2017",
          "price": 100,
          "brandId": 4,
          "brand": {
            "id": 4,
            "name": "Apple"
          },
          "categoryId": 3,
          "category": {
            "id": 3,
            "name": "Electronics"
          },
          "colors": [
            {
              "id": 2,
              "name": "RED",
              "code": "#FF0000"
            }
          ]
        }
      }
    ],
    "price": 100,
    "promotionName": "Thre For One",
    "date": "2023-10-04T02:10:52.3703482"
  },
 {
    "id": 20,
    "products": [
      {
        "productId": 5,
        "product": {
          "id": 5,
          "name": "iPhone 15",
          "description": "Apple iPhone 15 2023",
          "price": 1000,
          "brandId": 4,
          "brand": {
            "id": 4,
            "name": "Apple"
          },
          "categoryId": 6,
          "category": {
            "id": 6,
            "name": "Electronics"
          },
          "colors": [
            {
              "id": 4,
              "name": "GREEN",
              "code": "#008000"
            },
            {
              "id": 5,
              "name": "YELLOW",
              "code": "#FFFF00"
            }
          ]
        }
      }
    ],
    "price": 1000,
    "promotionName": null,
    "date": "2023-10-04T16:14:23.000151"
  }
]
```

```js
GET /api/sale/{id}
```


Devuelve la compra con el identificador especificado.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades:



* `id:` El identificador de la compra.
* `date:` La fecha de la compra.
* `promotionName:` El nombre de la promoción si hubiera alguna aplicada.
* `price:` El precio del producto.
* `products:`  Los productos de la venta, una colección de las siguientes propiedades:
    * `productId:` El identificador del producto.
    * `product:` El producto, con las siguientes propiedades:
        * `id:` El identificador del producto.
        * `name:` El nombre del producto.
        * `description:` La descripción del producto.
        * `price:` El precio del producto.
        * `categoryId:`  El identificador de la categoría del producto.
        * `brandId:`  El identificador de la marca del producto.
        * `colors:`  Los colores, una colección de las siguientes propiedades:
            * `id:` El identificador del color.
            * `name:` El nombre del color.
            * `code:` El codigo de color hexadecimal.

```js
 401 - (Unauthorized)
 404 - (Not Found)
 500 - (Internal Server Error)
```



**Example**

Request:


```js
GET /api/sale/20
```


Response:


```js
{
  "id": 20,
  "products": [
    {
      "productId": 5,
      "product": {
        "id": 5,
        "name": "iPhone 15",
        "description": "Apple iPhone 15 2023",
        "price": 1000,
        "brandId": 4,
        "brand": {
          "id": 4,
          "name": "Apple"
        },
        "categoryId": 6,
        "category": {
          "id": 3,
          "name": "Electronics"
        },
        "colors": [
          {
            "id": 4,
            "name": "GREEN",
            "code": "#008000"
          },
          {
            "id": 5,
            "name": "YELLOW",
            "code": "#FFFF00"
          }
        ]
      }
    }
  ],
  "price": 1000,
  "promotionName": null,
  "date": "2023-10-04T16:14:23.000151"
}
```



```js
POST /api/sale
```


Crea una compra.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**


<table>
  <tr>
  </tr>
</table>



```js
 200 - Éxito (Success)
 400 - (Bad Request)
 401 - (Unauthorized)
 500 - (Internal Server Error)
```


**Body**


```js
{
  "productIds": [
    0
  ]
}
```


**Example**

Request:


```js
POST /api/sale

{
  "productIds": [
    4,5,6
  ]
}
```


Response: 


```js
200 - Success
```





```js
POST /api/sale/discount
```


Recibe una colección de productos y devuelve el precio aplicando una promoción si aplica.

**Parameters**

No recibe parámetros.

**Response**


```js
 200 - Éxito (Success)
```


Devuelve un JSON con las siguientes propiedades:



* `price:` El precio de la compra potencialmente actualizado

<table>
  <tr>
  </tr>
</table>



```js
 400 - (Bad Request)
 500 - (Internal Server Error)
```



**Body**


```js
[
  0
]
```


**Example**

Request:


```js
POST /api/sale
[
  4,5,6
]
```


Response: 


```js
{
    "price": 810
}
```


```js
POST /api/sale
```


Crea una compra.

**Header**


```js
Authorization: Bearer {token}
```


**Parameters**

No recibe parámetros.

**Response**

```js
200 - Éxito (Success)
400 - (Bad Request)
401 - (Unauthorized)
500 - (Internal Server Error)
```


**Body**


```js
{
  "productIds": [
    0
  ]
}
```


**Example**

Request:


```js
POST /api/sale

{
  "productIds": [
    4,5,6
  ]
}
```

Response: 

```js
200 - Success
```
