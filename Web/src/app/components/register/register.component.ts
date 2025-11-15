import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  userName = '';
  password = '';
  message: string | null = null;
  constructor(private auth: AuthService, private router: Router) {}
  submit() {
    this.auth.register(this.userName, this.password).subscribe({
      next: () => { this.message = 'Registered. Please login.'; this.router.navigate(['/login']); },
      error: () => this.message = 'Registration failed'
    });
  }
}
