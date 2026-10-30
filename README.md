# Interfaces inteligentes - Práctica 5 - Cardboard e interacción con la mirada

Autor: Javier Acosta Portocarrero

En esta práctica he trabajado con Cardboard en Unity para crear una escena que pueda utilizarse en Android e interactuar con objetos mediante la mirada. También he preparado un entorno natural con recursos de la Asset Store, incorporando vegetación, animales, elementos decorativos y sonidos, y he utilizado Raycast y eventos para implementar la recolección de objetos.

## Ejercicio 1

En este primer ejercicio creé el proyecto de Unity para utilizar Cardboard y realicé pruebas con su funcionamiento. Configuré el proyecto para Android y probé la generación de una APK, con el objetivo de ejecutar la escena desde el móvil y experimentar con la visualización mediante Cardboard.

![Ejercicio 1](./gifs-readme/ejercicio1.gif)

## Ejercicio 2

En este ejercicio preparé la escena en la que se desplaza el jugador. Creé un **Terrain** y modifiqué su relieve para formar montañas y colinas alrededor de la zona de juego. Utilicé la herramienta **Paint Trees** del Terrain para distribuir los árboles y **Paint Details** para añadir la hierba. Para ambientar el bosque incorporé el paquete **Lowpoly Environments de Polytope Studio**, un molino y elementos del asset **Little Ghosts**. También utilicé otro paquete de animales, del que añadí un **ciervo y una gallina**, y un asset para el **cielo**. Por último, incorporé sonidos para mejorar la ambientación de la escena.

Implementé el desplazamiento del jugador para que pueda avanzar **hacia la dirección en la que mira**, o moverse utilizando un **mando**. Además, añadí objetos coleccionables que el jugador recoge al mirarlos desde una distancia suficientemente corta. Para detectar qué objeto está mirando utilicé un `Physics.Raycast` desde la cámara y Colliders en los objetos interactivos, tomando como referencia el funcionamiento de `OnPointerEnter()` del script `ObjectController` proporcionado como base.

![Ejercicio 2](./gifs-readme/ejercicio2.gif)

## Ejercicio 3

En este último ejercicio elegí un objeto de la escena para que actuara como activador de los coleccionables. Cuando el jugador lo selecciona con la mirada, se ejecuta `OnPointerEnter()` y se lanza un evento mediante un delegado.

Los coleccionables están suscritos a ese evento y, al recibirlo, comienzan a desplazarse hacia el jugador hasta llegar a una distancia de parada. De esta manera separé el objeto que notifica la acción de los que reaccionan a ella, utilizando el patrón observador, como en la práctica anterior. Para esta interacción eliminé del `ObjectController` original las funciones que no necesitaba, como el teletransporte y los cambios de material.

![Ejercicio 3](./gifs-readme/ejercicio3.gif)
