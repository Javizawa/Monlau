function getUsersByID(idUser){
    let url = "http://localhost:1337/api/usuarios/" + idUser;
    fetch(url).then((response) => {
                    return response.json();
                })
                .then((data) => {
                    let users = data;
                    document.getElementById("nombre").innerHTML = users.data.attributes.Nombre;
                    document.getElementById("apellido").innerHTML = users.data.attributes.id_usuario;})
                .catch(function(error) {
                    console.log(error);});
}

getUsersByID(1);

function getUsers(){
    let url = "http://localhost:1337/api/usuarios/";
    let ul = document.getElementById('users');
    let list = document.createDocumentFragment();
    fetch(url).then((response) => {
                    return response.json();
                })
                .then((data) => {
                    let users = data.data;
                    users.forEach(element => {
                       // console.log(element.attributes.Nombre);
                       let li = document.createElement('li');
                       let name = document.createElement('h2');
                       let id = document.createElement('span');
                        name.innerHTML = element.attributes.Nombre;
                        id.innerHTML = element.attributes.id_usuario;
                        li.appendChild(name);
                        li.appendChild(id);
                        list.appendChild(li);
                        ul.appendChild(list);
                    });
                })
                .catch(function(error) {
                    console.log(error);});
}

getUsers();


function postUser(data){
    const url = 'http://localhost:1337/api/usuarios';

    let request = new Request(url, {
                method: 'POST',
                body: JSON.stringify({data:data}),
                headers: new Headers({
                        'Content-Type': 'application/json; charset=UTF-8'
                    })
    });

fetch(request)
  .then(function() {
    // Handle response you get from the API
    console.log(request);
  });
}

let user = {
    Nombre: 'Sammy',
    id_usuario: '4'
};

//postUser(user);


function deleteUser(idUser){
    const url = 'http://localhost:1337/api/usuarios/' + idUser;
    let request = new Request(url, {method: 'DELETE'});

fetch(request)
  .then(function() {
    // Handle response you get from the API
    console.log(request);
  });
}

deleteUser(2);


//https://www.digitalocean.com/community/tutorials/how-to-use-the-javascript-fetch-api-to-get-data