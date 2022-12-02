import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {
    IPublicClientApplication,
    PublicClientApplication,
    BrowserCacheLocation,
    InteractionType,
} from '@azure/msal-browser';
import {
    MSAL_INSTANCE,
    MSAL_INTERCEPTOR_CONFIG,
    MsalInterceptorConfiguration,
    MSAL_GUARD_CONFIG,
    MsalGuardConfiguration,
    MsalBroadcastService,
    MsalService,
    MsalGuard,
    MsalRedirectComponent,
    MsalModule,
    MsalInterceptor,
} from '@azure/msal-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProjectsModule } from "./projects/projects.module";
import { AuthModule } from "./auth/auth.module";
import { environment } from "../environments/environment";
import { ProjectsService } from "./projects/projects.service";
import { TasksService } from "./tasks/tasks.service";

const isIE =
    window.navigator.userAgent.indexOf('MSIE ') > -1 ||
    window.navigator.userAgent.indexOf('Trident/') > -1;

// MSAL instance to be passed to msal-angular
export function MSALInstanceFactory(): IPublicClientApplication {
    return new PublicClientApplication({
        auth: {
            clientId: environment.aadAuth.clientId,
            authority: environment.aadAuth.loginAuthority,
            redirectUri: environment.aadAuth.redirectUri
        },
        cache: {
            cacheLocation: BrowserCacheLocation.LocalStorage,
            storeAuthStateInCookie: isIE,
        }
    });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
    protectedResourceMap.set(ProjectsService.apiUrl, environment.aadAuth.scopes);
    protectedResourceMap.set(TasksService.apiUrl, environment.aadAuth.scopes);

    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap
    };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
    return {
        interactionType: InteractionType.Redirect,
        authRequest: {
            scopes: environment.aadAuth.scopes,
        },
        loginFailedRoute: '/'
    };
}


@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        MsalModule,

        AuthModule,
        AppRoutingModule,
        ProjectsModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: MsalInterceptor,
            multi: true,
        },
        {
            provide: MSAL_INSTANCE,
            useFactory: MSALInstanceFactory,
        },
        {
            provide: MSAL_GUARD_CONFIG,
            useFactory: MSALGuardConfigFactory,
        },
        {
            provide: MSAL_INTERCEPTOR_CONFIG,
            useFactory: MSALInterceptorConfigFactory,
        },
        MsalService,
        MsalGuard,
        MsalBroadcastService,
    ],
    bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
