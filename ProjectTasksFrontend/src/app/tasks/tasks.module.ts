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

import { TasksListComponent } from './tasks-list/tasks-list.component';
import { TaskEditComponent } from './task-edit/task-edit.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { TaskCreateComponent } from './task-create/task-create.component';
import { CommonComponentsModule } from "../common-components/common-components.module";
import { TasksService } from "./tasks.service";

@NgModule({
    declarations: [
        TasksListComponent,
        TaskEditComponent,
        TaskDetailsComponent,
        TaskCreateComponent
    ],
    providers: [TasksService],
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
    ],
    exports: [
        TasksListComponent,
    ]
})
export class TasksModule { }
