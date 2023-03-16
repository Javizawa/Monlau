import {  Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductsService } from '../../services/services';


@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.css']
})

export class ContentComponent implements OnInit{
  
  @Input() title:string ='';

  constructor(private router:Router, private productService:ProductsService) { }
  productName:string | undefined;

  ngOnInit() {
    this.productService.castProduct.subscribe(productName => this.productName = productName);
  }

  queryParams = {id: 1, name: 'hola'};

  gotoProducts(){
    this.router.navigate(['/product'],{queryParams: this.queryParams});
  }

}


