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
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  isLoading = false;
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  onSubmit() {
    if (!this.registerForm.valid) {
      return;
    }

    const role = this.registerForm.value['role'] ? 'Owner' : 'Client';

    const registerData = { ...this.registerForm.value, role };

    this.isLoading = true;

    console.log(registerData);

    this.authService.register(registerData).subscribe(
      (res) => {
        const { role } = res.data;
        if (role === 'Owner') {
          this.router.navigate(['owner', 'applicants']);
        } else {
          this.router.navigate(['applicants']);
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

  private initForm() {
    this.registerForm = new FormGroup(
      {
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        confirmPassword: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        role: new FormControl(''),
      },
      [this.MatchValidator('password', 'confirmPassword')]
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
      this.registerForm.getError('mismatch') &&
      this.registerForm.get('confirmPassword')?.touched
    );
  }
}
