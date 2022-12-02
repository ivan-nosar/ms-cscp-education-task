import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Project, ProjectDto } from "./project.types";

@Injectable()
export class ProjectsService {

    private static _apiUrl = new URL(`api/v1/project`, environment.apiUrl).toString();;

    constructor(private httpClient: HttpClient) { }

    static get apiUrl() {
        return ProjectsService._apiUrl;
    }

    getAll(withTasks: boolean = false) {
        return this.httpClient.get<Project[]>(ProjectsService._apiUrl, {
            params: { withTasks: withTasks.toString() }
        });
    }

    getById(id: number, withTasks: boolean = false) {
        const url = `${ProjectsService._apiUrl}/${id}`;

        return this.httpClient.get<Project>(url, {
            params: { withTasks: withTasks.toString() }
        });
    }

    create(project: ProjectDto) {
        return this.httpClient.post<Project>(ProjectsService._apiUrl, project);
    }

    update(id: number, project: ProjectDto) {
        const url = `${ProjectsService._apiUrl}/${id}`;

        return this.httpClient.put<Project>(url, project);
    }

    delete(id: number) {
        const url = `${ProjectsService._apiUrl}/${id}`;

        return this.httpClient.delete(url);
    }
}
