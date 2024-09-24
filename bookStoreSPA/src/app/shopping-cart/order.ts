import { CartItem } from "../book/models/cart-item.model";

export interface Order {
        id: number;
        orderDate:Date;
        totalAmount:number;
        cartItems: CartItem[];
   
}
