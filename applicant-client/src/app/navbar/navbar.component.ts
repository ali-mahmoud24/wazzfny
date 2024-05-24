import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService) {}

  private userSub: Subscription;
  isAuthenticated = false;
  isOwner = false;
  isAdmin = false;
  isClient = false;

  ngOnInit(): void {
    this.userSub = this.authService.user.subscribe((user) => {
      console.log(user);
      this.isAuthenticated = !!user;
      if (this.isAuthenticated) {
        this.isOwner = user.role === 'Owner' ? true : false;
        this.isAdmin = user.role === 'Admin' ? true : false;
        this.isClient = user.role === 'Client' ? true : false;
      }
    });
  }

  onLogout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }
}
