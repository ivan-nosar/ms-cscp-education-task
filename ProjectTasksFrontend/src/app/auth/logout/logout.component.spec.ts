import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LogoutComponent } from './logout.component';

describe('LogoutComponent', () => {
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                RouterTestingModule
            ],
            declarations: [
                LogoutComponent
            ],
        }).compileComponents();
    });

    it('should create the logout', () => {
        const fixture = TestBed.createComponent(LogoutComponent);
        const logout = fixture.componentInstance;
        expect(logout).toBeTruthy();
    });
});
