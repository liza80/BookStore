import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { BookListComponent } from "./book/book-list/book-list.component";
import { ShoppingCartComponent } from "./shopping-cart/shopping-cart.component";
;

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, BookListComponent, RouterLink, RouterLinkActive, ShoppingCartComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
    title = 'bookStoreSPA';
}
