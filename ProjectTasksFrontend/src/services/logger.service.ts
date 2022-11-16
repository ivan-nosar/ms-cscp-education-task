import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class Logger {

    log(msg: any) {
        console.log(msg);
    }

    warn(msg: any) {
        console.warn(msg);
    }

    error(msg: any) {
        console.error(msg);
    }
}
