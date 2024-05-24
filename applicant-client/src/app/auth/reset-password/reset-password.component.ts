import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from '../auth.service';
import { catchError } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  resetToken: string;
  isLoading = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.resetToken = this.route.snapshot.queryParams['resetToken'];

    this.initForm();
  }

  onSubmit() {
    if (!this.resetPasswordForm.valid) {
      return;
    }

    const resetPasswordData = {
      password: this.resetPasswordForm.value['newPassword'],
      resetToken: this.resetToken,
    };
    this.isLoading = true;

    this.authService
      .resetPassowrd(resetPasswordData)
      .pipe(
        catchError((err) => {
          this.isLoading = false;

          throw new Error(err);
        })
      )
      .subscribe((res) => {
        this.router.navigate(['/login']);
        this.isLoading = false;

        const durationInSeconds = 3;
        this.snackBar.open('تم تغيير كلمة السر بنجاح', 'تم', {
          duration: durationInSeconds * 1000,
          // panelClass: ['success-snackbar'],
        });
      });
  }



  private initForm() {
    this.resetPasswordForm = new FormGroup(
      {
        newPassword: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        confirmPassword: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
      },
      [this.MatchValidator('newPassword', 'confirmPassword')]
    );
  }

  MatchValidator(source: string, target: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const sourceCtrl = control.get(source);
      const targetCtrl = control.get(target);

      return sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value
        ? { mismatch: true }
        : null;
    };
  }


  get passwordMatchError() {
    return (
      this.resetPasswordForm.getError('mismatch') &&
      this.resetPasswordForm.get('confirmPassword')?.touched
    );
  }


}
