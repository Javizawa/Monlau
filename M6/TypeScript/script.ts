//Variable con dos posibles tipos
let var1: (number | string) = 3;

var1 = 'hola';
var1 = 3;



interface datos{
    direccion: string,
    cp: string,
    tlf: string
}

//Una Interface con un campo opcional y otro de tipo función también opcional
export interface trabajador {
    nombre: string,
    apellido1: string,
    apellido2?: string,
    fNacimiento: Date,
    aficiones: string[],
    mostrarAficion?: () => void,
    datosPersonales?: datos
}




//Crea un objeto a través de una Interfaz
const traba1: trabajador = {
    nombre : 'Javier',
    apellido1 : 'Salvador',
 //   apellido2 : '',
    fNacimiento : new Date('20-03-1981') ,
    aficiones : ['pescar', 'nadar', 'correr'],
    mostrarAficion: ()=> {
        console.log("El trabajador " + traba1.nombre + " es aficionado a la " + traba1.aficiones[0])
    },
    datosPersonales:{
        direccion: "Calle 1",
        cp: "08017",
        tlf: "7878676767"
    }
}



console.log(traba1.datosPersonales?.tlf);






















//Podemos extraer un atributo del nombre a través del objeto.atributo
console.log (traba1.nombre);

//También podemos desestructurar objetos y crear una variable a parte
const {nombre} = traba1;
console.log(nombre);

//También podemos desestructurar objetos y crear una variable a parte 
//con otro nombre diferente al atributo
const {nombre:nombre1} = traba1;
console.log(nombre1);



//Función con objetos
function changeName(persona:trabajador, nombre:string){
    persona.nombre = nombre;
    persona.mostrarAficion;
}

//Podemos mandar menos parámetros de los que pide la función
//Primero obligatorios, después opcionales y finalmente por valor
//Si b no se pasa como parámetro, se recupera a+b
function fSuma(a:number,b?:number, c:number=3):number{
if(typeof b !="undefined"){
    return a+b+c;
    }else{
        return a+c;
    }
}

let a = fSuma(3,3);

//Funcion flecha
const fSumaFlecha = (a,b) =>{
    return a+b;
}










console.log(fSumaFlecha(3,3))

console.log(a);