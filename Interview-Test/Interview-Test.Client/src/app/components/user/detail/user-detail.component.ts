import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../services/user.service';

@Component({
    standalone: true,
    selector: 'app-user-detail',
    imports: [CommonModule, FormsModule, RouterModule],
    templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit {
    userId = '';
    user: any = null;
    isLoading = false;
    errorMessage = '';
    debugMeg ='';
    constructor(private route: ActivatedRoute,private userService: UserService) {}

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        this.userId = id ?? '';

        if (this.userId !== '') {
            console.log('ID: ', id);
            this.loadUserDetail();
        }
    }
    loadUserDetail(): void {
        this.isLoading = true;
        this.errorMessage = '';

        this.userService.getUserById(this.userId).subscribe({
        next: (res) => {
            this.user = res;
            console.log('User: ', this.user);
            this.isLoading = false;
        },
        error: (err) => {
            console.error(err);
            this.errorMessage = 'Failed to load user detail.';
            this.isLoading = false;
        }
        });
    }
}
