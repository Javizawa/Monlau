import { Component, ElementRef, ViewChild } from '@angular/core';
import { ProductsService } from 'src/app/services/productServices';


@Component({
  selector: 'app-addproducts',
  templateUrl: './addproducts.component.html',
  styleUrls: ['./addproducts.component.css']
})
export class AddproductsComponent {
  products:Product[] = [ ];
  productName:string = "";
  productPrice:string = "";
  @ViewChild("it1") myNameElem!: ElementRef;
  @ViewChild("it2") myNameElem2!: ElementRef;


  constructor(private productService:ProductsService){}

  public addProduct(){   
    /* 
    this.productService.broadCastProductName(this.myNameElem.nativeElement.value, 
                                             this.myNameElem2.nativeElement.value);
                                             */
    this.productService.sendProducts(this.myNameElem.nativeElement.value,
                                      this.myNameElem2.nativeElement.value);
  }
}
export interface Product {name: string; 
                          price: string; }