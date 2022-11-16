import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { TaskDto } from "../task.types";
import { TasksService } from "../tasks.service";

@Component({
    selector: 'app-task-edit',
    templateUrl: './task-edit.component.html',
    styleUrls: ['./task-edit.component.scss']
})
export class TaskEditComponent implements OnInit {

    projectId?: number;
    taskId?: number;
    task?: TaskDto;

    constructor(
        private service: TasksService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.taskId = Number(this.route.snapshot.paramMap.get('taskId'));
        this.projectId = Number(this.route.snapshot.paramMap.get('projectId'));
        this.service.getById(this.taskId)
            .subscribe(task => {
                this.task = task;
            });
    }

    updateTask() {
        this.service.update(this.taskId!, this.task!)
            .subscribe(_ => {
                this.router.navigate(['/projects', 'view', this.projectId]);
            });
    }

    isDataLoaded() {
        return (
            this.taskId !== undefined &&
            this.projectId !== undefined &&
            this.task !== undefined
        );
    }
}
