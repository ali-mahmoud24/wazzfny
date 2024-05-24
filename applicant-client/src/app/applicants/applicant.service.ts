import { Injectable } from '@angular/core';

import { Subject } from 'rxjs';

import {
  Experience,
  HttpApplicantService,
  JobCategory,
} from './http-applicant.service';
import { ApplicantRequest } from './applicant.model';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class ApplicantService {
  private applicants: ApplicantRequest[] = [];
  applicantsChanged = new Subject<ApplicantRequest[]>();

  private experiences: Experience[] = [];
  experiencesChanged = new Subject<Experience[]>();

  private jobCategories: JobCategory[] = [];
  jobCategoriesChanged = new Subject<JobCategory[]>();

  constructor(
    private httpApplicantService: HttpApplicantService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  // APPLICANT REQUEST
  setApplicants(applicants: ApplicantRequest[]) {
    this.applicants = applicants;
    this.applicantsChanged.next(this.applicants);
  }

  getAdminApplicants() {
    this.httpApplicantService
      .fetchAdminApplicants()
      .subscribe((applicants: ApplicantRequest[]) => {
        this.setApplicants(applicants);
      });
    return this.applicants;
  }

  getApplicantsByOwnerId(userId: number) {
    this.httpApplicantService
      .fetchApplicantsByOwnerId(userId)
      .subscribe((applicants: ApplicantRequest[]) => {
        this.setApplicants(applicants);
      });
    return this.applicants;
  }

  editApplicant(editedApplicantReqeust: ApplicantRequest) {
    this.httpApplicantService
      .editApplicantById(editedApplicantReqeust)
      .subscribe((applicant: ApplicantRequest) => {
        this.dialog.closeAll();

        this.snackBar.open('تم تعديل الوظيفة بنجاح', 'تم', {
          duration: 3 * 1000,
        });

        const applicantIndex = this.applicants.findIndex(
          (applicant) => applicant.requestId == editedApplicantReqeust.requestId
        );

        this.applicants[applicantIndex] = applicant;

        this.setApplicants(this.applicants);
      });
  }

  deleteApplicantById(requestId: number) {
    this.httpApplicantService.deleteApplicantById(requestId).subscribe(() => {
      this.snackBar.open('تم إلغاء الوظيفة بنجاح', 'تم', {
        duration: 3 * 1000,
      });

      const filteredApplicants = this.applicants.filter(
        (applicant) => applicant.requestId !== requestId
      );

      this.setApplicants(filteredApplicants);
    });
  }

  // EXPERIENCES
  setExperiences(experiences: Experience[]) {
    this.experiences = experiences;
    this.experiencesChanged.next(this.experiences);
  }

  getExperiences() {
    this.httpApplicantService.fetchExperiences().subscribe((experiences) => {
      this.setExperiences(experiences);
    });
    return this.experiences;
  }

  // JOB CATEGORES
  setJobCategories(jobCategories: JobCategory[]) {
    this.jobCategories = jobCategories;
    this.jobCategoriesChanged.next(this.jobCategories);
  }

  getJobCategories() {
    this.httpApplicantService
      .fetchJobCategories()
      .subscribe((jobCategories) => {
        this.setJobCategories(jobCategories);
      });
    return this.jobCategories;
  }
}
