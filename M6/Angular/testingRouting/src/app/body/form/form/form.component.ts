import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  customVal(campo: FormControl) {
    if (campo.value === 'Rojo') {return { colorErroneo: true };}
    else {return null;}
  }   

  nombre: FormControl = new FormControl('', Validators.required);
  apellido1: FormControl = new FormControl('', [Validators.minLength(3), Validators.maxLength(10)]);
  apellido2: FormControl = new FormControl('');
  color: FormControl = new FormControl('', [this.customVal, Validators.required]);
  adulto: FormControl = new FormControl('', Validators.required);
  fnacimiento: FormControl = new FormControl('', Validators.required);

  MyNewForm: FormGroup = new FormGroup({nombre:  this.nombre,
                                        apellido1: this.apellido1,
                                        apellido2: this.apellido2,
                                        color:  this.color,
                                        fnacimiento: this.fnacimiento,
                                        adulto: this.adulto});                               
  constructor() { }
  ngOnInit() {}
  Clic(datos:FormGroup) {console.log(datos);}
}





