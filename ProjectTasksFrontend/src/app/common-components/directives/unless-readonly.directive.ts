import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { environment } from '../../../environments/environment';

@Directive({
    selector: '[appUnlessReadonly]'
})
export class UnlessReadonlyDirective {

    private isReadonly: boolean;
    private showed: boolean;

    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef
    ) {
        this.isReadonly = environment.isReadonly;
        this.showed = false;
    }

    @Input() set appUnlessReadonly(condition: boolean) {
        // Too nested condition could be simplified - but thus it will become unreadable
        if (condition) {
            // Show only if not readonly
            if (!this.isReadonly) {
                this.show();
            } else {
                this.hide();
            }
        } else {
            // Show always
            this.show();
        }
    }

    show() {
        if (this.showed) {
            return;
        }

        this.viewContainer.createEmbeddedView(this.templateRef);

        this.showed = true;
    }

    hide() {
        if (!this.showed) {
            return;
        }

        this.viewContainer.clear();

        this.showed = false;
    }
}
