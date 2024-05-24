import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { environment } from 'src/environments/environment';
import {
  Experience,
  HttpApplicantService,
  JobCategory,
} from '../../http-applicant.service';

@Component({
  selector: 'app-add-applicant-form',
  templateUrl: './add-applicant-form.component.html',
  styleUrls: ['./add-applicant-form.component.css'],
})
export class AddApplicantFormComponent implements OnInit {
  addRequestApplicantForm: FormGroup;
  isLoading = false;
  userId: number;

  showJobs = false;

  jobCategoryList: JobCategory[];
  experienceList: Experience[];
  jobList: any;
  languageList: any;
  computerSkillList: any;

  constructor(
    private httpClient: HttpClient,
    private httpApplicantServcie: HttpApplicantService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.userId = this.authService.user.value.userId;

    // Job Category
    this.httpClient
      .get(`${environment.apiUrl}/JobCategory/GetJobCategories`)
      .subscribe((res: any) => (this.jobCategoryList = res.data));

    // // Job List
    // this.httpClient
    //   .get(`${this.apiUrl}/Job/GetJobs`)
    //   .subscribe((res: any) => (this.jobList = res.data));

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
    if (!this.addRequestApplicantForm.valid) {
      return;
    }

    const newRequestApplicant = {
      ...this.addRequestApplicantForm.value,
      userId: this.userId,
      postDate: new Date(),
      isEnd: false,
    };

    this.isLoading = true;

    this.httpApplicantServcie
      .addRequestApplicant(newRequestApplicant)
      .subscribe(
        (res) => {
          this.router.navigate(['owner', 'applicants']);
          this.isLoading = false;
        },
        (err) => (this.isLoading = false)
      );
  }

  private initForm() {
    this.addRequestApplicantForm = new FormGroup({
      //

      startPublish: new FormControl<Date | null>(null, Validators.required),
      endPublish: new FormControl<Date>(null, Validators.required),
      //
      jobCategoryId: new FormControl('', Validators.required),
      jobId: new FormControl('', Validators.required),
      //
      experienceId: new FormControl('', Validators.required),

      gender: new FormControl('', Validators.required),
      //
      ageFrom: new FormControl('', [
        Validators.required,
        Validators.min(18),
        Validators.max(60),
      ]),
      ageTo: new FormControl('', [
        Validators.required,
        Validators.min(18),
        Validators.max(60),
      ]),
      //
      computerSkillsIds: new FormControl('', Validators.required),
      languagesIds: new FormControl('', Validators.required),
      //
      salaryFrom: new FormControl('', Validators.required),
      salaryTo: new FormControl('', Validators.required),
      isNegotiate: new FormControl('', Validators.required),
      //

      workType: new FormControl('', Validators.required),
      workHour: new FormControl('', Validators.required),

      details: new FormControl('', [
        Validators.required,
        Validators.maxLength(550),
      ]),
    });
  }
}
