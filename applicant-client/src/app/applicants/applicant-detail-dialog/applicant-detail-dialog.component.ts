import { Inject, Component } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApplicantRequest } from '../applicant.model';

interface DialogData {
  applicant: ApplicantRequest;
}

@Component({
  selector: 'app-applicant-detail-dialog',
  templateUrl: './applicant-detail-dialog.component.html',
  styleUrls: ['./applicant-detail-dialog.component.css'],
})
export class ApplicantDetailDialogComponent {
  applicant = this.data.applicant;

  constructor(@Inject(MAT_DIALOG_DATA) private data: DialogData) {}

  getApplicantGender(genderNumber: number) {
    if (genderNumber == 1) {
      return 'ذكر';
    }
    if (genderNumber == 2) {
      return 'أنثى';
    } else {
      return 'كلاهما';
    }
  }
}
