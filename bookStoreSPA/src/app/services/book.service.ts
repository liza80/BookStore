import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../book/models/book.model';


@Injectable({
    providedIn: 'root'
})
export class BookService {

    private baseUrl = 'http://localhost:5029/api/book';  // Your backend API
    private searchUrl = 'http://localhost:5029/api/bookssearch/search';  // Your backend API

    constructor(private http: HttpClient) { }

    // Fetch all books
    getBooks(): Observable<Book[]> {
        return this.http.get<Book[]>(`${this.baseUrl}`);
    }

    // Search books
    searchBooks(searchTerm: string): Observable<Book[]> {
        return this.http.get<Book[]>(`${this.searchUrl}?queryParameters=${searchTerm}`);
    }

    // Get book by ID
    getBookById(id: number): Observable<Book> {
        return this.http.get<Book>(`${this.baseUrl}/${id}`);
    }
}
