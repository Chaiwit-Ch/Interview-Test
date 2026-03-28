import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';

@Component({
  standalone: true,
  selector: 'app-users-list',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './users-list.component.html',
})
export class UsersListComponent implements OnInit {
  users: any[] = [];
  filteredUsers: any[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.userService.getUsers().subscribe({
      next: (res) => {
        this.users = res ?? [];
        this.filteredUsers = [...this.users];
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = 'Failed to load users.';
        this.isLoading = false;
      }
    });
  }
}