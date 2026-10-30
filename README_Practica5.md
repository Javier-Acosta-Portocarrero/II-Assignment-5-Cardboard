# Interfaces inteligentes - Práctica 5 - UI y Raycast

Autor: Javier Acosta Portocarrero

En esta práctica he trabajado con el control del jugador mediante la mirada y con la detección de objetos utilizando `Physics.Raycast`. También he utilizado delegados y eventos para que varios coleccionables reaccionen al mirar un objeto activador, sin necesidad de utilizar XR.

## Ejercicio 1

En este primer ejercicio implementé un sistema de movimiento en primera persona que permite al jugador avanzar hacia la dirección en la que está mirando. Para ello, utilicé el script `FirstExercisePlayerMovement`, que obtiene la dirección de la cámara y elimina la componente vertical para que el movimiento sea horizontal, aunque el jugador mire hacia arriba o hacia abajo.

Añadí un objeto `Player` con un componente `CharacterController` y coloqué la `Main Camera` como hija, a la altura de los ojos. Utilicé `CharacterController.Move` para desplazar al jugador y apliqué gravedad para mantenerlo sobre el terreno, que tiene un Collider. La rotación horizontal se aplica al jugador y la vertical a la cámara, limitando esta última para evitar giros excesivos.

También configuré distintos métodos de entrada para poder probar el mismo movimiento en varios dispositivos. En PC se avanza manteniendo pulsada la barra espaciadora y se gira la cámara con el botón derecho del ratón. Con un mando se utiliza el botón A o ✕ para avanzar. En móvil, mantener pulsada la mitad izquierda de la pantalla permite avanzar, mientras que deslizar el dedo por la mitad derecha gira la cámara. El control del mando utiliza el Input System nuevo.

![Ejercicio 1](./gifs-readme/ejercicio1.gif)

## Ejercicio 2

En este ejercicio implementé una interacción por mirada basada en el script `ObjectController` proporcionado como ejemplo. Eliminé las funciones que no necesitaba, como el teletransporte aleatorio y el cambio de materiales, y conservé la idea de utilizar `OnPointerEnter()` para ejecutar una acción cuando se empieza a mirar un objeto.

Para detectar la mirada utilicé el script `GazeRaycaster`, colocado en la `Main Camera`. Este lanza un `Physics.Raycast` hacia delante en cada actualización y comprueba con qué objeto colisiona. Cuando el jugador empieza a mirar un objeto, llama a `OnPointerEnter()`, y cuando deja de mirarlo, a `OnPointerExit()`. Tanto el objeto activador como los coleccionables necesitan un Collider para que el rayo pueda detectarlos. No fue necesario utilizar XR ni un `EventSystem` para esta detección de objetos 3D.

El objeto activador utiliza `ObjectController`, que lanza un evento la primera vez que el jugador lo mira. Por otro lado, cada coleccionable tiene el script `CollectibleController`, que se suscribe al evento para comenzar a acercarse al jugador mediante `Vector3.MoveTowards`. Los coleccionables se desplazan horizontalmente y se detienen al alcanzar una distancia determinada.

Finalmente, implementé que los coleccionables desaparezcan con `Destroy(gameObject)` cuando el jugador los mira desde una distancia suficientemente corta. Para comprobarlo, guardé si el objeto está siendo observado y calculé la distancia hasta la cámara con `Vector3.Distance`, de forma que también pueda recogerse si se acerca mientras lo seguimos mirando. Los coleccionables dejan de estar suscritos al evento al desactivarse mediante `OnDisable()`.

![Ejercicio 2](./gifs-readme/ejercicio2.gif)
