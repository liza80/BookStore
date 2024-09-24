import { Component, OnInit } from '@angular/core';
import { CartItem } from '../book/models/cart-item.model';
import { CartService } from '../services/cart.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../services/order.service';

@Component({
    selector: 'shopping-cart',
    standalone: true,
    imports: [NgIf, NgFor, FormsModule],
    templateUrl: './shopping-cart.component.html',
    styleUrl: './shopping-cart.component.css'
})
export class ShoppingCartComponent implements OnInit {

    cartItems: CartItem[] = [];
    totalPrice: number = 0;

    constructor(private cartService: CartService, private orderService: OrderService ) {
        
    }

    ngOnInit(): void {
        this.getCartItems();
    }

    getCartItems(): void {
        this.cartItems = this.cartService.getCartItems();
        this.calculateTotal();
      }
    
      calculateTotal(): void {
        this.totalPrice = this.cartItems.reduce((acc, item) => acc + item.book.price * item.quantity, 0);
      }
      
    // Update the quantity of an item
    updateQuantity(item: any, quantity: number): void {
        this.cartService.updateQuantity(item, quantity);
        this.calculateTotal();
      }

    // Remove an item from the cart
    removeFromCart(bookId: number): void {
        this.cartService.removeFromCart(bookId);
        this.getCartItems();
    }
    processPayment(): void {
        // Prepare order data
        const order = {
            OrderItems: this.cartItems,
          totalAmount: this.totalPrice,
          id: 123, // You can use a real customer ID from your authentication system
          orderDate: new Date()
        };
    
        // Send the POST request to place the order
        this.orderService.placeOrder(order).subscribe(
            {
            next: (response) => {
                console.log('Order placed successfully', response);
                alert('Order placed successfully!');
                this.cartService.clearCart();
                this.getCartItems(); // Refresh cart items
              },
            error: (error) => {
                console.error('Error placing order', error);
                alert('There was an issue placing your order. Please try again.');
              }

            });
    }
}
