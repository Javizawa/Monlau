
/*function mostrarImagen(token) {
    // URL de la imagen que deseas mostrar
    const url = 'https://futdb.app/api/players/16247/image';
  
    // Crear una nueva solicitud GET
    const solicitud = new XMLHttpRequest();
  
    // Configurar la solicitud con el token de autenticación y la aceptación de imagen PNG
    solicitud.open('GET', url);
    solicitud.setRequestHeader('X-AUTH-TOKEN', token);
    solicitud.setRequestHeader('accept', 'image/png');
  
    // Cuando la respuesta de la solicitud esté lista, mostrar la imagen en un elemento de imagen HTML
    solicitud.onload = function() {
      const imagen = document.createElement('img');
      const imageBlob = new Blob([solicitud.response], { type: 'image/png' });
      imagen.src = URL.createObjectURL(imageBlob);
      document.body.appendChild(imagen);
    };
  
    // Enviar la solicitud
    solicitud.send();
  }
  */
  

  

function mostrarImagen(token) {
    // URL de la imagen que deseas mostrar
    var xhr = new XMLHttpRequest();
        xhr.open('get', 'https://futdb.app/api/players/16247/image', true);
        xhr.setRequestHeader('X-AUTH-TOKEN', token);
        xhr.responseType = 'blob';

        xhr.onload = () => {
            const imageBlob = new Blob([xhr.response], { type: 'image/png' });
            document.querySelector('#test').src = URL.createObjectURL(imageBlob);
        };

        xhr.send(); 

  }

  // Llamar a la función con el token de autenticación
  const token = 'd856397f-6543-4eb9-8170-9969d6a559cb';
  mostrarImagen(token);