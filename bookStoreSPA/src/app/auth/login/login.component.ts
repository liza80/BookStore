
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Router } from '@angular/router'; // Router for navigating after login
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements OnInit {
  loginForm: FormGroup;  // The form group for login form
  isSubmitted = false;   // Tracks form submission status
  errorMessage: string;  // Error message to display if login fails

  constructor(
    private fb: FormBuilder,  // FormBuilder service for reactive forms
    private authService: AuthService, // Authentication service
    private router: Router  // Router to navigate to other routes
  ) {}

  ngOnInit(): void {
    // Initialize the form with form controls
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],  // Username field with required validator
      password: ['', [Validators.required]]   // Password field with required validator
    });
  }

  // Getter for form controls for easy access in the template
  get formControls() { 
    return this.loginForm.controls; 
  }

  // Method to handle form submission
  onSubmit(): void {
    this.isSubmitted = true;

    // Stop if the form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    // Call the login method from AuthService and pass the form values
    this.authService.login(this.loginForm.value).subscribe({
      next: (response: any) => {
        // Store JWT token in local storage (or any other storage mechanism)
        localStorage.setItem('token', response.token);

        // Redirect to home or desired route after successful login
        this.router.navigate(['/']);
      },
      error: (error) => {
        // Handle error such as wrong credentials
        this.errorMessage = "Invalid username or password. Please try again.";
      }
    });
  }
}
