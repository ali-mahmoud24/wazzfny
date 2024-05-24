export interface ApplicantRequest {
  requestId: number;

  jobId: number;
  jobName: string;

  jobCategoryId: number;
  // jobCategorName: string;

  experienceId: number;
  experienceName: string;

  postDate: Date;

  startPublish: Date;
  endPublish: Date;

  ageFrom: number;
  ageTo: number;

  gender: number;
  salaryFrom: number;
  salaryTo: number;
  isNegotiate: false;

  workType: true;
  workHour: number;

  languages: { languageId: number; languageName: string }[];
  computerSkills: { computerSkillId: number; skillName: string }[];

  details: string;

  isEnd: boolean;
}
