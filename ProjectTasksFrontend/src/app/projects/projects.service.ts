import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Project, ProjectDto } from "./project.types";

@Injectable()
export class ProjectsService {

    private apiUrl: string;

    constructor(private httpClient: HttpClient) {
        this.apiUrl = new URL(`api/v1/project`, environment.apiUrl).toString();
    }

    getAll(withTasks: boolean = false) {
        return this.httpClient.get<Project[]>(this.apiUrl, {
            params: { withTasks: withTasks.toString() }
        });
    }

    getById(id: number, withTasks: boolean = false) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.get<Project>(url, {
            params: { withTasks: withTasks.toString() }
        });
    }

    create(project: ProjectDto) {
        return this.httpClient.post<Project>(this.apiUrl, project);
    }

    update(id: number, project: ProjectDto) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.put<Project>(url, project);
    }

    delete(id: number) {
        const url = `${this.apiUrl}/${id}`;

        return this.httpClient.delete(url);
    }
}
