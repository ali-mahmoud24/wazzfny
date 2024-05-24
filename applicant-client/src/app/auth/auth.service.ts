import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  BehaviorSubject,
  Subject,
  catchError,
  map,
  tap,
  throwError,
} from 'rxjs';
import { User } from './user.model';
import { Router } from '@angular/router';

import { Response } from '../response.model';

import { environment } from '../../environments/environment';

export interface AuthResponseData {
  userId: number;
  email: string;
  role: string;
  token: string;
  expiresIn: number;
}

export interface LoginData {
  email: string;
  password: string;
}

export interface RegisterData {
  email: string;
  password: string;
  role: string;
}

export interface ResetPasswordData {
  password: string;
  resetToken: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  user = new BehaviorSubject<User | null>(null);
  private tokenExpirationTimer: any;

  constructor(private http: HttpClient, private router: Router) {}

  register(registerData: RegisterData) {
    return this.http
      .post<Response<AuthResponseData>>(
        `${environment.apiUrl}/Auth/Register`,
        registerData
      )
      .pipe(
        catchError(this.handleError),
        tap((res) => this.handleAuthentication(res.data))
      );
  }

  login(loginData: LoginData) {
    return this.http
      .post<Response<AuthResponseData>>(
        `${environment.apiUrl}/Auth/Login`,
        loginData
      )
      .pipe(
        catchError(this.handleError),
        // map((res: any) => res.data),
        tap((res) => this.handleAuthentication(res.data))
      );
  }

  forgotPassword(email: string) {
    return this.http.post<Response<boolean>>(
      `${environment.apiUrl}/Auth/ForgotPassword`,
      {
        email,
      }
    );
    // .pipe(
    //   catchError(this.handleError)
    //   // map((res: any) => res.data),
    // );
  }

  resetPassowrd(resetPasswordData: ResetPasswordData) {
    return this.http.post<Response<boolean>>(
      `${environment.apiUrl}/Auth/ResetPassword`,
      resetPasswordData
    );
    // .pipe(
    //   catchError(this.handleError)
    //   // map((res: any) => res.data),
    // );
  }

  autoLogin() {
    const userDataJson = localStorage.getItem('userData');

    if (!userDataJson) {
      return;
    }

    const userData: {
      userId: number;
      email: string;
      role: string;
      _token: string;
      _tokenExpirationDate: string;
    } = JSON.parse(userDataJson);
    if (!userData) {
      return;
    }

    const loadedUser = new User(
      userData.userId,
      userData.email,
      userData.role,
      userData._token,
      new Date(userData._tokenExpirationDate)
    );

    if (loadedUser.token) {
      this.user.next(loadedUser);
      const expraitionDuration =
        new Date(userData._tokenExpirationDate).getTime() -
        new Date().getTime();

      this.autoLogout(expraitionDuration);
    }
  }

  logout() {
    this.user.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('userData');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.tokenExpirationTimer = null;
  }

  autoLogout(expirationDuration: number) {
    this.tokenExpirationTimer = setTimeout(
      () => this.logout(),
      expirationDuration
    );
  }

  private handleAuthentication(authData: AuthResponseData) {
    const expirationDate = new Date(
      new Date().getTime() + authData.expiresIn * 1000
    );

    const user = new User(
      authData.userId,
      authData.email,
      authData.role,
      authData.token,
      expirationDate
    );
    this.user.next(user);
    this.autoLogout(authData.expiresIn * 1000);
    localStorage.setItem('userData', JSON.stringify(user));
  }

  private handleError(errorRes: HttpErrorResponse) {
    let errorMessage = 'An unknown error occured!';

    console.log(errorRes.error.message);
    if (!errorRes.error || !errorRes.error) {
      return throwError(errorMessage);
    }
    switch (errorRes.error.message) {
      case 'EMAIL_EXISTS':
        errorMessage = 'هذا البريد اللإلكتروني مسجل من قبل.';
        break;
      case 'WRONG_CREDENTIALS':
        errorMessage = 'خطأ في البريد الإلكتروني أو كلمة السر.';
        break;
      // case 'INVALID_PASSWORD':
      //   errorMessage = 'This password is not correct.';
    }
    return throwError(errorMessage);
  }
}
