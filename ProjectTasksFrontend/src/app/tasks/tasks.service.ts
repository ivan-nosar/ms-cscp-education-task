import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Task, TaskDto } from "./task.types";

@Injectable()
export class TasksService {

    private static _apiUrl = new URL(`api/v1/task`, environment.apiUrl).toString();

    constructor(private httpClient: HttpClient) { }

    static get apiUrl() {
        return TasksService._apiUrl;
    }

    getAll(projectId?: number) {
        const params = {
            ...(projectId !== undefined && { projectId })
        };

        return this.httpClient.get<Task[]>(TasksService._apiUrl, { params });
    }

    getById(id: number) {
        const url = `${TasksService._apiUrl}/${id}`;

        return this.httpClient.get<Task>(url);
    }

    create(task: TaskDto) {
        return this.httpClient.post<Task>(TasksService._apiUrl, task);
    }

    update(id: number, task: TaskDto) {
        const url = `${TasksService._apiUrl}/${id}`;

        return this.httpClient.put<Task>(url, task);
    }

    delete(id: number) {
        const url = `${TasksService._apiUrl}/${id}`;

        return this.httpClient.delete(url);
    }
}
