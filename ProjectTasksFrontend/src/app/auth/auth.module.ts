import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { LogoutComponent } from "./logout/logout.component";


@NgModule({
    declarations: [
        LogoutComponent,
    ],
    imports: [
        CommonModule,
        MatButtonModule,
        MatIconModule,
    ],
    exports: [
        LogoutComponent,
    ]
})
export class AuthModule { }
