import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { ProjectDto } from "../project.types";
import { ProjectsService } from "../projects.service";

@Component({
    selector: 'app-project-create',
    templateUrl: './project-create.component.html',
    styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent {

    project: ProjectDto = { name: '' };

    constructor(
        private service: ProjectsService,
        private router: Router
    ) { }

    createProject() {
        this.service.create(this.project)
            .subscribe(_ => {
                this.router.navigate(['/projects']);
            });
    }
}
