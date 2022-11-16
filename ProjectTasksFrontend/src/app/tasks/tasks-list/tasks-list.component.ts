import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Task } from "../task.types";
import { TasksService } from "../tasks.service";
import {
    DeleteEntityDialogComponent
} from "../../common-components/delete-entity-dialog/delete-entity-dialog.component";

@Component({
    selector: 'app-tasks-list[projectId]',
    templateUrl: './tasks-list.component.html',
    styleUrls: ['./tasks-list.component.scss']
})
export class TasksListComponent implements OnInit {

    @Input() tasks?: Task[];
    @Input() projectId: number = 0;

    constructor(
        private service: TasksService,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        if (this.tasks === undefined) {
            this.reloadTasks();
        }
    }

    reloadTasks() {
        this.service.getAll(this.projectId)
            .subscribe((data: Task[]) => this.tasks = data);
    }

    isDataLoaded() {
        return this.tasks !== undefined;
    }

    deleteTask(task: Task, $event: any) {
        const deleteDialogRef = this.dialog.open(DeleteEntityDialogComponent, {
            data: {
                entityType: "task",
                entityName: task.name,
            }
        });

        deleteDialogRef.afterClosed().subscribe((result: boolean) => {
            if (!result) {
                return;
            }

            this.service.delete(task.id)
                .subscribe(_ => this.reloadTasks());
        });

        $event.stopPropagation();
    }
}
