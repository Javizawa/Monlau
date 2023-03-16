import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './body/home/home.component';
import { ProductsComponent } from './body/products/products.component';
import { FormComponent } from './body/form/form/form.component';

const routes: Routes = [
  { path: 'product', component: ProductsComponent },
  { path: 'home', component: HomeComponent },
  { path: 'form', component: FormComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
  //,{ path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
