//Quiero programar una función que ataque a un endpoint de una API y me devuelva un JSON con los datos de la API
//La función debe recibir como parámetro el endpoint de la API
//La función debe devolver un JSON con los datos de la API
//La función debe devolver un error si no se puede conectar con la API
let url = "https://api.github.com/users/ironhack-labs";
const func = async (url: string) => {
    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
    //creamos un objeto con los datos que queremos
    let obj = { name: data.name, location: data.location, bio: data.bio };
    console.log(obj);
}

func(url);