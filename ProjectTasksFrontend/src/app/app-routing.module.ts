import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProjectComponent } from './projects/project.component';
import { NotFoundPageComponent } from "./common-components/not-found-page/not-found-page.component";
import { ProjectsListComponent } from './projects/projects-list/projects-list.component';
import { ProjectDetailsComponent } from './projects/project-details/project-details.component';
import { ProjectCreateComponent } from './projects/project-create/project-create.component';
import { ProjectEditComponent } from './projects/project-edit/project-edit.component';
import { TaskEditComponent } from './tasks/task-edit/task-edit.component';
import { TaskDetailsComponent } from './tasks/task-details/task-details.component';
import { TaskCreateComponent } from './tasks/task-create/task-create.component';
import { CommonComponentsModule } from './common-components/common-components.module';
import { UnlessReadonlyGuardService } from "./common-components/services/unless-readonly-guard.service";

const routes: Routes = [
    {
        path: 'projects',
        component: ProjectComponent,
        children: [
            { path: 'new', component: ProjectCreateComponent, canMatch: [UnlessReadonlyGuardService] },
            { path: 'edit/:projectId', component: ProjectEditComponent, canMatch: [UnlessReadonlyGuardService] },
            { path: 'view/:projectId', component: ProjectDetailsComponent },
            { path: ':projectId/tasks/new', component: TaskCreateComponent, canMatch: [UnlessReadonlyGuardService] },
            {
                path: ':projectId/tasks/edit/:taskId',
                component: TaskEditComponent,
                canMatch: [UnlessReadonlyGuardService]
            },
            { path: ':projectId/tasks/view/:taskId', component: TaskDetailsComponent },
            { path: '', component: ProjectsListComponent },
        ]
    },
    { path: '', redirectTo: 'projects', pathMatch: 'full' },
    { path: '**', component: NotFoundPageComponent },
];

@NgModule({
    imports: [
        CommonComponentsModule,
        RouterModule.forRoot(routes),
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
