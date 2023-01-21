import {trabajador} from '../script'

const traba2: trabajador = {
    nombre : 'Javier',
    apellido1 : 'Salvador',
 //   apellido2 : '',
    fNacimiento : new Date('20-03-1981') ,
    aficiones : ['pescar', 'nadar', 'correr'],
    mostrarAficion: ()=> {
        console.log("El trabajador " + traba2.nombre + " es aficionado a la " + traba2.aficiones[0])
    },
    datosPersonales:{
        direccion: "Calle 1",
        cp: "08017",
        tlf: "7878676767"
    }
}