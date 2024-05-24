import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { map } from 'rxjs';

import { Response } from '../response.model';
import { ApplicantRequest } from '../applicants/applicant.model';

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

@Injectable({
  providedIn: 'root',
})
export class HttpApplicantService {
  constructor(private httpClient: HttpClient) {}

  addRequestApplicant(requestApplicant) {
    return this.httpClient
      .post<Response<ApplicantRequest>>(
        `${environment.apiUrl}/RequestApplicant/AddRequestApplicant`,
        requestApplicant
      )
      .pipe(map((res) => res.data));
  }

  fetchAdminApplicants() {
    return this.httpClient
      .get<Response<ApplicantRequest[]>>(
        `${environment.apiUrl}/RequestApplicant/Admin/GetRequestApplicants`
      )
      .pipe(map((res) => res.data));
  }

  fetchApplicantsByOwnerId(userId: number) {
    return this.httpClient
      .get<Response<ApplicantRequest[]>>(
        `${environment.apiUrl}/RequestApplicant/GetRequestApplicants/${userId}`
      )
      .pipe(map((res) => res.data));
  }

  fetchApplicants() {
    return this.httpClient
      .get<Response<ApplicantRequest[]>>(
        `${environment.apiUrl}/RequestApplicant/GetRequestApplicants`
      )
      .pipe(map((res) => res.data));
  }

  editApplicantById(editedApplicantReqeust: ApplicantRequest) {
    return this.httpClient
      .patch<Response<ApplicantRequest>>(
        `${environment.apiUrl}/RequestApplicant/EditRequestApplicant`,
        editedApplicantReqeust
      )
      .pipe(map((res) => res.data));
  }

  deleteApplicantById(requestId: number) {
    return this.httpClient
      .delete<Response<boolean>>(
        `${environment.apiUrl}/RequestApplicant/DeleteRequestApplicant/${requestId}`
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

  fetchJobCategories() {
    return this.httpClient
      .get<Response<JobCategory[]>>(
        `${environment.apiUrl}/JobCategory/GetJobCategories`
      )
      .pipe(map((res) => res.data));
  }

  editIsEndRequestApplicant(patchRequestData: {
    requestId: number;
    isEnd: boolean;
  }) {
    return this.httpClient
      .patch<Response<boolean>>(
        `${environment.apiUrl}/RequestApplicant/EditIsEndRequestApplicant`,
        patchRequestData
      )
      .pipe(map((res) => res.data));
  }
}
