import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { MatDialog } from '@angular/material/dialog';
import { ApplicantDetailDialogComponent } from '../../applicant-detail-dialog/applicant-detail-dialog.component';
import { ApplicantRequest } from '../../applicant.model';
import { ApplicantService } from '../../applicant.service';
import {
  Experience,
  HttpApplicantService,
  JobCategory,
} from '../../http-applicant.service';

import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { EditApplicantModalComponent } from 'src/app/applicants/edit-applicant-modal/edit-applicant-modal.component';

@Component({
  selector: 'app-admin-applicant-list',
  templateUrl: './admin-applicant-list.component.html',
  styleUrls: ['./admin-applicant-list.component.css'],
})
export class AdminApplicantListComponent implements OnInit {
  jobCategoryList: JobCategory[];
  experienceList: Experience[];

  applicantList: ApplicantRequest[];
  filteredApplicantList: ApplicantRequest[];

  filterForm: FormGroup;
  changeStatusForm: FormGroup;

  activeCardId: number;

  constructor(
    private applicantService: ApplicantService,
    private httpApplicantService: HttpApplicantService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    // APPLICANT REQUESTS
    this.applicantList = this.applicantService.getAdminApplicants();
    this.filteredApplicantList = this.applicantList;

    this.applicantService.applicantsChanged.subscribe((applicants) => {
      this.applicantList = applicants;
      this.filteredApplicantList = applicants;
    });

    // JOB CATEGORIES
    this.jobCategoryList = this.applicantService.getJobCategories();

    this.applicantService.jobCategoriesChanged.subscribe((jobCategories) => {
      this.jobCategoryList = jobCategories;
    });

    // EXPERIENCES
    this.experienceList = this.applicantService.getExperiences();

    this.applicantService.experiencesChanged.subscribe((experiences) => {
      this.experienceList = experiences;
    });

    // INITALIZE FORM
    this.initForm();
  }

  onOpenDetailsDialog(requestId: number) {
    const applicant = this.applicantList.find(
      (request) => request.requestId == requestId
    );

    this.activeCardId = applicant.requestId;

    this.dialog.open(ApplicantDetailDialogComponent, {
      data: { applicant },
    });
  }

  onFilter() {
    const selectedExperienceIds =
      this.filterForm.value['selectedExperienceIds'];

    const selectedJobCategoryIds =
      this.filterForm.value['selectedJobCategoryIds'];

    const jobName = this.filterForm.value['jobName'];

    const selectedStatuses = this.filterForm.value['selectedStatuses'];

    let selectedWorkTypes = this.filterForm.value['selectedWorkTypes'];
    // Mapping the option values from "0" and "1" to true and false bec. values from Db are boolean
    if (selectedWorkTypes.length > 0) {
      selectedWorkTypes = selectedWorkTypes.map((type) => Boolean(+type));
    }

    let selectedGenders = this.filterForm.value['selectedGenders'];
    // Mapping the option values from "0" and "1" to true and false bec. values from Db are boolean
    if (selectedGenders.length > 0) {
      selectedGenders = selectedGenders.map((type) => +type);
    }

    const filters = {
      jobName,
      experienceId: selectedExperienceIds,
      jobCategoryId: selectedJobCategoryIds,
      workType: selectedWorkTypes,
      gender: selectedGenders,
      isEnd: selectedStatuses,
    };

    const filterKeys = Object.keys(filters);

    this.filteredApplicantList = this.applicantList.filter((req) => {
      return filterKeys.every((key) => {
        if (!filters[key].length) return true;
        // Loops again if req[key] is an array (for material attribute).
        if (Array.isArray(req[key])) {
          return req[key].some((keyEle) => filters[key].includes(keyEle));
        }
        if (key === 'jobName') {
          // Add the job name filter logic
          return req[key].toLowerCase().includes(filters[key].toLowerCase());
        }
        return filters[key].includes(req[key]);
      });
    });
  }

  getDiffDays(publishDate: any) {
    const today = new Date();
    const diffInMs = +new Date(today) - +new Date(publishDate);

    const diffInDays = diffInMs / (1000 * 60 * 60 * 24);

    return Math.floor(diffInDays);
  }

  changeRequestApplicantStatus(event, id: number, isEnd: boolean) {
    event.stopPropagation();

    const data = { requestId: id, isEnd };

    this.httpApplicantService
      .editIsEndRequestApplicant(data)
      .subscribe((res) => this.openSnackBar());
  }

  private initForm() {
    this.filterForm = new FormGroup({
      jobName: new FormControl(''),
      selectedJobCategoryIds: new FormControl(''),
      selectedExperienceIds: new FormControl(''),
      selectedWorkTypes: new FormControl(''),
      selectedGenders: new FormControl(''),
      selectedStatuses: new FormControl(''),
    });

    // this.changeStatusForm = new FormGroup({
    //   isEnd: new FormControl(true),
    // });
  }

  // openSnackBar() {
  //   const durationInSeconds = 5;
  //   this.snackBar.openFromComponent(PizzaPartyAnnotatedComponent, {
  //     duration: durationInSeconds * 1000,
  //     panelClass: ['success-snackbar'],
  //   });
  // }

  openSnackBar() {
    const durationInSeconds = 3;
    this.snackBar.open('تم تعديل الحالة بنجاح', 'تم', {
      duration: durationInSeconds * 1000,
      // panelClass: ['success-snackbar'],
    });
  }

  onOpenEditModal(event, requestId: number) {
    event.stopPropagation();

    const applicant = this.applicantList.find(
      (request) => request.requestId == requestId
    );

    this.dialog.open(EditApplicantModalComponent, {
      data: { applicant },
    });
  }

  onDeleteApplicant(event, requestId: number) {
    event.stopPropagation();

    this.applicantService.deleteApplicantById(requestId);
  }
}

// @Component({
//   selector: 'app-snack-bar-success',
//   template: `
//     <span class="example-pizza-party" matSnackBarLabel> تم التعديل بنجاح </span>
//     <span matSnackBarActions>
//       <button
//         mat-button
//         matSnackBarAction
//         (click)="snackBarRef.dismissWithAction()"
//       >
//         🍕
//       </button>
//     </span>
//   `,
//   styles: [
//     `
//       :host {
//         display: flex;
//       }

//       .example-pizza-party {
//         color: #fff;
//       }
//     `,
//   ],
// })
// export class PizzaPartyAnnotatedComponent {
//   snackBarRef = inject(MatSnackBarRef);
// }
