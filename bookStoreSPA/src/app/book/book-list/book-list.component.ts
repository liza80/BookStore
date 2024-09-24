import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Book } from '../models/book.model';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';


@Component({
    selector: 'book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.css'],
    standalone: true,
    imports: [NgFor, FormsModule, RouterLink],
})
export class BookListComponent implements OnInit {

    books: Book[] = [];
    searchTerm: string = '';

    constructor(private bookService: BookService) { }

    ngOnInit(): void {
        this.loadBooks();
    }

    loadBooks(): void {
        this.bookService.getBooks().subscribe((data: Book[]) => {
            this.books = data;
        });
    }

    searchBooks(): void {
        if (this.searchTerm) {
            this.bookService.searchBooks(this.searchTerm).subscribe((data: Book[]) => {
                this.books = data;
            });
        } else {
            this.loadBooks();
        }
    }
}
