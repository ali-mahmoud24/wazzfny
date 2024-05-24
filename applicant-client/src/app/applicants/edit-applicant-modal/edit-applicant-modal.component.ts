import { environment } from '../../../environments/environment';
import { Component, Inject } from '@angular/core';

import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { Experience, JobCategory } from '../http-applicant.service';

import { ApplicantService } from '../applicant.service';
import { ApplicantRequest } from '../applicant.model';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

interface DialogData {
  applicant: ApplicantRequest;
}

@Component({
  selector: 'app-edit-applicant-modal',
  templateUrl: './edit-applicant-modal.component.html',
  styleUrls: ['./edit-applicant-modal.component.css'],
})
export class EditApplicantModalComponent {
  applicant: ApplicantRequest = this.data.applicant;

  editRequestApplicantForm: FormGroup;
  showJobs = true;

  jobCategoryList: JobCategory;
  experienceList: Experience;

  jobList: any;

  languageList: any;
  computerSkillList: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: DialogData,
    private httpClient: HttpClient,
    private applicantService: ApplicantService
  ) {}

  ngOnInit(): void {
    this.initForm();

    // Job Category
    this.httpClient
      .get(`${environment.apiUrl}/JobCategory/GetJobCategories`)
      .subscribe((res: any) => (this.jobCategoryList = res.data));

    // Job List
    this.httpClient
      .get(`${environment.apiUrl}/Job/GetJobs`)
      .subscribe((res: any) => (this.jobList = res.data));

    // Experience List
    this.httpClient
      .get(`${environment.apiUrl}/Experience/GetExperiences`)
      .subscribe((res: any) => (this.experienceList = res.data));

    // Language List
    this.httpClient
      .get(`${environment.apiUrl}/Language/GetLanguages`)
      .subscribe((res: any) => (this.languageList = res.data));

    // ComputerSkill List
    this.httpClient
      .get(`${environment.apiUrl}/ComputerSkill/GetComputerSkills`)
      .subscribe((res: any) => (this.computerSkillList = res.data));
  }

  onChange({ value }) {
    const jobCategoryId = value;

    // Job List
    this.httpClient
      .get(`${environment.apiUrl}/Job/GetJobsByCategory/${jobCategoryId}`)
      .subscribe(
        (res: any) => {
          this.jobList = res.data;
          this.showJobs = true;
        },
        (err) => (this.showJobs = false)
      );
  }

  onSubmit() {
    if (!this.editRequestApplicantForm.valid) {
      return;
    }

    const editedRequestApplicant = {
      requestId: this.applicant.requestId,
      ...this.editRequestApplicantForm.value,
      gender: this.editRequestApplicantForm.value['gender'],
      isNegotiate: this.editRequestApplicantForm.value['isNegotiate'],
      workType: this.editRequestApplicantForm.value['workType'],
    };

    this.applicantService.editApplicant(editedRequestApplicant);
  }

  private initForm() {
    this.editRequestApplicantForm = new FormGroup({
      //

      startPublish: new FormControl<Date | null>(
        this.applicant.startPublish,
        Validators.required
      ),
      endPublish: new FormControl<Date>(
        this.applicant.endPublish,
        Validators.required
      ),

      jobCategoryId: new FormControl(
        this.applicant.jobCategoryId,
        Validators.required
      ),
      jobId: new FormControl(this.applicant.jobId, Validators.required),
      //

      experienceId: new FormControl(
        this.applicant.experienceId,
        Validators.required
      ),

      gender: new FormControl(this.applicant.gender, Validators.required),
      //
      ageFrom: new FormControl(this.applicant.ageFrom, [
        Validators.required,
        Validators.min(18),
        Validators.max(60),
      ]),
      ageTo: new FormControl(this.applicant.ageTo, [
        Validators.required,
        Validators.min(18),
        Validators.max(60),
      ]),

      //
      computerSkillsIds: new FormControl(
        this.applicant.computerSkills.map((skill) => skill.computerSkillId),
        Validators.required
      ),
      languagesIds: new FormControl(
        this.applicant.languages.map((language) => language.languageId),
        Validators.required
      ),
      //
      salaryFrom: new FormControl(
        this.applicant.salaryFrom,
        Validators.required
      ),
      salaryTo: new FormControl(this.applicant.salaryTo, Validators.required),
      isNegotiate: new FormControl(
        this.applicant.isNegotiate,
        Validators.required
      ),
      //

      workType: new FormControl(this.applicant.workType, Validators.required),
      workHour: new FormControl(this.applicant.workHour, Validators.required),

      details: new FormControl(this.applicant.details, [
        Validators.required,
        Validators.maxLength(550),
      ]),
    });
  }
}
