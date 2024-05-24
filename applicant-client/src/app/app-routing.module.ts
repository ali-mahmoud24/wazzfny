import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddApplicantFormComponent } from './applicants/owner/add-applicant-form/add-applicant-form.component';
import { ApplicantListComponent } from './applicants/applicant-list/applicant-list.component';
import { AdminApplicantListComponent } from './applicants/admin/admin-applicant-list/admin-applicant-list.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { OwnerApplicantListComponent } from './applicants/owner/owner-applicant-list/owner-applicant-list.component';
import { LoginGuard } from './auth/login-guard';
import { OwnerGuard } from './auth/owner-guard';
import { AuthGuard } from './auth/auth-guard';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { AdminManagementComponent } from './applicants/admin/admin-management/admin-management.component';

const routes: Routes = [
  { path: '', redirectTo: '/applicants', pathMatch: 'full' },
  // { path: '', redirectTo: '/login', pathMatch: 'full' },

  // AUTH
  { path: 'login', component: LoginComponent, canActivate: [LoginGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [LoginGuard] },
  { path: 'reset-password', component: ResetPasswordComponent, canActivate: [LoginGuard] },
  // AUTH

  // APPLICANT - OWNER
  {
    path: 'applicants/new',
    component: AddApplicantFormComponent,
    canActivate: [AuthGuard, OwnerGuard],
  },
  {
    path: 'owner/applicants',
    component: OwnerApplicantListComponent,
    canActivate: [AuthGuard, OwnerGuard],
  },

          // APPLICANT - CLIENT
          {
            path: 'applicants',
            component: ApplicantListComponent,
            // canActivate: [AuthGuard],
          },
          // APPLICANT - CLIENT

  // APPLICANT - OWNER

  // APPLICANT - ADMIN
  { path: 'admin/applicants', component: AdminApplicantListComponent },
  { path: 'admin/management', component: AdminManagementComponent },
  // APPLICANT - ADMIN
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
