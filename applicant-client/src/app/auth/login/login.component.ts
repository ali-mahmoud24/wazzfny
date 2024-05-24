import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ForgotPasswordDialogComponent } from '../forgot-password-dialog/forgot-password-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  isLoading = false;
  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  private initForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }
    const loginData = this.loginForm.value;

    this.isLoading = true;

    this.authService.login(loginData).subscribe(
      (res) => {
        const { role } = res.data;
        if (role === 'Owner') {
          this.router.navigate(['owner', 'applicants']);
        } else if (role === 'Client') {
          this.router.navigate(['applicants']);
        } else {
          this.router.navigate(['admin', 'management']);
        }

        this.isLoading = false;
      },
      (errMessage) => {
        console.log(errMessage);
        const durationInSeconds = 3;
        this.snackBar.open(errMessage, 'تم', {
          duration: durationInSeconds * 1000,
          // panelClass: ['success-snackbar'],
        });

        this.isLoading = false;
      }
    );
  }

  onOpenForgetPasswordDialog() {
    this.dialog.open(ForgotPasswordDialogComponent);
  }
}
