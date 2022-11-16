import { Task } from "../tasks/task.types";

export interface Project {
    id: number;
    name: string;

    tasks?: Task[];
};

export interface ProjectDto {
    name: string;
};
