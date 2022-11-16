import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-delete-entity-dialog',
    templateUrl: './delete-entity-dialog.component.html',
    styleUrls: ['./delete-entity-dialog.component.scss']
})
export class DeleteEntityDialogComponent {
    constructor(
        public dialogRef: MatDialogRef<DeleteEntityDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DeleteEntityDialogData
    ) { }

    reject() {
        this.dialogRef.close(false);
    }

    approve() {
        this.dialogRef.close(true);
    }
}

export interface DeleteEntityDialogData {
    entityType: string;
    entityName: string;
}
