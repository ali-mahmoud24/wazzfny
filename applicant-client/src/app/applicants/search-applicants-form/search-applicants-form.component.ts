import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ApplicantService } from '../applicant.service';
import { Experience, JobCategory } from '../http-applicant.service';
import { ApplicantRequest } from '../applicant.model';

@Component({
  selector: 'app-search-applicants-form',
  templateUrl: './search-applicants-form.component.html',
  styleUrls: ['./search-applicants-form.component.css'],
})
export class SearchApplicantsFormComponent implements OnInit {
  jobCategoryList: JobCategory[];
  experienceList: Experience[];

  applicantList: ApplicantRequest[];

  filterForm: FormGroup;

  constructor(private applicantService: ApplicantService) {}

  ngOnInit(): void {
    // APPLICANT REQUESTS
    // this.applicantList = this.applicantService.getApplicants();

    this.applicantService.applicantsChanged.subscribe((applicants) => {
      this.applicantList = applicants;
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

    this.initForm();
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

    const filteredApplicantList = this.applicantList.filter((req) => {
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

    this.applicantService.setApplicants(filteredApplicantList);
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
}
