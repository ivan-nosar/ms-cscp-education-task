import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Project } from "../project.types";
import { ProjectsService } from "../projects.service";
import {
    DeleteEntityDialogComponent
} from "../../common-components/delete-entity-dialog/delete-entity-dialog.component";

@Component({
    selector: 'app-projects-list',
    templateUrl: './projects-list.component.html',
    styleUrls: ['./projects-list.component.scss']
})
export class ProjectsListComponent implements OnInit {

    projects: Project[] = [];

    constructor(
        private service: ProjectsService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.reloadProjects();
    }

    reloadProjects() {
        this.service.getAll()
            .subscribe((data: Project[]) => this.projects = data);
    }

    deleteProject(project: Project, $event: any) {
        const deleteDialogRef = this.dialog.open(DeleteEntityDialogComponent, {
            data: {
                entityType: "project",
                entityName: project.name,
            }
        });

        deleteDialogRef.afterClosed().subscribe((result: boolean) => {
            if (!result) {
                return;
            }

            this.service.delete(project.id)
                .subscribe(_ => this.reloadProjects());
        });

        $event.stopPropagation();
    }
}
