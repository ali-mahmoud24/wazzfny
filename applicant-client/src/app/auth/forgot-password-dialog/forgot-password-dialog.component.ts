import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { catchError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-forgot-password-dialog',
  templateUrl: './forgot-password-dialog.component.html',
  styleUrls: ['./forgot-password-dialog.component.css'],
})
export class ForgotPasswordDialogComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  isLoading = false;

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  onSubmit() {
    if (!this.forgotPasswordForm.valid) {
      return;
    }
    const email = this.forgotPasswordForm.value['email'];

    this.isLoading = true;

    this.authService
      .forgotPassword(email)
      .pipe(
        catchError((err) => {
          console.log(err);
          const durationInSeconds = 3;
          this.snackBar.open(err.error.message, 'تم', {
            duration: durationInSeconds * 1000,
            // panelClass: ['success-snackbar'],
          });

          this.isLoading = false;

          throw new Error(err);
        })
      )
      .subscribe((res) => {
        this.isLoading = false;


        const durationInSeconds = 3;
        this.snackBar.open('تم إرسال الرابط إلي البريد الإلكتروني', 'تم', {
          duration: durationInSeconds * 1000,
          // panelClass: ['success-snackbar'],
        });


        this.dialog.closeAll();
        

      });
  }

  private initForm() {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl('', [Validators.email, Validators.required]),
    });
  }
}
