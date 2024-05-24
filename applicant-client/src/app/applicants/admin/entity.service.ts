import { Injectable } from '@angular/core';

import { Subject } from 'rxjs';

import {
  ComputerSkill,
  Experience,
  HttpEntityService,
  Job,
  JobCategory,
  Language,
} from './http-entity.service';

@Injectable({
  providedIn: 'root',
})
export class EntityService {
  constructor(private httpEntityService: HttpEntityService) {}

  // EXPERIENCES

  private experiences: Experience[] = [];
  experiencesChanged = new Subject<Experience[]>();

  setExperiences(experiences: Experience[]) {
    this.experiences = experiences;
    this.experiencesChanged.next(this.experiences);
  }

  getExperiences() {
    this.httpEntityService.fetchExperiences().subscribe((experiences) => {
      this.setExperiences(experiences);
    });
    return this.experiences;
  }

  addExperience(name: string, notes: string) {
    this.httpEntityService
      .addExperience(name, notes)
      .subscribe((experience) => {
        this.experiences.push(experience);
        this.setExperiences(this.experiences);
      });
  }

  // JOB CATEGORES

  private jobCategories: JobCategory[] = [];
  jobCategoriesChanged = new Subject<JobCategory[]>();

  setJobCategories(jobCategories: JobCategory[]) {
    this.jobCategories = jobCategories;
    this.jobCategoriesChanged.next(this.jobCategories);
  }

  getJobCategories() {
    this.httpEntityService.fetchJobCategories().subscribe((jobCategories) => {
      this.setJobCategories(jobCategories);
    });
    return this.jobCategories;
  }

  addJobCategory(name: string, notes: string) {
    this.httpEntityService
      .addJobCategory(name, notes)
      .subscribe((jobCategory) => {
        this.jobCategories.push(jobCategory);
        this.setJobCategories(this.jobCategories);
      });
  }

  // deleteJobCategoryId(jobCategoryId: number) {
  //   this.httpEntityService
  //     .deleteJobCategoryById(jobCategoryId)
  //     .subscribe(() => {
  //       this.snackBar.open('تم إلغاء الوظيفة بنجاح', 'تم', {
  //         duration: 3 * 1000,
  //       });

  //       const filteredJobCategories = this.jobCategories.filter(
  //         (applicant) => applicant.jobCategoryId !== jobCategoryId
  //       );

  //       this.setJobCategories(filteredJobCategories);
  //     });
  // }

  // COMPUTER SKILLS

  private computerSkills: ComputerSkill[] = [];
  computerSkillsChanged = new Subject<ComputerSkill[]>();

  setComputerSkills(computerSkills: ComputerSkill[]) {
    this.computerSkills = computerSkills;
    this.computerSkillsChanged.next(this.computerSkills);
  }

  getComputerSkills() {
    this.httpEntityService.fetchComputerSkills().subscribe((computerSkills) => {
      this.setComputerSkills(computerSkills);
    });
    return this.computerSkills;
  }

  addComputerSkill(name: string, notes: string) {
    this.httpEntityService
      .addComputerSkill(name, notes)
      .subscribe((computerSkill) => {
        this.computerSkills.push(computerSkill);
        this.setComputerSkills(this.computerSkills);
      });
  }

  // LANGUAGES

  private languages: Language[] = [];
  languagesChanged = new Subject<Language[]>();

  setLanguages(languages: Language[]) {
    this.languages = languages;
    this.languagesChanged.next(this.languages);
  }

  getLanguages() {
    this.httpEntityService.fetchLanguages().subscribe((languages) => {
      this.setLanguages(languages);
    });
    return this.computerSkills;
  }

  addLanguage(name: string, notes: string) {
    this.httpEntityService.addLanguage(name, notes).subscribe((languages) => {
      this.languages.push(languages);
      this.setLanguages(this.languages);
    });
  }

  // JOBS

  private jobs: Job[] = [];
  jobsChanged = new Subject<Job[]>();

  setJobs(jobs: Job[]) {
    this.jobs = jobs;
    this.jobsChanged.next(this.jobs);
  }

  getJobs() {
    this.httpEntityService.fetchJobs().subscribe((jobs) => {
      this.setJobs(jobs);
    });
    return this.jobs;
  }

  addJob(name: string, notes: string, jobCategoryId: number) {
    this.httpEntityService
      .addJob(name, notes, jobCategoryId)
      .subscribe((jobs) => {
        this.jobs.push(jobs);
        this.setJobs(this.jobs);
      });
  }
}
