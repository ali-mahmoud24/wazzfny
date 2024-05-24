import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Job, JobCategory } from '../http-entity.service';
import { EntityService } from '../entity.service';

@Component({
  selector: 'app-manage-job-dialog',
  templateUrl: './manage-job-dialog.component.html',
  styleUrls: ['./manage-job-dialog.component.css'],
})
export class ManageJobDialogComponent implements OnInit {
  jobCategoryList: JobCategory[];
  jobList: Job[];
  addJobForm: FormGroup;
  isLoading: false;

  constructor(private entityService: EntityService) {}

  ngOnInit(): void {
    // JOB CATEGORIES
    this.jobCategoryList = this.entityService.getJobCategories();

    this.entityService.jobCategoriesChanged.subscribe(
      (jobCategories) => (this.jobCategoryList = jobCategories)
    );

    // JOBs

    this.jobList = this.entityService.getJobs();

    this.entityService.jobsChanged.subscribe((jobs) => (this.jobList = jobs));

    this.initForm();
  }

  onSubmit() {
    if (!this.addJobForm.valid) {
      return;
    }

    const jobCategoryId = this.addJobForm.value['jobCategoryId'];
    const name = this.addJobForm.value['name'];
    const notes = this.addJobForm.value['notes'];

    this.entityService.addJob(name, notes, jobCategoryId);
    this.addJobForm.reset();
  }

  findName(obj: any) {
    const keys = Object.keys(obj);
    const dynamicKey = keys.find(
      (key) => key !== 'notes' && !key.includes('Id')
    );

    const name = obj[dynamicKey];
    return name;
  }

  private initForm() {
    this.addJobForm = new FormGroup({
      jobCategoryId: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      notes: new FormControl('', [Validators.maxLength(250)]),
    });
  }
}
