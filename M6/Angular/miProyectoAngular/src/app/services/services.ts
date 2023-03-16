import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class ProductsService{
    constructor(){}
    public productName = new BehaviorSubject<string>('');
    castProduct = this.productName.asObservable();


    public broadCastProductName(name:string){
        this.productName.next(name);
    }
}