"use strict";
var _a;
exports.__esModule = true;
//Variable con dos posibles tipos
var var1 = 3;
var1 = 'hola';
var1 = 3;
//Crea un objeto a través de una Interfaz
var traba1 = {
    nombre: 'Javier',
    apellido1: 'Salvador',
    //   apellido2 : '',
    fNacimiento: new Date('20-03-1981'),
    aficiones: ['pescar', 'nadar', 'correr'],
    mostrarAficion: function () {
        console.log("El trabajador " + traba1.nombre + " es aficionado a la " + traba1.aficiones[0]);
    },
    datosPersonales: {
        direccion: "Calle 1",
        cp: "08017",
        tlf: "7878676767"
    }
};
console.log((_a = traba1.datosPersonales) === null || _a === void 0 ? void 0 : _a.tlf);
//Podemos extraer un atributo del nombre a través del objeto.atributo
console.log(traba1.nombre);
//También podemos desestructurar objetos y crear una variable a parte
var nombre = traba1.nombre;
console.log(nombre);
//También podemos desestructurar objetos y crear una variable a parte 
//con otro nombre diferente al atributo
var nombre1 = traba1.nombre;
console.log(nombre1);
//Función con objetos
function changeName(persona, nombre) {
    persona.nombre = nombre;
    persona.mostrarAficion;
}
//Podemos mandar menos parámetros de los que pide la función
//Primero obligatorios, después opcionales y finalmente por valor
//Si b no se pasa como parámetro, se recupera a+b
function fSuma(a, b, c) {
    if (c === void 0) { c = 3; }
    if (typeof b != "undefined") {
        return a + b + c;
    }
    else {
        return a + c;
    }
}
var a = fSuma(3, 3);
//Funcion flecha
var fSumaFlecha = function (a, b) {
    return a + b;
};
console.log(fSumaFlecha(3, 3));
console.log(a);
