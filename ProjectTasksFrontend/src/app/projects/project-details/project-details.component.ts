import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { ProjectsService } from "../projects.service";
import { Project } from "../project.types";

@Component({
    selector: 'app-project-details',
    templateUrl: './project-details.component.html',
    styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {
    projectId?: number;
    project?: Project;

    constructor(
        private service: ProjectsService,
        private route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        this.projectId = Number(this.route.snapshot.paramMap.get('projectId'));
        this.service.getById(this.projectId, true)
            .subscribe(project => {
                this.project = project;
            });
    }

    isDataLoaded() {
        return (
            this.projectId !== undefined &&
            this.project !== undefined
        );
    }
}
