import { Injectable } from '@angular/core';
import { CanMatch } from '@angular/router';
import { environment } from '../../../environments/environment';

@Injectable()
export class UnlessReadonlyGuardService implements CanMatch {

    private isReadonly: boolean;

    constructor() {
        this.isReadonly = environment.isReadonly;
    }

    canMatch(): boolean {
        return !this.isReadonly;
    }
}
