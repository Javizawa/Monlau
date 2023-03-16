import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header/header.component';
import { HomeComponent } from './body/home/home.component';
import { ProductsComponent } from './body/products/products.component';
import { FooterComponent } from './footer/footer/footer.component';
import { AddproductsComponent } from './body/products/addproducts/addproducts/addproducts.component';
import { ShowProductsComponent } from './body/products/showProducts/show-products/show-products.component';
import { ProductsService } from './services/productServices';
import { FormComponent } from './body/form/form/form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    ProductsComponent,
    FooterComponent,
    AddproductsComponent,
    ShowProductsComponent,
    FormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [ProductsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
