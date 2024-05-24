import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { map } from 'rxjs';

import { Response } from '../../response.model';

export interface Experience {
  experienceId: number;
  experienceName: string;
  notes: string;
}

export interface JobCategory {
  jobCategoryId: number;
  categoryName: string;
  notes: string;
}
export interface Job {
  jobId: number;
  jobCategoryId: number;
  jobName: string;
  notes: string;
}

export interface ComputerSkill {
  computerSkillId: number;
  skillName: string;
  notes: string;
}

export interface Language {
  languageId: number;
  languageName: string;
  notes: string;
}

@Injectable({
  providedIn: 'root',
})
export class HttpEntityService {
  constructor(private httpClient: HttpClient) {}

  // EXPERIENCES

  addExperience(name: string, notes: string) {
    return this.httpClient
      .post<Response<Experience>>(
        `${environment.apiUrl}/Experience/AddExperience`,
        { experienceName: name, notes: notes }
      )
      .pipe(map((res) => res.data));
  }

  fetchExperiences() {
    return this.httpClient
      .get<Response<Experience[]>>(
        `${environment.apiUrl}/Experience/GetExperiences`
      )
      .pipe(map((res) => res.data));
  }

  // JOB CATEGORIES

  addJobCategory(name: string, notes: string) {
    return this.httpClient
      .post<Response<JobCategory>>(
        `${environment.apiUrl}/JobCategory/AddJobCategory`,
        { categoryName: name, notes: notes }
      )
      .pipe(map((res) => res.data));
  }

  fetchJobCategories() {
    return this.httpClient
      .get<Response<JobCategory[]>>(
        `${environment.apiUrl}/JobCategory/GetJobCategories`
      )
      .pipe(map((res) => res.data));
  }

  // COMPUTER SKILLS

  addComputerSkill(name: string, notes: string) {
    return this.httpClient
      .post<Response<ComputerSkill>>(
        `${environment.apiUrl}/ComputerSkill/AddComputerSkill`,
        { skillName: name, notes: notes }
      )
      .pipe(map((res) => res.data));
  }

  fetchComputerSkills() {
    return this.httpClient
      .get<Response<ComputerSkill[]>>(
        `${environment.apiUrl}/ComputerSkill/GetComputerSkills`
      )
      .pipe(map((res) => res.data));
  }

  // LANGUAGES

  addLanguage(name: string, notes: string) {
    return this.httpClient
      .post<Response<Language>>(`${environment.apiUrl}/Language/AddLanguage`, {
        languageName: name,
        notes: notes,
      })
      .pipe(map((res) => res.data));
  }

  fetchLanguages() {
    return this.httpClient
      .get<Response<Language[]>>(`${environment.apiUrl}/Language/GetLanguages`)
      .pipe(map((res) => res.data));
  }

  // JOBS

  addJob(name: string, notes: string, jobCategoryId: number) {
    return this.httpClient
      .post<Response<Job>>(`${environment.apiUrl}/Job/AddJob`, {
        jobCategoryId,
        jobName: name,
        notes,
      })
      .pipe(map((res) => res.data));
  }

  fetchJobs() {
    return this.httpClient
      .get<Response<Job[]>>(`${environment.apiUrl}/Job/GetJobs`)
      .pipe(map((res) => res.data));
  }
}
