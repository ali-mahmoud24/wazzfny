import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';

import { NavbarComponent } from './navbar/navbar.component';

// AUTH
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

// OWNER
import { AddApplicantFormComponent } from './applicants/owner/add-applicant-form/add-applicant-form.component';
import { OwnerApplicantListComponent } from './applicants/owner/owner-applicant-list/owner-applicant-list.component';

// OWNER + ADMIN
import { EditApplicantModalComponent } from './applicants/edit-applicant-modal/edit-applicant-modal.component';

// ADMIN
import { AdminApplicantListComponent } from './applicants/admin/admin-applicant-list/admin-applicant-list.component';

import { ApplicantListComponent } from './applicants/applicant-list/applicant-list.component';
import { ApplicantDetailDialogComponent } from './applicants/applicant-detail-dialog/applicant-detail-dialog.component';
import { SearchApplicantsFormComponent } from './applicants/search-applicants-form/search-applicants-form.component';
import { AuthInterceptorService } from './auth/auth-intreceptor.service';
import { ForgotPasswordDialogComponent } from './auth/forgot-password-dialog/forgot-password-dialog.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { AdminManagementComponent } from './applicants/admin/admin-management/admin-management.component';
import { EntityDialogComponent } from './applicants/admin/entity-dialog/entity-dialog.component';
import { ManageJobDialogComponent } from './applicants/admin/manage-job-dialog/manage-job-dialog.component';

// import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,

    LoginComponent,
    RegisterComponent,

    AddApplicantFormComponent,
    OwnerApplicantListComponent,

    ApplicantListComponent,
    ApplicantDetailDialogComponent,
    SearchApplicantsFormComponent,
    AdminApplicantListComponent,
    EditApplicantModalComponent,
    ForgotPasswordDialogComponent,
    ResetPasswordComponent,
    AdminManagementComponent,
    EntityDialogComponent,
    ManageJobDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,

    //
    AppRoutingModule,
    //
    MaterialModule,
    //
    // FlexLayoutModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],

  bootstrap: [AppComponent],
})
export class AppModule {}
