import { Component, OnInit } from '@angular/core';
import { zip } from 'rxjs';
import { ProductsService } from '../../../../services/productServices';

@Component({
  selector: 'app-show-products',
  templateUrl: './show-products.component.html',
  styleUrls: ['./show-products.component.css']
})
export class ShowProductsComponent {
  
  products:Product[] = [ ];
  productName:any = "";
  productPrice:any = "";
  constructor(private productService:ProductsService){}

  ngOnInit(): void {
    /*
    zip(this.productService.castProduct, this.productService.castPrice)
      .subscribe(([productName, productPrice]) => {
        const product = { name: productName, price: productPrice };
        this.products.push(product);
      });
    */
   
    this.productService.sendProducts(this.productName, this.productPrice).subscribe((products:Product[]) => {
        this.products = products;
      });
  }

}

export interface Product {name: string; 
                          price: string; }