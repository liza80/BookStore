import { Injectable } from '@angular/core';
import { Book } from '../book/models/book.model';
import { CartItem } from '../book/models/cart-item.model';


@Injectable({
  providedIn: 'root'
})
export class CartService {
  private localStorageKey = 'shoppingCart';
  cartItems: CartItem[] = [];

  constructor() {
    this.loadCartFromLocalStorage();  // Load cart from local storage on service initialization
   }

  addToCart(book: Book): void {
    const existingItem = this.cartItems.find(item => item.book.id === book.id);
    if (existingItem) {
      existingItem.quantity++;
    } else {
      this.cartItems.push({ book, quantity: 1 , unitprice:  book.price });
    }
     this.saveCartToLocalStorage();  // Save to local storage after modifying the cart
  }

  getCartItems(): CartItem[] {
    return this.cartItems;
  }

   // Update the quantity of an item in the cart
  updateQuantity(bookId: number, quantity: number): void {
    const item = this.cartItems.find(item => item.book.id === bookId);
    if (item) {
      item.quantity = quantity;
      if (item.quantity <= 0) {
        this.removeFromCart(bookId);  // Remove item if quantity is 0
      }
      this.saveCartToLocalStorage();  // Save the updated cart
    }
  }
  
  removeFromCart(bookId: number): void {
    this.cartItems = this.cartItems.filter(item => item.book.id !== bookId);
    this.saveCartToLocalStorage();  // Save the updated cart
  }

  clearCart(): void {
    this.cartItems = [];
    this.saveCartToLocalStorage();  // Clear local storage
  }
    // Calculate the total price of the cart
    getTotalPrice(): number {
      return this.cartItems.reduce((total, item) => total + item.book.price * item.quantity, 0);
    }

    private saveCartToLocalStorage(): void {
       localStorage.setItem(this.localStorageKey, JSON.stringify(this.cartItems));
    }

    // Load the cart from local storage
  private loadCartFromLocalStorage(): void {
     const storedCart = localStorage.getItem(this.localStorageKey);
     if (storedCart) {
       this.cartItems = JSON.parse(storedCart);
     }
  }
}
