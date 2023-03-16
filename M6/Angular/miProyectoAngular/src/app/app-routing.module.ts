import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentComponent } from './content/content/content.component';
import { ProductsComponent } from './products/products/products.component';

const routes: Routes = [
  { path: 'product', component: ProductsComponent },
  { path: 'home', component: ContentComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
  //,{ path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
