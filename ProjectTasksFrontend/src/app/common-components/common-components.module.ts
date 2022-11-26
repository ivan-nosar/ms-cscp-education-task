import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

import { DeleteEntityDialogComponent } from './delete-entity-dialog/delete-entity-dialog.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { UnlessReadonlyGuardService } from "./services/unless-readonly-guard.service";
import { UnlessReadonlyDirective } from './directives/unless-readonly.directive';

@NgModule({
    declarations: [
        DeleteEntityDialogComponent,
        NotFoundPageComponent,
        UnlessReadonlyDirective,
    ],
    providers: [
        UnlessReadonlyGuardService,
    ],
    imports: [
        CommonModule,
        RouterModule,
        MatButtonModule,
        MatDialogModule
    ],
    exports: [
        DeleteEntityDialogComponent,
        NotFoundPageComponent,
        UnlessReadonlyDirective,
    ]
})
export class CommonComponentsModule { }
