
    
    function nuevoCliente(){
    
    document.getElementById("btnNew").style.display="none";
    document.getElementById("Button1").style.display="inline";
    document.getElementById("Button2").style.display="inline";
    Effect.SlideDown('x');
      document.getElementById("form1").reset();
    
   
    

    }
    
    
    function cierraNuevo(){
    document.getElementById("form1").reset();
    Effect.SlideUp('x');
    
     document.getElementById("btnNew").style.display="inline";
    document.getElementById("Button1").style.display="none";
    document.getElementById("Button2").style.display="none";
    
    }
    
    
    
    function nuevaSucur(){
    
    document.getElementById("Button4").style.display="none";
    document.getElementById("Button6").style.display="inline";
    document.getElementById("Button7").style.display="inline";

 Effect.SlideDown('y');
   document.getElementById("form1").reset();
     
    
   
    

    }
    
    
    function cierraSucur(){
    document.getElementById("form1").reset();
    Effect.SlideUp('y');
    
     document.getElementById("Button4").style.display="inline";
    document.getElementById("Button6").style.display="none";
    document.getElementById("Button7").style.display="none";
    
    }
    
    
    
    function confirmaGuardar(){
    
    if (confirm('¿Está seguro de querer guardar los datos?')==false) 
    return false;
    
    }


    function confirmaEditar(){
    
    if (confirm('¿Está seguro de querer actualizar los datos?')==false) 
    return false;
    
    }
    
    
    function confirmaEliminar(){
    
     if (confirm('¿Está seguro de querer eliminar los datos?')==false) 
    return false;
    
    
    }