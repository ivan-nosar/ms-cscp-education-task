import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { TaskDto } from "../task.types";
import { TasksService } from "../tasks.service";

@Component({
    selector: 'app-task-create',
    templateUrl: './task-create.component.html',
    styleUrls: ['./task-create.component.scss']
})
export class TaskCreateComponent implements OnInit {

    projectId?: number;
    task: TaskDto = { name: '', description: '', projectID: 0 };

    constructor(
        private service: TasksService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.projectId = Number(this.route.snapshot.paramMap.get('projectId'));
        this.task.projectID = this.projectId;
    }

    createTask() {
        this.service.create(this.task)
            .subscribe(_ => {
                this.router.navigate(['/projects', 'view', this.projectId]);
            });
    }
}
