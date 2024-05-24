import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpEntityService } from '../http-entity.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { JobCategory } from '../../http-applicant.service';
import { EntityService } from '../entity.service';

interface EntityData {
  title: string;
  key: string;
}

@Component({
  selector: 'app-entity-dialog',
  templateUrl: './entity-dialog.component.html',
  styleUrls: ['./entity-dialog.component.css'],
})
export class EntityDialogComponent implements OnInit {
  addEntityForm: FormGroup;
  isLoading = false;
  jobCategories: JobCategory[];
  list: any;
  submitFunction: any;

  constructor(
    private httpEntityService: HttpEntityService,
    private entityService: EntityService,
    @Inject(MAT_DIALOG_DATA) public data: EntityData
  ) {}

  ngOnInit(): void {
    console.log(this.data);

    switch (this.data.key) {
      case 'JOB_CATEGORIES':
        this.list = this.entityService.getJobCategories();

        this.entityService.jobCategoriesChanged.subscribe((jobCategories) => {
          this.list = jobCategories;
        });

        break;
      case 'EXPERIENCES':
        this.list = this.entityService.getExperiences();

        this.entityService.experiencesChanged.subscribe((experiences) => {
          this.list = experiences;
        });

        break;

      case 'LANGUAGES':
        this.list = this.entityService.getLanguages();

        this.entityService.languagesChanged.subscribe((languages) => {
          this.list = languages;
        });

        break;

      case 'SKILLS':
        this.list = this.entityService.getComputerSkills();

        this.entityService.computerSkillsChanged.subscribe((computerSkill) => {
          this.list = computerSkill;
        });

        break;
    }

    this.initForm();
  }

  onSubmit() {
    if (!this.addEntityForm.valid) {
      return;
    }

    const name = this.addEntityForm.value['name'];
    const notes = this.addEntityForm.value['notes'];

    switch (this.data.key) {
      case 'JOB_CATEGORIES':
        // this.httpEntityService
        //   .addJobCategory(name, notes)
        //   .subscribe((res) => console.log(res));
        this.entityService.addJobCategory(name, notes);
        this.addEntityForm.reset();

        break;
      case 'EXPERIENCES':
        // this.httpEntityService
        //   .addExperience(name, notes)
        //   .subscribe((res) => console.log(res));
        this.entityService.addExperience(name, notes);
        this.addEntityForm.reset();

        break;
      case 'LANGUAGES':
        // this.httpEntityService
        //   .addJobCategory(name, notes)
        //   .subscribe((res) => console.log(res));
        this.entityService.addLanguage(name, notes);
        this.addEntityForm.reset();

        break;
      case 'SKILLS':
        // this.httpEntityService
        //   .addExperience(name, notes)
        //   .subscribe((res) => console.log(res));
        this.entityService.addComputerSkill(name, notes);
        this.addEntityForm.reset();

        break;
    }
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
    this.addEntityForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      notes: new FormControl('', [Validators.maxLength(250)]),
    });
  }
}
