import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ProjectDto } from "../project.types";
import { ProjectsService } from "../projects.service";

@Component({
    selector: 'app-project-edit',
    templateUrl: './project-edit.component.html',
    styleUrls: ['./project-edit.component.scss']
})
export class ProjectEditComponent implements OnInit {

    projectId?: number;
    project?: ProjectDto;

    constructor(
        private service: ProjectsService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.projectId = Number(this.route.snapshot.paramMap.get('projectId'));
        this.service.getById(this.projectId)
            .subscribe(project => {
                this.project = project;
            });
    }

    updateProject() {
        this.service.update(this.projectId!, this.project!)
            .subscribe(_ => {
                this.router.navigate(['/projects']);
            });
    }

    isDataLoaded() {
        return (
            this.projectId !== undefined &&
            this.project !== undefined
        );
    }
}
