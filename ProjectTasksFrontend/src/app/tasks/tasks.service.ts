import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Task, TaskDto } from "./task.types";

@Injectable()
export class TasksService {

    private apiUrl: string;

    constructor(private httpClient: HttpClient) {
        this.apiUrl = new URL(`api/v1/task`, environment.apiUrl).toString();
    }

    getAll(projectId?: number) {
        const params = {
            ...(projectId !== undefined && { projectId })
        };

        return this.httpClient.get<Task[]>(this.apiUrl, { params });
    }

    getById(id: number) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.get<Task>(url);
    }

    create(task: TaskDto) {
        return this.httpClient.post<Task>(this.apiUrl, task);
    }

    update(id: number, task: TaskDto) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.put<Task>(url, task);
    }

    delete(id: number) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.delete(url);
    }
}
