import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book.service';
import { CartService } from '../../services/cart.service';
import { Book } from '../models/book.model';
import { NgIf } from '@angular/common';


@Component({
  selector: 'book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css'],
  standalone: true,
  imports: [NgIf],
})
export class BookDetailsComponent implements OnInit {

  book: Book | null = null;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    const bookId = +this.route.snapshot.paramMap.get('id')!;
    this.bookService.getBookById(bookId).subscribe((data: Book) => {
      this.book = data;
    });
  }

  addToCart(book: Book): void {
    this.cartService.addToCart(book);
    alert(`${book.title} has been added to your cart.`);
  }
}
