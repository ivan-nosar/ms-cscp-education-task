import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Task } from "../task.types";
import { TasksService } from "../tasks.service";

@Component({
    selector: 'app-task-details',
    templateUrl: './task-details.component.html',
    styleUrls: ['./task-details.component.scss']
})
export class TaskDetailsComponent implements OnInit {

    projectId?: number;
    taskId?: number;
    task?: Task;

    constructor(
        private service: TasksService,
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

    isDataLoaded() {
        return (
            this.taskId !== undefined &&
            this.projectId !== undefined &&
            this.task !== undefined
        );
    }
}
