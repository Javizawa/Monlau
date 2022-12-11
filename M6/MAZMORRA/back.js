    //POSICIONAMIENTO DEL PERSONAJE
    personajeY = 2;
    personajeX = 0;

    //META
    metaX = 7;
    metaY = 1;
    meta = document.getElementById("meta");

    //CUENTA INTENTOS
    count = 0;
    intentos = document.getElementById("intentos");
    intentos.innerHTML = count;
    //MARCO PRINCIPAL
    let canvas = document.getElementById("marco");
    let context = canvas.getContext("2d");

    //BOOLEANAS
    let ct00,ct01,ct02,ct03,ct04,ct05,ct06,ct07,ct13,ct23,ct25,ct35 = false;


    clearCanvas();
    createMaze();

    context.fillStyle = "yellow";
    context.fillRect(personajeX*100, personajeY*100, 100, 100);
    context.strokeRect(personajeX*100, personajeY*100, 100, 100);

    function createMaze(){
        //FONDO DEL CUADRO
        context.fillStyle = "lightblue";
        context.fillRect(0, 0, 800, 400);
        context.strokeRect(0, 0, 800, 400);
        context.fillStyle = "red";
        //ZELDAS DE LA MAZMORRA
        if(ct00){
            context.fillRect(0, 0, 100, 100);
            context.strokeRect(0, 0, 100, 100);
        }
        if(ct01){
            context.fillRect(100, 0, 100, 100);
            context.strokeRect(100, 0, 100, 100);
        }
        if(ct02){
            context.fillRect(200, 0, 100, 100);
            context.strokeRect(200, 0, 100, 100);
        }
        if(ct03){
            context.fillRect(300, 0, 100, 100);
            context.strokeRect(300, 0, 100, 100);
        }
        if(ct04){
            context.fillRect(400, 0, 100, 100);
            context.strokeRect(400, 0, 100, 100);
        }
        if(ct05){
            context.fillRect(500, 0, 100, 100);
            context.strokeRect(500, 0, 100, 100);
        }
        if(ct06){
            context.fillRect(600, 0, 100, 100);
            context.strokeRect(600, 0, 100, 100);
        }
        if(ct07){
            context.fillRect(700, 0, 100, 100);
            context.strokeRect(700, 0, 100, 100);
        }
        if(ct13){
            context.fillRect(300, 100, 100, 100);
            context.strokeRect(300, 100, 100, 100);
        }
        if(ct23){
            context.fillRect(300, 200, 100, 100);
            context.strokeRect(300, 200, 100, 100);
        }
        if(ct25){
            context.fillRect(500, 200, 100, 100);
            context.strokeRect(500, 200, 100, 100);
        }
        if(ct35){
            context.fillRect(500, 300, 100, 100);
            context.strokeRect(500, 300, 100, 100);
        }
}

//GENERACIÓN DE LA LÓGICA DEL LABERINTO

var arrayBidimensional= new Array(4);
for (var i = 0; i < arrayBidimensional.length; i++) {
  arrayBidimensional[i] = new Array(7);
}

//INICIALIZACIÓN DE LA MAZMORRA
arrayBidimensional[0][0]=1;
arrayBidimensional[0][1]=1;
arrayBidimensional[0][2]=1;
arrayBidimensional[0][3]=1;
arrayBidimensional[0][4]=1;
arrayBidimensional[0][5]=1;
arrayBidimensional[0][6]=1;
arrayBidimensional[0][7]=1;
arrayBidimensional[1][3]=1;
arrayBidimensional[2][3]=1;
arrayBidimensional[2][5]=1;
arrayBidimensional[3][5]=1;



document.onkeyup = (e) => {
    if (e.key === "ArrowUp") {
        movimiento(1);
    } else if (e.key === "ArrowDown") {
        movimiento(2);
    } else if (e.key === "ArrowLeft") {
        movimiento(3);
    } else if (e.key === "ArrowRight") {
        movimiento(4);
    }
  }


function clearCanvas(){
    context.clearRect(0,0,canvas.width, canvas.height);
}

function mueveCanvas(){
    clearCanvas();
    createMaze();
    context.fillStyle = "yellow";
    context.fillRect(personajeX*100, personajeY*100, 100, 100);
    context.strokeRect(personajeX*100, personajeY*100, 100, 100);
}

function checkMeta(){
    if(personajeY == metaY && personajeX == metaX){
        meta.innerHTML = "HAS LLEGADO A LA META";
        context.fillStyle = "green";
        context.fillRect(personajeX*100, personajeY*100, 100, 100);
        context.strokeRect(personajeX*100, personajeY*100, 100, 100);
    }
}

function movimiento(desp){
    intentos.innerHTML = ++count;
    if(desp == 1){
        //ARRIBA
        if((personajeY - 1 )< 0){
            //ERROR 
            //window.alert("NO TE DEJO" );
        }else if(arrayBidimensional[personajeY - 1][personajeX] == 1){
            //window.alert("NO TE DEJO" );
            eval(('ct'+ (personajeY - 1) +''+personajeX) +" = " + true);            
            mueveCanvas();
        }else{
            personajeY--;
            mueveCanvas();
            checkMeta();
        }
    }
    if(desp == 2){
        if((personajeY + 1 ) >= arrayBidimensional.length){
            //ERROR 
            //window.alert("NO TE DEJO" );
        }else if(arrayBidimensional[personajeY + 1][personajeX] == 1){
            //window.alert("NO TE DEJO" );
            eval(('ct'+ (personajeY + 1) +''+personajeX) +" = " + true);            
            mueveCanvas();
        }else{
            personajeY++;
            mueveCanvas();
            checkMeta();
        }
    }
    if(desp == 3){
        if((personajeX - 1 ) < 0){
            //ERROR 
            //window.alert("NO TE DEJO" );
        }else if(arrayBidimensional[personajeY][personajeX -1] == 1){
            //window.alert("NO TE DEJO" );
            eval(('ct'+ (personajeY) +''+(personajeX -1)) +" = " + true);            
            mueveCanvas();
        }else{
            personajeX--;
            mueveCanvas();
            checkMeta();
        }
    }
    if(desp == 4){
        if((personajeX + 1 ) >= arrayBidimensional[0].length){
            //ERROR 
            //window.alert("NO TE DEJO" );
        }else if(arrayBidimensional[personajeY][personajeX +1] == 1){
            //window.alert("NO TE DEJO" );
            eval(('ct'+ (personajeY) +''+(personajeX + 1)) +" = " + true);            
            mueveCanvas();
        }else{
            personajeX++;
            mueveCanvas();
            checkMeta();
        }
    }
}