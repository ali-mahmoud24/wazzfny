import { NgModule } from '@angular/core';

import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatChipsModule } from '@angular/material/chips';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';

import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MatCheckboxModule } from '@angular/material/checkbox';


import { NgxLoadingButtonsModule } from 'ngx-loading-buttons';

@NgModule({
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatExpansionModule,
    //
    MatDatepickerModule,
    MatNativeDateModule,

    //
    MatChipsModule,

    //
    MatToolbarModule,
    MatSidenavModule,

    //
    MatSlideToggleModule,
    //
    MatSnackBarModule,
    //
    MatCheckboxModule,
    //
    NgxLoadingButtonsModule,
  ],
  exports: [
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatExpansionModule,
    //
    MatDatepickerModule,
    MatNativeDateModule,

    //
    MatChipsModule,
    //
    MatToolbarModule,
    MatSidenavModule,
    //
    MatSlideToggleModule,
    //
    //
    MatSnackBarModule,
    //
    MatCheckboxModule,
    //
    NgxLoadingButtonsModule,
  ],

  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'ar' }],
})
export class MaterialModule {}
