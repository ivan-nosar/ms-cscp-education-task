export interface Task {
    id: number;
    projectID: number;
    name: string;
    description: string;
};

export interface TaskDto {
    projectID: number;
    name: string;
    description: string;
};
