import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteEntityDialogComponent } from './delete-entity-dialog.component';

describe('DeleteEntityDialogComponent', () => {
    let component: DeleteEntityDialogComponent;
    let fixture: ComponentFixture<DeleteEntityDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [DeleteEntityDialogComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(DeleteEntityDialogComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
