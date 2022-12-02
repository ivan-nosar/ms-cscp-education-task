import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from "@azure/msal-angular";
import { BrowserUtils } from "@azure/msal-browser";

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
import { UnlessReadonlyGuard } from "./common-components/guards/unless-readonly.guard";

const routes: Routes = [
    {
        path: 'projects',
        component: ProjectComponent,
        canActivate: [MsalGuard],
        children: [
            { path: 'new', component: ProjectCreateComponent, canMatch: [UnlessReadonlyGuard] },
            { path: 'edit/:projectId', component: ProjectEditComponent, canMatch: [UnlessReadonlyGuard] },
            { path: 'view/:projectId', component: ProjectDetailsComponent },
            { path: ':projectId/tasks/new', component: TaskCreateComponent, canMatch: [UnlessReadonlyGuard] },
            {
                path: ':projectId/tasks/edit/:taskId',
                component: TaskEditComponent,
                canMatch: [UnlessReadonlyGuard]
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
        RouterModule.forRoot(routes, {
            // Don't perform initial navigation in iframes or popups
            initialNavigation:
                !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup()
                    ? 'enabledNonBlocking'
                    : 'disabled',
        }),
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
