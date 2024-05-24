import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EntityDialogComponent } from '../entity-dialog/entity-dialog.component';
import { ManageJobDialogComponent } from '../manage-job-dialog/manage-job-dialog.component';

interface Count {
  jobCategoryCount: number;
  experienceCount: number;
  languagesCount: number;
  skillCount: number;
}

@Component({
  selector: 'app-admin-management',
  templateUrl: './admin-management.component.html',
  styleUrls: ['./admin-management.component.css'],
})
export class AdminManagementComponent {
  constructor(private dialog: MatDialog) {}

  onOpenJobCategoryDialog() {
    this.dialog.open(EntityDialogComponent, { data: { ...jobCategoryData } });
  }

  onOpenExperienceDialog() {
    this.dialog.open(EntityDialogComponent, { data: { ...experienceData } });
  }

  onOpenLanguageDialog() {
    this.dialog.open(EntityDialogComponent, { data: { ...languageData } });
  }

  onOpenSkillDialog() {
    this.dialog.open(EntityDialogComponent, { data: { ...skillsData } });
  }

  onOpenJobDialog() {
    this.dialog.open(ManageJobDialogComponent);
  }
}

const jobCategoryData = {
  title: 'تصنيف الوظائف',
  key: 'JOB_CATEGORIES',
};
const experienceData = {
  title: 'الخبرات',
  key: 'EXPERIENCES',
};
const languageData = {
  title: 'اللغات',
  key: 'LANGUAGES',
};
const skillsData = {
  title: 'المهارات',
  key: 'SKILLS',
};
