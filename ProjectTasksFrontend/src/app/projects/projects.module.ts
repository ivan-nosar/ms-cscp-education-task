import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';

import { ProjectComponent } from './project.component';
import { ProjectsListComponent } from './projects-list/projects-list.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';
import { ProjectCreateComponent } from './project-create/project-create.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { ProjectsService } from "./projects.service";
import { TasksModule } from "../tasks/tasks.module";
import { CommonComponentsModule } from "../common-components/common-components.module";

@NgModule({
    declarations: [
        ProjectComponent,
        ProjectsListComponent,
        ProjectDetailsComponent,
        ProjectCreateComponent,
        ProjectEditComponent,
    ],
    providers: [ProjectsService],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        MatListModule,
        MatIconModule,
        MatButtonModule,
        MatDividerModule,
        MatFormFieldModule,
        MatInputModule,
        MatDialogModule,

        CommonComponentsModule,
        TasksModule,
    ],
    exports: [
        ProjectComponent
    ]
})
export class ProjectsModule { }
