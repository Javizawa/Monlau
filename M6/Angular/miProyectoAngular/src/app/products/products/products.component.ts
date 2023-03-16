import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductsService } from 'src/app/services/services';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  public productName: string = 'hola 2';
  data:any;
  ELEMENT_DATA: Fruits[] = [
    {position: 1, name: 'Patatas', price: 1.5, group: 'Kilo'},
    {position: 2, name: 'Cebollas', price: 0.75, group: 'Kilo'},
    {position: 3, name: 'Alcachofas', price: 2.99, group: 'Kilo'},
    {position: 4, name: 'Acelgas', price: 1, group: 'manojo'},
    {position: 5, name: 'Manzanas', price: 1.5, group: 'Kilo'},
    {position: 6, name: 'Peras', price: 2.2, group: 'Kilo'},
    {position: 7, name: 'Naranjas', price: 1.7, group: 'Kilo'},
    {position: 8, name: 'Mandarinas', price: 3.2, group: 'Kilo'},
    {position: 9, name: 'Sandía', price: 4.99, group: 'Pieza'},
    {position: 10, name: 'Melón', price: 3, group: 'Pieza'},
  ];
  dataSource = this.ELEMENT_DATA;


  constructor(private router:ActivatedRoute, private productService:ProductsService) { 
    this.productService.broadCastProductName(this.productName);
  }

  getProductName(){
    this.productService.broadCastProductName(this.productName);
  }

  ngOnInit() {
      this.router.snapshot.queryParams;
      this.data = this.router;
  }
}

export interface Fruits {
  name: string;
  position: number;
  price: number;
  group: string;
}



