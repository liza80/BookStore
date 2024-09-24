import { Routes } from '@angular/router';
import { BookListComponent } from './book/book-list/book-list.component';
import { BookDetailsComponent } from './book/book-details/book-details.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    { path: '', component: BookListComponent },
    { path: 'book/:id', component: BookDetailsComponent },
    { path: 'cart', component: ShoppingCartComponent }
];
