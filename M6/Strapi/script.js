/*

function getUser(callback, idUser) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = (e) => {
      if (xhr.readyState !== 4) {
        return;
      }
      if (xhr.status === 200) {
        console.log('SUCCESS', xhr.responseText);
        callback(JSON.parse(xhr.responseText));
      } else {
        console.warn('request_error');
      }
    };
    xhr.open('GET', "http://localhost:1337/api/usuarios/" + idUser);
    xhr.send();
  }
  //Llamada a la función
  getUser(response => (document.getElementById("nombre").innerHTML = response.data.attributes.Nombre),1);

*/

function getUsersByID(idUser){
    let url = "http://localhost:1337/api/usuarios/" + idUser;
    fetch(url).then((response) => {
                    return response.json();
                })
                .then((data) => {
                    let authors = data;
                    document.getElementById("nombre").innerHTML = authors.data.attributes.Nombre;
                    document.getElementById("apellido").innerHTML = authors.data.attributes.id_usuario;

}).catch(function(error) {
  console.log(error);
});
}

getUsersByID(1);

 // getUser(1).then(res => (document.getElementById("nombre").innerHTML = response.data.attributes.Nombre));



//document.getElementById("test1").innerHTML = getUser(1);


/*

//POST
let xhrpost = new XMLHttpRequest();
xhrpost.open("POST", "https://reqres.in/api/users");
xhrpost.setRequestHeader("Content-type", "application/json");
xhrpost.onreadystatechange = function () {
if (xhrpost.readyState === 4) {
    console.log(xhrpost.status);
    console.log(xhrpost.responseText);
}};

let object = {name:'Javier', job:'leader'};

xhrpost.send(JSON.stringify(object));


//PUT
let xhrput = new XMLHttpRequest();
xhrput.open("PUT", "https://reqres.in/api/users/2");
xhrput.setRequestHeader("Content-type", "application/json");
xhrput.onreadystatechange = function () {
if (xhrput.readyState === 4) {
    console.log(xhrput.status);
    console.log(xhrput.responseText);
}};

let objectput = {name:'Javier', job:'leader'};

xhrput.send(JSON.stringify(objectput));

//DELETE
let xhrdelete = new XMLHttpRequest();
xhrdelete.open("DELETE", "https://reqres.in/api/users/2");
xhrdelete.responseType = 'json';
xhrdelete.onreadystatechange = function () {
    if (xhrdelete.readyState === 4) { //Cuando termina de cargarse
        if(xhrdelete.status == 204){
            console.log(xhrdelete.status);
            console.log("Borrado correctamente");
        }
        }};
xhrdelete.send();


*/