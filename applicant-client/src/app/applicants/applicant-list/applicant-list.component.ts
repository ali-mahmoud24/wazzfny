import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { MatDialog } from '@angular/material/dialog';
import { ApplicantDetailDialogComponent } from '../applicant-detail-dialog/applicant-detail-dialog.component';
import { ApplicantRequest } from '../applicant.model';
import { ApplicantService } from '../applicant.service';
import {
  Experience,
  HttpApplicantService,
  JobCategory,
} from '../http-applicant.service';

@Component({
  selector: 'app-applicant-list',
  templateUrl: './applicant-list.component.html',
  styleUrls: ['./applicant-list.component.css'],
})
export class ApplicantListComponent implements OnInit {
  jobCategoryList: JobCategory[];
  experienceList: Experience[];

  applicantList: ApplicantRequest[];
  filteredApplicantList: ApplicantRequest[];

  filterForm: FormGroup;

  activeCardId: number;

  constructor(
    private applicantService: ApplicantService,
    private httpApplicantService: HttpApplicantService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    // APPLICANT REQUESTS
    this.httpApplicantService.fetchApplicants().subscribe((applicants) => {
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

  private initForm() {
    this.filterForm = new FormGroup({
      jobName: new FormControl(''),
      selectedJobCategoryIds: new FormControl(''),
      selectedExperienceIds: new FormControl(''),
      selectedWorkTypes: new FormControl(''),
      selectedGenders: new FormControl(''),
    });
  }

  // onFilter() {
  //   const selectedWorkTypes = this.filterForm.value['workType'];

  //   const selectedExperiences = this.filterForm.value['experienceName'];

  //   // console.log(selectedWorkTypes);
  //   console.log(selectedExperiences);

  //   // Check if all filter arrays are empty, and if so, display all jobs
  //   if (selectedWorkTypes.length === 0 && selectedExperiences.length === 0) {
  //     this.filteredApplicantList = this.applicantList;
  //   }

  //   this.filteredApplicantList = this.applicantList.filter((req) => {
  //     let matchesFilter = true;
  //     if (
  //       selectedWorkTypes.length > 0 &&
  //       !selectedWorkTypes.includes(req.workType)
  //     ) {
  //       matchesFilter = false;
  //     }

  //     if (
  //       selectedExperiences.length > 0 &&
  //       !selectedExperiences.includes(req.experienceName)
  //     ) {
  //       matchesFilter = false;
  //     }

  //     return matchesFilter;
  //   });
  // }
}
