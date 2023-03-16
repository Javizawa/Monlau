import { Injectable } from "@angular/core";
import { BehaviorSubject, of } from "rxjs";
import { Observable } from 'rxjs/internal/Observable';


@Injectable()
export class ProductsService{
    constructor(){}
   // public productName = new BehaviorSubject<string>('');
   // castProduct = this.productName.asObservable();

   // public productPrice = new BehaviorSubject<string>('');
   // castPrice = this.productPrice.asObservable();

    /*
    public broadCastProductName(name:string, price:string){
        this.productName.next(name);
        this.productPrice.next(price);
    }
    */

    products:Product[] = [ ];

    public sendProducts(name:string, price:string):Observable<Product[]>{
        if(name != "" && price != ""){
            this.products.push({name: name, price: price});
        }
        return of(this.products);
    }

}
export interface Product {name: string;  price: string; }